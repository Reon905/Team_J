using UnityEngine;
using UnityEngine.UI;  // UI操作のために必要

public class PlayerCarController : MonoBehaviour
{
    // 加速度（スペースキー押しで増える速さ）
    public float acceleration = 5f;

    // 最大速度（ゲージの最大値としても使用）
    public float maxSpeed = 20f;

    // 減速度（スペースキー離した時に減る速さ）
    public float deceleration = 3f;

    // 現在の速度
    private float currentSpeed = 0f;

    // Rigidbody2Dコンポーネントの参照（物理制御用）
    private Rigidbody2D rb;

    // スピードゲージとして使うUIのSlider（インスペクターでセットする）
    public Slider speedSlider;

    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        // スライダーがセットされていれば初期設定をする
        if (speedSlider != null)
        {
            speedSlider.minValue = 0f;        // 最小値は0に設定
            speedSlider.maxValue = maxSpeed;  // 最大値は車の最大速度に設定
        }
    }

    void Update()
    {
        // エンターキーを押している間は加速
        if (Input.GetKey(KeyCode.W))  //キー入力
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // キーを離すと減速
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // 速度を0からmaxSpeedの範囲に制限
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Rigidbody2Dの速度を更新（Y軸方向にcurrentSpeedだけ動かす）
        rb.linearVelocity = new Vector2(0, currentSpeed);

        // スライダーの値を現在の速度に合わせて更新
        if (speedSlider != null)
        {
            speedSlider.value = currentSpeed;
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