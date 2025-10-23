//ReceManager.cs
using UnityEngine;
using TMPro;   // TextMeshPro を使うため

public class RaceManager : MonoBehaviour
{
    // ======================================================
    // 🔸 シングルトン（他のスクリプトから簡単にアクセスできるようにする）
    // ======================================================
    public static RaceManager Instance;
    public bool IsCountdownActive()
    {
        return countdownActive;
    }
    // ======================================================
    // 🔸 インスペクターで設定する項目
    // ======================================================
    public TextMeshProUGUI messageText;     // 「3・2・1・GO!」「Finish!!」などを表示するUI
    public PlayerCarController playerCar;   // プレイヤー車（スクリプト付きオブジェクト）
    public RivalCarController[] rivals;     // ライバル車（複数台対応）

    // ======================================================
    // 🔸 内部で使う変数
    // ======================================================
    private float raceStartTime;     // レースが開始された瞬間の時間
    private bool raceOngoing = false; // 現在レース中かどうか
    private RaceState currentState = RaceState.Waiting; // 状態を管理
   
    // ======================================================
    // 🔸 レース状態を表す列挙型
    // ======================================================
    private enum RaceState
    {
        Waiting,   // スタート前
        Running,   // レース中
        Finished   // ゴール後
    }

    // ======================================================
    // 🔸 Awake（最初に呼ばれる）— シングルトン設定
    // ======================================================
    private void Awake()
    {
        // まだインスタンスが存在しないなら自分を登録
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが変わっても消えないようにする
        }
        else
        {
            Destroy(gameObject); // すでにあるなら重複を防ぐため削除
        }

        // メッセージテキストがあるなら非表示にしておく
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    // ======================================================
    // 🔸 Start — ゲーム起動時に呼ばれる
    // ======================================================
    private void Start()
    {
        // 最初はスタート前状態
        currentState = RaceState.Waiting;

        // カウントダウンを開始
        StartCountdown();
    }

    // ======================================================
    // 🔸 カウントダウン処理（コルーチンを使わない版）
    // ======================================================
    private float countdownTimer = 3f; // カウントダウンの秒数
    private bool countdownActive = true;

    private void Update()
    {
        // スタート前のカウントダウン中だけ動作
        if (countdownActive)
        {
            // 毎フレーム、残り時間を減らす
            countdownTimer -= Time.deltaTime;

            // 小数点を切り上げて「3」「2」「1」を表示
            int displayNum = Mathf.CeilToInt(countdownTimer);
            if (displayNum > 0)
            {
                ShowMessage(displayNum.ToString(), 0.2f);
            }
            else
            {
                // 0以下になったらスタート！
                countdownActive = false;
                StartRace();
            }
        }

        // メッセージ表示の時間管理
        HandleMessageDisplay();
    }

    // ======================================================
    // 🔸 カウントダウン開始メソッド
    // ======================================================
    private void StartCountdown()
    {
        Debug.Log("StartCountdown called - 操作禁止にします");
        countdownTimer = 3f;
        countdownActive = true;

        // プレイヤーとライバルの操作を禁止（動かないように）
        if (playerCar != null)
            playerCar.DisableControl();

        if (rivals != null)
        {
            foreach (var r in rivals)
                r.DisableControl();
        }
    }

    // ======================================================
    // 🔸 レース開始処理
    // ======================================================
    public void StartRace()
    {
        Debug.Log("StartRace called - 操作許可します");
        ShowMessage("GO!", 1f);            // GO!表示
        raceStartTime = Time.time;          // タイマー開始
        raceOngoing = true;                 // レース中フラグON
        currentState = RaceState.Running;   // 状態変更

        // プレイヤーを操作可能にする
        if (playerCar != null)
            playerCar.EnableControl();

        // ライバルたちも動けるように
        if (rivals != null)
        {
            foreach (var r in rivals)
            {
                r.EnableControl();

                // 👇 ランダムな速度を与える（個性を出す）
                r.SetRandomSpeed();
            }
        }
    }

    // ======================================================
    // 🔸 ゴール時に呼び出される
    // ======================================================
    public void Finish()
    {
        // まだレース中でなければ無視
        if (!raceOngoing) return;

        // 経過時間を計算
        float finishTime = Time.time - raceStartTime;

        // フラグをOFF
        raceOngoing = false;
        currentState = RaceState.Finished;

        // プレイヤーを止める
        if (playerCar != null)
        {
            playerCar.DisableControl();
            StopCar(playerCar);
        }

        // ライバルも止める
        if (rivals != null)
        {
            foreach (var r in rivals)
            {
                r.DisableControl();
                StopCar(r);
            }
        }

        // タイムを表示
        ShowMessage($"Finish!!\nTime: {finishTime:F2} + 秒", 3f);
        Debug.Log("レース終了！タイム: " + finishTime + "秒");
    }

    // ======================================================
    // 🔸 車を物理的に停止させる処理
    // ======================================================
    private void StopCar(MonoBehaviour car)
    {
        // Rigidbody2D コンポーネントを取得
        Rigidbody2D rb = car.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 移動速度をゼロに
            rb.linearVelocity = Vector2.zero;

            // 回転（スピン）も止める
            rb.angularVelocity = 0f;
        }
    }

    // ======================================================
    // 🔸 メッセージ表示処理（時間経過で消える）
    // ======================================================
    private float messageDisplayTime = 0f; // メッセージを出した時間
    private float messageDuration = 0f;    // 表示時間の長さ
    private string currentMessage = "";    // 今表示している内容

    public void ShowMessage(string message, float duration = 1f)
    {
        // メッセージUIが設定されていなければ何もしない
        if (messageText == null) return;

        // 同じメッセージを何度も出さないように
        if (message == currentMessage) return;

        currentMessage = message;

        // テキストを更新して表示
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        // 時間を記録
        messageDuration = duration;
        messageDisplayTime = Time.time;
    }

    // ======================================================
    // 🔸 Updateで呼ばれる「メッセージ時間管理」
    // ======================================================
    private void HandleMessageDisplay()
    {
        if (messageText == null) return;

        // 表示中で、かつ一定時間経過したら非表示にする
        if (messageText.gameObject.activeSelf &&
            (Time.time - messageDisplayTime > messageDuration))
        {
            messageText.gameObject.SetActive(false);
            currentMessage = ""; // 状態リセット
        }
    }
}
