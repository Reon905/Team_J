//PlaiyerCarController 
using UnityEngine;
using UnityEngine.UI;

public class PlayerCarController : MonoBehaviour
{
    // ▼ 公開変数（Inspectorで調整可能）
    public float acceleration = 5f;      // 加速度：Wキーを押している間に速度がどれだけ増えるか
    public float maxSpeed = 20f;         // 最大速度：この値以上には加速しない
    public float deceleration = 3f;      // 減速度：Wキーを離したときにどれだけ減速するか

    private float currentSpeed = 0f;     // 現在の速度（内部で管理）
    private Rigidbody2D rb;              // Rigidbody2D：物理演算で移動させるためのコンポーネント

    public Slider speedSlider;           // UIスライダー：スピードゲージ用（Inspectorでドラッグして設定）

    public bool canDrive = false;        // このフラグが true のときだけ車は動ける（カウントダウンが終わるまで false）

    //ブーストゲージ追加
    public float boostMultiplier = 2f;     //ブースト中の加速倍率
    public float boostDuration = 2f;       //ブーストの持続時間
    public float boostCooldown = 5f;       //ブースト俄然回復するまでの時間

    public Slider boostSlider;             //ブーストゲージ用スライダーUI
    private float boostTimeRemaining = 0f; //残りブースト時間
    private bool isBoosting = false;       //ブースト中フラグ
    void Start()
    {
        // Rigidbody2D を取得（Unity の物理制御で使う）
        rb = GetComponent<Rigidbody2D>();

        // スピードゲージが設定されていれば、範囲を初期化
        if (speedSlider != null)
        {
            speedSlider.minValue = 0f;
            speedSlider.maxValue = maxSpeed;
        }

        //ブーストゲージ初期化
        if (boostSlider != null)
        {
            boostSlider.minValue = 0f;
            boostSlider.maxValue = boostDuration;
            boostTimeRemaining = boostDuration;   //最初は満タン
            boostSlider.value = boostTimeRemaining;
        }
    }

    void Update()
    {
        // 操作可能かどうかチェック（canDriveがfalseなら車を止めて処理終了）
        if (!canDrive)
        {
            rb.linearVelocity = Vector2.zero; // 車を停止
            return;                    // 以降の処理をスキップ
        }

        // --- ブーストキーの入力判定 ---
        // キーが押されているか（Returnキーを押しているかどうか）
        bool isBoostKeyPressed = Input.GetKey(KeyCode.Return);

        // ブースト条件：キーが押されていて、かつゲージが残っている場合にブースト開始
        if (isBoostKeyPressed && boostTimeRemaining > 0f)
        {
            isBoosting = true;  // ブーストON
        }
        else
        {
            isBoosting = false; // ブーストOFF（キーを離したかゲージ切れ）
        }

        // --- 加速・減速の処理 ---
        // ブースト中は加速度をboostMultiplier倍にアップ
        float appliedAcceleration = isBoosting ? acceleration * boostMultiplier : acceleration;

        // Wキーが押されていれば加速、離していれば減速
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += appliedAcceleration * Time.deltaTime; // 加速
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;         // 減速
        }

        // --- 速度の最大値を設定 ---
        // ブースト中は maxSpeed × boostMultiplier まで速度上限アップ
        float currentMaxSpeed = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;

        // currentSpeedを0～currentMaxSpeedの範囲に制限
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, currentMaxSpeed);

        // --- Rigidbody2Dの速度を更新（Y軸方向に移動） ---
        rb.linearVelocity = new Vector2(0, currentSpeed);

        // --- ブーストゲージの消費・回復処理 ---
        if (isBoosting)
        {
            // ブースト使用中はゲージを減らす
            boostTimeRemaining -= Time.deltaTime;

            // ゲージがなくなったらブースト終了
            if (boostTimeRemaining <= 0f)
            {
                boostTimeRemaining = 0f;
                isBoosting = false;
            }
        }
        else
        {
            // ブースト未使用時はゲージを回復させる
            if (boostTimeRemaining < boostDuration)
            {
                boostTimeRemaining += (boostDuration / boostCooldown) * Time.deltaTime;

                // 回復上限を超えないように調整
                boostTimeRemaining = Mathf.Min(boostTimeRemaining, boostDuration);
            }
        }

        // --- UIの更新 ---
        // スピードゲージを更新
        if (speedSlider != null)
        {
            speedSlider.value = currentSpeed;
        }

        // ブーストゲージを更新
        if (boostSlider != null)
        {
            boostSlider.value = boostTimeRemaining;
        }
    }


    // 他のスクリプト（RaceManager）から車の操作を有効化する用
    public void EnableControl()
    {
        canDrive = true;
        Debug.Log("車の操作を許可しました");
    }

    // 他のスクリプト（RaceManager）から操作を禁止する用
    public void DisableControl()
    {
        canDrive = false;
        currentSpeed = 0f;  // 停止

        //Rigidbodyがちゃんとあるか確認
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("Rigidbody(rb)が未設定です!!");
        }
        //スピードゲージが設定されているかチェック
        if(speedSlider != null)
        {
            speedSlider.value = 0f;
        }
        else
        {
            Debug.LogWarning("speedSlider が設定されていません！");
        }

        //ブーストもリセット
        isBoosting = false;
        boostTimeRemaining = boostDuration;
        if (boostSlider != null)
        {
            boostSlider.value = boostTimeRemaining;
        }
        else
        {
            Debug.LogWarning("boostSlider が設定されていません！");
        }
    }
}



/*
using UnityEngine.UI;

public class PlayerCarController : MonoBehaviour
{
    public float acceleration = 5f;    // �����x
    public float maxSpeed = 20f;       // �ő呬�x
    public float deceleration = 3f;    // �����x

    private float currentSpeed = 0f;   // ���݂̑��x
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���́F�X�y�[�X�L�[�ŉ���
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // �L�[�𗣂��ƌ���
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // ���x����
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // ������iY���j�Ɉړ�
        rb.linearVelocity = new Vector2(0, currentSpeed);
    }
}
*/