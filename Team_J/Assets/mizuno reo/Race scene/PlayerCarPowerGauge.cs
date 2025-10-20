using UnityEngine;
using UnityEngine.UI; // UI操作に必要な名前空間

public class PlayerCarPowerGauge : MonoBehaviour
{
    // 車の加速度（キー押し時に速度が増える速さ）
    public float acceleration = 5f;

    // 車の最大速度（ゲージの最大値にも使う）
    public float maxSpeed = 20f;

    // 減速度（キーを離した時に速度が減る速さ）
    public float deceleration = 3f;

    // 現在の速度（内部計算用）
    private float currentSpeed = 0f;

    // Rigidbody2Dコンポーネント（物理挙動制御に使用）
    private Rigidbody2D rb;

    // 速度ゲージ（UIのSliderをInspectorで接続する）
    public Slider speedSlider;

    void Start()
    {
        // Rigidbody2Dを取得（アタッチされているコンポーネントを参照）
        rb = GetComponent<Rigidbody2D>();

        // スライダーがセットされているか確認して初期設定
        if (speedSlider != null)
        {
            speedSlider.minValue = 0f;       // スライダーの最小値を0に設定
            speedSlider.maxValue = maxSpeed; // スライダーの最大値をmaxSpeedに設定
        }
    }

    void Update()
    {
        // スペースキーを押している間は加速する処理
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            // 加速度をフレーム時間で掛けて速度を増加
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // スペースキーを離したら減速する処理
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // 速度が0未満やmaxSpeed以上にならないように制限をかける
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Rigidbody2Dの速度を更新（Y軸方向にcurrentSpeedだけ動かす）
        rb.linearVelocity = new Vector2(0, currentSpeed);

        // スライダーが存在すれば値を更新してゲージを動かす
        if (speedSlider != null)
        {
            speedSlider.value = currentSpeed;
        }
    }
}

