//ReceManager.cs
// RaceManager.cs
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    // ======================================================
    // 🔹 シングルトン設定
    // ======================================================
    // 他のスクリプトから RaceManager にアクセスできるようにする
    public static RaceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this; // 最初に生成されたインスタンスを保持
        else
            Destroy(gameObject); // すでに存在する場合は破棄
    }

    [Header("UI関連")]
    [SerializeField] private TextMeshProUGUI messageText; // 画面にメッセージを表示するUI

    [Header("車両関連")]
    public PlayerCarController playerCar; // プレイヤー車
    public List<RivalCarController> rivals = new List<RivalCarController>(); // ライバル車リスト

    // ======================================================
    // 🔹 レース状態管理
    // ======================================================
    private enum RaceState { Countdown, Racing, Finish, Done } // レース全体の状態
    private RaceState raceState = RaceState.Countdown;

    private enum FinishState { None, ShowRank, ShowPoints, ShowFinalResults, Done } // ゴール後の表示状態
    private FinishState finishState = FinishState.None;

    private float stateTimer = 0f; // レース内タイマー（フレームで経過時間計測）

    // ======================================================
    // 🔹 カウントダウン関連
    // ======================================================
    private float countdownInterval = 1.0f;  // 数字切り替え間隔（秒）
    private float countdownTimer = 0f;       // 経過時間
    private int countdownValue = 3;          // カウントダウン開始値
    private bool countdownActive = false;    // カウントダウン中フラグ
    private bool firstFrameShown = false;    // 初期フレームの一瞬スキップ対策

    // ======================================================
    // 🔹 リザルト関連
    // ======================================================
    private float raceStartTime;                     // レース開始時間
    private List<GameObject> finishedCars = new List<GameObject>(); // ゴール済み車リスト
    private List<string> finishOrder = new List<string>();         // ゴール順（名前）
    private int playerRank;                           // プレイヤーの順位
    private float playerPoints;                       // プレイヤーの獲得ポイント

    private string currentMessage = "";   // 現在表示中のメッセージ
    private float messageDisplayTime = 0f; // メッセージ表示経過時間
    private float messageDuration = 0f;   // メッセージ表示時間

    private void Start()
    {
        StartCountdown(); // シーン開始時にカウントダウン開始
    }

    private void Update()
    {
        HandleCountdown();      // カウントダウン処理
        HandleFinishSequence(); // ゴール後のメッセージ・シーケンス
        HandleMessageDisplay(); // メッセージ表示管理
    }

    // ======================================================
    // 🔹 カウントダウン開始
    // ======================================================
    private void StartCountdown()
    {
        countdownValue = 3;
        countdownTimer = 0f;
        countdownActive = true;
        raceState = RaceState.Countdown;

        // 車の操作を一時停止
        if (playerCar != null) playerCar.DisableControl();
        if (rivals != null)
        {
            foreach (var r in rivals) r.DisableControl();
        }

        // 最初に「3」を即表示
        ShowMessage("3", 1f);
    }

    // ======================================================
    // 🔹 カウントダウン処理
    // ======================================================
    private void HandleCountdown()
    {
        // カウントダウン中以外は何もしない
        if (raceState != RaceState.Countdown || !countdownActive) return;

        countdownTimer += Time.unscaledDeltaTime; // 時間を加算（ゲームの時間スケールに依存しない）

        // 初フレームの経過時間を無視して表示安定化
        if (!firstFrameShown)
        {
            firstFrameShown = true;
            countdownTimer = 0f;
            return;
        }

        if (countdownTimer >= countdownInterval)
        {
            countdownTimer = 0f;
            countdownValue--; // カウントを1減らす

            if (countdownValue > 0)
            {
                ShowMessage("Lady…", countdownInterval); // 1 の代わりに「Lady…」を表示
            }
            else if (countdownValue == 0)
            {
                ShowMessage("GO!", 1f); // 最後にGOを表示
                countdownActive = false;
                StartRace(); // レース開始
            }
        }
    }

    // ======================================================
    // 🔹 レース開始
    // ======================================================
    public void StartRace()
    {
        raceState = RaceState.Racing;
        raceStartTime = Time.time;

        // 車の操作を有効化
        if (playerCar != null) playerCar.EnableControl();
        if (rivals != null)
        {
            foreach (var r in rivals)
            {
                r.EnableControl();
                r.SetRandomSpeed(); // ライバル車はランダム速度に
            }
        }

        Debug.Log("レース開始！");
    }

    // ======================================================
    // 🔹 ゴール処理
    // ======================================================
    public void RegisterFinish(GameObject car)
    {
        if (finishedCars.Contains(car)) return; // 既に登録済みなら無視

        finishedCars.Add(car);
        finishOrder.Add(car.name);

        int rank = finishOrder.Count;               // 現在の順位
        float time = Time.time - raceStartTime;    // 経過時間（デバッグ用）

        if (car.CompareTag("Rival"))
        {
            Debug.Log($"Rival {car.name} が {rank} 位でゴール！");
            return; // ライバルはここで処理終了
        }

        if (car.CompareTag("Player"))
        {
            playerRank = rank;
            playerPoints = CalculatePoints(rank); // 順位に応じてポイント計算

            // 操作を全車無効化
            if (playerCar != null) playerCar.DisableControl();
            if (rivals != null)
            {
                foreach (var r in rivals) r.DisableControl();
            }

            raceState = RaceState.Finish;
            finishState = FinishState.ShowRank;
            stateTimer = 0f;

            // ポイントを保存
            PlayerDataManager.AddPoints(playerPoints);
            SaveResult(playerRank, (int)playerPoints);
        }
    }

    // ======================================================
    // 🔹 ゴール後のメッセージ・シーケンス
    // ======================================================
    private void HandleFinishSequence()
    {
        if (raceState != RaceState.Finish) return;

        stateTimer += Time.unscaledDeltaTime;

        switch (finishState)
        {
            case FinishState.ShowRank:
                ShowMessage($"Finish!!\nRanking {playerRank} !!", 2f);
                if (stateTimer > 2f)
                {
                    finishState = FinishState.ShowPoints;
                    stateTimer = 0f;
                }
                break;

            case FinishState.ShowPoints:
                ShowMessage($"{playerPoints}Get Points!! ", 2f);
                if (stateTimer > 2f)
                {
                    finishState = FinishState.ShowFinalResults;
                    stateTimer = 0f;
                }
                break;

            case FinishState.ShowFinalResults:
                ShowFinalResults(); // 「リザルトを表示中…」と表示
                if (stateTimer > 3f)
                {
                    finishState = FinishState.Done;
                    raceState = RaceState.Done;
                    SceneManager.LoadScene("HasuiRikuto/Scene/Result Scene"); // リザルトシーンへ移動
                }
                break;
        }
    }

    // ======================================================
    // 🔹 メッセージ表示
    // ======================================================
    private void ShowMessage(string msg, float duration)
    {
        currentMessage = msg;
        messageDuration = duration;
        messageDisplayTime = 0f;

        if (messageText != null)
            messageText.text = msg;
    }

    private void HandleMessageDisplay()
    {
        if (string.IsNullOrEmpty(currentMessage)) return;

        messageDisplayTime += Time.unscaledDeltaTime;

        if (messageDisplayTime >= messageDuration)
        {
            if (messageText != null)
                messageText.text = ""; // 表示を消す
            currentMessage = "";
        }
    }

    // ======================================================
    // 🔹 ポイント計算（順位に応じて）
    // ======================================================
    private float CalculatePoints(int rank)
    {
        switch (rank)
        {
            case 1: return 100;
            case 2: return  80;
            case 3: return  30;
            default: return 10;
        }
    }

    // ======================================================
    // 🔹 結果をPlayerPrefsに保存
    // ======================================================
    private void SaveResult(int rank, int points)
    {
        PlayerPrefs.SetInt("LastRank", rank);
        PlayerPrefs.SetInt("LastPoints", points);
        PlayerPrefs.Save();
    }

    // ======================================================
    // 🔹 最終リザルト表示（デバッグ用）
    // ======================================================
    private void ShowFinalResults()
    {
        ShowMessage("Moving to Results...", 3f);
    }

    // ======================================================
    // 🔹 外部からレース状態を確認する用メソッド
    // ======================================================
    public bool IsRaceStarted() => raceState == RaceState.Racing;
    public bool IsCountdownActive() => raceState == RaceState.Countdown;
    public bool IsRaceFinished() => raceState == RaceState.Finish || raceState == RaceState.Done;
}
