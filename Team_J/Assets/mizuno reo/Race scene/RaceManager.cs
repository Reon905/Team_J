
// RaceManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("車両関連")]
    public PlayerCarController playerCar;
    public List<RivalCarController> rivals = new List<RivalCarController>();

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

    private void Start()
    {
        StartCountdown();
    }

    private void Update()
    {
        HandleFinishSequence();
        HandleMessageDisplay();
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
    }

    // ======================================================
    // 🔹 ゴール処理
    // ======================================================
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

    private void SaveResult(int rank, int points)
    {
        PlayerPrefs.SetInt("LastRank", rank);
        PlayerPrefs.SetInt("LastPoints", points);

        int currentTotal = PlayerPrefs.GetInt("TotalRacePoints", 0);
        currentTotal += points;
        PlayerPrefs.SetInt("TotalRacePoints", currentTotal);
        PlayerPrefs.Save();
    }

    // ======================================================
    // 🔹 リザルト表示
    // ======================================================
    private void ShowFinalResults()
    {
        ShowMessage("Moving to Results...", 3f);
    }

    // ======================================================
    // 🔹 状態確認メソッド
    // ======================================================
    public bool IsRaceStarted() => raceState == RaceState.Racing;
    public bool IsCountdownActive() => raceState == RaceState.Countdown;
    public bool IsRaceFinished() => raceState == RaceState.Finish || raceState == RaceState.Done;
}
