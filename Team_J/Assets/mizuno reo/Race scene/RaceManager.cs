//ReceManager.cs
using TMPro;         // TextMeshPro 用（メッセージ表示）
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;  // シングルトン：他のスクリプトからアクセスしやすくする

    public TextMeshProUGUI messageText;      // カウントダウンやゴールなどの表示テキスト（インスペクターで設定）
    public PlayerCarController playerCar;    // 操作対象の車（インスペクターで設定）

    private float raceStartTime;             // レース開始時間（タイマーとして使用）
    private bool raceOngoing = false;        // レース中かどうか

    private Coroutine messageCoroutine;      // メッセージ表示コルーチンを管理するための変数

    void Awake()
    {
        // シングルトンの初期化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンをまたいでも残る
        }
        else
        {
            Destroy(gameObject);  // 2つ以上あったら古いほうを消す
        }

        // 最初はメッセージ非表示
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Start()
    {
        // ゲーム開始時にカウントダウンを始める
        StartCoroutine(RaceStartCoroutine());
    }

    public void StartRace()
    {
        StartCoroutine(RaceStartCoroutine());
    }
    // カウントダウンしてからレース開始する処理
    private System.Collections.IEnumerator RaceStartCoroutine()
    {
        // 車の操作を無効化（止める）
        if (playerCar != null)
            playerCar.DisableControl();

        // カウントダウン表示（1秒ずつ）
        string[] countdownTexts = { "3", "2", "1" };

        foreach (string t in countdownTexts)
        {
            ShowMessage(t, 1f);
            yield return new WaitForSeconds(1f);
        }

        // GO！表示
        ShowMessage("GO!", 1f);

        // 車の操作を有効化（ここでやっと車が動ける）
        if (playerCar != null)
        {
            playerCar.EnableControl();
        }
        else
        {
            Debug.LogError("playerCar が RaceManager にセットされていません！");
        }

        // タイマー開始
        raceStartTime = Time.time;
        raceOngoing = true;
    }

    // ゴール時に呼び出す（トリガーから）
    public void Finish()
    {
        if (!raceOngoing) return;

        float finishTime = Time.time - raceStartTime;
        raceOngoing = false;

        //車を止める(操作を無効化)
        if(playerCar != null)
        {
            playerCar.DisableControl(); //入力を止める
            StartCoroutine(SmoothStop(playerCar)); //ゆっくり止めるようにする
        }
       
        ShowMessage($"Finish!!\nTime: {finishTime:F2} 秒", 3f); //メッセージ表示
        Debug.Log("レース終了！タイム: " + finishTime + "秒");
    }

    private System.Collections.IEnumerator SmoothStop(PlayerCarController car)
    {
        Rigidbody2D rb = car.GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        while(rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity,Vector2.zero, 1f * Time.deltaTime);
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // メッセージを画面に表示する（数秒間）
    public void ShowMessage(string message, float duration = 2f)
    {
        // 前のメッセージが残ってたら止める
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // メッセージ表示コルーチン（時間経過後に非表示にする）
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        if (messageText == null)
        {
            Debug.LogWarning("messageText が設定されていません！");
            yield break;
        }

        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }
}
