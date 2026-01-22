//RivalCarController
//using JetBrains.Annotations;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ライバル車を制御するスクリプト
/// プレイヤーと同じように前進するが、操作入力はなく
/// ランダムなスピードでまっすぐ走るだけ
/// </summary>
public class RivalCarController : MonoBehaviour
{
    // ======================================================
    // 🔸 公開パラメータ（Inspectorで調整可能）
    // ======================================================
    private float minSpeed = 20f;   // ライバルの最低速度
    private float maxSpeed = 21f;  // ライバルの最高速度

    public float houseminSpeed = 0;
    public float housemaxSpeed = 0;
    public float officeminSpeed = 0;
    public float officemaxSpeed = 0;
    public float bankminSpeed = 0;
    public float bankmaxSpeed = 0;

    private float acceleration = 10f; // 加速力
    public float deceleration = 2f; // 減速力
    public float baseMin;
    public float baseMax;

    // ======================================================
    // 🔸 内部管理用
    // ======================================================
    private float targetSpeed = 0f;   // 目標速度（ランダムで決まる）
    private float currentSpeed = 0f;  // 現在速度
    private bool canDrive = false;    // 動けるかどうか
    private Rigidbody2D rb;           // Rigidbody2Dコンポーネント
    

    // ======================================================
    // 🔸 初期化処理
    // ======================================================
    /// <summary>
    /// スタート前にRivalを初期化する
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Rigidbodyがなければ警告
        if (rb == null)
        {
            Debug.LogWarning($"{gameObject.name} に Rigidbody2D がありません！");
        }

        // 最初は止まっている
        currentSpeed = 0f;
        targetSpeed = 0f;
    }

    // ======================================================
    // 🔸 毎フレーム呼ばれる処理
    // ======================================================
    void Update()
    {

        // 動けない状態なら止める
        if (!canDrive)
        {
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
            return;
        }

        // 目標速度に近づくように現在速度を調整
        if (currentSpeed < targetSpeed)
            currentSpeed += acceleration * Time.deltaTime;
        else if (currentSpeed > targetSpeed)
            currentSpeed -= deceleration * Time.deltaTime;

            // 過剰な速度を防ぐ
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Rigidbodyを使って前進（上方向に移動）
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, currentSpeed);
        }
    }

    // ======================================================
    // 🔸 動けるようにする（RaceManagerから呼ばれる）
    // ======================================================
    public void EnableControl()
    {
        canDrive = true;
    }

    // ======================================================
    // 🔸 操作を無効化する（ゴール後など）
    // ======================================================
    public void DisableControl()
    {
        canDrive = false;
        currentSpeed = 0f;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    // ======================================================
    // 🔸 スピードをランダムに設定する
    // ======================================================
    /// <summary>
    /// プレイヤーの段階ごとの最高速度に合わせて速度をばらつきがあるように設定する
    /// </summary>
    public void SetRandomSpeed()
    {

        //民家
        if (GameStateManager.Game_Progress == 2)
        {
            minSpeed = houseminSpeed;
            maxSpeed = housemaxSpeed;
        }
        //オフィス
        else if (GameStateManager.Game_Progress == 4)
        {
            minSpeed = officeminSpeed;
            maxSpeed = officemaxSpeed;
        }
        //銀行
        else if (GameStateManager.Game_Progress == 5)
        {
            minSpeed = bankminSpeed;
            maxSpeed = bankmaxSpeed;
        }

        //ランダム設定
        targetSpeed = Random.Range(minSpeed, maxSpeed);

        // デバッグ出力（ゲーム中はコンソールに出る）
        Debug.Log($"{gameObject.name} の目標速度: {targetSpeed:F2} / 現在速度: {currentSpeed:F2}");
    }     
}
