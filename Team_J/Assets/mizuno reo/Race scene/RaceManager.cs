
// RaceManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using TMPro;

public class RaceManager : MonoBehaviour
{
    // ======================================================
    // 🔹 シングルトン設定
    // ======================================================
    public static RaceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("UI関連")]
    [SerializeField] private Text messageText;
    [SerializeField] private GameObject @objectmessageBackground;

    [Header("車両関連")]
    public PlayerCarController playerCar;
    public List<RivalCarController> rivals = new List<RivalCarController>();

    [Header("Raycastゴール判定用")]
    [SerializeField] private float goalRayDistance = 5f;   // ★Rayの距離（必要に応じて調整）
    [SerializeField] private LayerMask goalLayer;          // ★ゴールオブジェクト専用のLayer

    // ======================================================
    // 🔹 レース状態管理
    // ======================================================
    private enum RaceState { Countdown, Racing, Finish, Done }
    private RaceState raceState = RaceState.Countdown;

    private enum FinishState { None, ShowRank, ShowPoints, ShowFinalResults, Done }
    private FinishState finishState = FinishState.None;

    private float stateTimer = 0f;

    // ======================================================
    // 🔹 カウントダウン関連
    // ======================================================
    private bool countdownActive = false;
    private int countdownValue = 3;

    // ======================================================
    // 🔹 リザルト関連
    // ======================================================
    private float raceStartTime;
    private List<GameObject> finishedCars = new List<GameObject>();
    private List<string> finishOrder = new List<string>();
    private int playerRank;
    private float playerPoints;

    private string currentMessage = "";
    private float messageDisplayTime = 0f;
    private float messageDuration = 0f;

    public GameObject countdownBG;

    private void Start()
    {
        StartCountdown();
    }

    private void Update()
    {
        HandleFinishSequence();
        HandleMessageDisplay();

        //レース中のみゴール判定
        if(raceState == RaceState.Racing && playerCar != null)
        {
            CheckPlayerGoalRay();
        }

    }

    // ======================================================
    // 🔹 カウントダウン開始（コルーチン）
    // ======================================================
    private void StartCountdown()
    {
        raceState = RaceState.Countdown;
        countdownActive = true;
        countdownValue = 3;

        if (playerCar != null)
        {
            playerCar.DisableControl();
            playerCar.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        if (rivals != null)
        {
            foreach (var r in rivals)
            {
                r.DisableControl();
                r.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        // カウントダウン開始前に少し待機して描画安定
        yield return new WaitForSeconds(0.1f);

        // 3, 2, 1 のカウントダウン
        while (countdownValue > 0)
        {
            ShowMessage(countdownValue.ToString(), 1f);
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        // GO! 表示
        ShowMessage("GO!", 1f);

        // GO! が見えている1秒間はまだ操作できない
        yield return new WaitForSeconds(1f);

        // カウントダウン終了 → レース開始
        countdownActive = false;
        StartRace();
    }

    // ======================================================
    // 🔹 レース開始
    // ======================================================
    public void StartRace()
    {
        raceState = RaceState.Racing;
        raceStartTime = Time.time;

        if (playerCar != null) playerCar.EnableControl();
        if (rivals != null)
        {
            foreach (var r in rivals)
            {
                r.EnableControl();
                r.SetRandomSpeed();
            }
        }

        Debug.Log("レース開始！");
        countdownBG.SetActive(false);
    }

    // ======================================================
    // 🔹 ゴール処理
    // ======================================================
    /// <summary>
    /// Rivalが先にゴールしたときに残りの順位を計算する
    /// </summary>
    /// <param name="car"></param>
    public void RegisterFinish(GameObject car)
    {
        if (finishedCars.Contains(car)) return;

        finishedCars.Add(car);
        finishOrder.Add(car.name);

        int rank = finishOrder.Count;
        float time = Time.time - raceStartTime;

        if (car.CompareTag("Rival"))
        {
            Debug.Log($"Rival {car.name} が {rank} 位でゴール！");
            return;
        }
        
        if (car.CompareTag("Player"))
        {
            playerRank = rank;
            playerPoints = CalculatePoints(rank);

            if (Money.Instance != null)
            {
                Money.Instance.DayPoint += (int)playerPoints;
                Debug.Log($"[Race] Rank {playerRank} → +{playerPoints}pt / DayPoint={Money.Instance.DayPoint}");
            }

            if (playerCar != null) playerCar.DisableControl();
            if (rivals != null)
            {
                foreach (var r in rivals) r.DisableControl();
            }

            raceState = RaceState.Finish;
            finishState = FinishState.ShowRank;
            stateTimer = 0f;

            PlayerDataManager.AddPoints(playerPoints);
            SaveResult(playerRank, (int)playerPoints);
        }
        PlayerPrefs.SetInt("EvaluationPoint", Money.Instance.DayPoint);
        PlayerPrefs.Save();
    }

    // ======================================================
    // 🔹 ゴール後のメッセージ・シーケンス
    // ======================================================

    /// <summary>
    /// プレイヤーがゴールした時にメッセージを表示する
    /// </summary>
    private void HandleFinishSequence()
    {
        if (raceState != RaceState.Finish) return;

        stateTimer += Time.unscaledDeltaTime;
        countdownBG.SetActive(true);
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
                ShowMessage($"{playerPoints} Get Points!!", 2f);
                if (stateTimer > 2f)
                {
                    finishState = FinishState.ShowFinalResults;
                    stateTimer = 0f;
                }
                break;

            case FinishState.ShowFinalResults:
                ShowFinalResults();
                if (stateTimer > 3f)
                {
                    finishState = FinishState.Done;
                    raceState = RaceState.Done;
                    SceneManager.LoadScene("Result Scene2");
                }
                break;
        }
    }

    // ======================================================
    // 🔹 メッセージ表示関連
    // ======================================================
    private void ShowMessage(string msg, float duration)
    {
        currentMessage = msg;
        messageDuration = duration;
        messageDisplayTime = 0f;

        if (messageText != null)
            messageText.text = msg;

        //if (messageBackground != null)
        //    messageBackground.SetActive(true);
    }

    private void HandleMessageDisplay()
    {
        if (string.IsNullOrEmpty(currentMessage)) return;

        messageDisplayTime += Time.unscaledDeltaTime;

        if (messageDisplayTime >= messageDuration)
        {
            if (messageText != null)
                messageText.text = "";
            currentMessage = "";
        }
    }

    // ======================================================
    // 🔹 ポイント計算・保存
    // ======================================================
    /// <summary>
    /// ゴールした時に順位ごとにポイントを計算する
    /// </summary>
    /// <param name="rank"></param>
    /// <returns></returns>
    private float CalculatePoints(int rank)
    {
        switch (rank)
        {
            case 1: return 100;
            case 2: return 70;
            case 3: return 30;
            default: return 10;

        }
    }
    /// <summary>
    /// ゴールした時に順位ごとにポイントを保存する
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="points"></param>
    private void SaveResult(int rank, int points)
    {
        PlayerPrefs.SetInt("LastRank", rank);
        PlayerPrefs.SetInt("LastPoints", points);

        int currentTotal = PlayerPrefs.GetInt("TotalRacePoints", 0);
        currentTotal += points;
        PlayerPrefs.SetInt("TotalRacePoints", currentTotal);
        PlayerPrefs.Save();
    }

    // 🔹 リザルト表示

    private void ShowFinalResults()
    {
        ShowMessage("Moving to Results...", 3f);
    }

    // 🔹 状態確認メソッド

    public bool IsRaceStarted() => raceState == RaceState.Racing;
    public bool IsCountdownActive() => raceState == RaceState.Countdown;
    public bool IsRaceFinished() => raceState == RaceState.Finish || raceState == RaceState.Done;

    /// <summary>
    /// PlayerCar から前方向に Ray を飛ばしてゴール判定をつける
    /// </summary>
    private void CheckPlayerGoalRay()
    {
        // Ray の開始位置：車の位置
        Vector2 origin = playerCar.transform.position;

        // Ray の方向：車の進行方向
        // 右向きが前の場合は transform.right、上が前なら transform.up に変更
        Vector2 direction = playerCar.transform.right;

        // Sceneビューで見えるように Ray を描画（デバッグ用）
        Debug.DrawRay(origin, direction * goalRayDistance, Color.green);

        // Raycast 実行
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, goalRayDistance, goalLayer);

        if (hit.collider != null)
        {
            // ゴールに当たったら RegisterFinish で処理
            RegisterFinish(playerCar.gameObject);
        }
    }

}
