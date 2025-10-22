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

    public Slider boostSlidet;             //ブーストゲージ用スライダーUI
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
        if (boostSlidet != null)
        {
            boostSlidet.minValue = 0f;
            boostSlidet.minValue = boostDuration;
            boostTimeRemaining = boostDuration;   //最初は満タン
            boostSlidet.value = boostTimeRemaining;
        }
    }
    
    void Update()
    {
        // デバッグ表示：現在の canDrive の状態をログ出力
        Debug.Log("canDrive = " + canDrive);

        // canDrive が false のときは一切操作できない（レース前など）
        if (!canDrive)
        {
            rb.linearVelocity = Vector2.zero;  // 車を止める
            return;                      // ここで処理終了
        }

        //ブースト発動処理
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isBoosting = true;
        }

        // Wキーを押している間は加速
        if (Input.GetKey(KeyCode.W))
        {
            //ブースト中は加速度アップ
            float appliedAccleration = isBoosting ? acceleration * boostMultiplier : acceleration;
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Wキーを離したら減速
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // currentSpeed を 0～maxSpeed に制限（負の速度やオーバーを防ぐ）
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Rigidbody2D で移動（Y軸方向に currentSpeed だけ移動）
        rb.linearVelocity = new Vector2(0, currentSpeed);

        // スピードゲージ（スライダー）を更新
        if (speedSlider != null)
        {
            speedSlider.value = currentSpeed;
        }

        //ブーストゲージの処理
        if (isBoosting )
        {
            //ブースト中は時間を減らしていく
            boostTimeRemaining -= Time.deltaTime;

            if (boostTimeRemaining <=0f)
            {
                boostTimeRemaining = 0f;
                isBoosting  = false;  //ブースト終了
            }
        }
        else
        {
            //ブースト未使用中はゲージ回復
            if (boostTimeRemaining < boostDuration)
            {
                boostTimeRemaining += (boostDuration / boostCooldown) * Time.deltaTime;
                boostTimeRemaining = Mathf.Min(boostTimeRemaining, boostDuration);
            }
        }

        //ブーストゲージの更新
        if(boostSlidet != null)
        {

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