//PlaiyerCarController 
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCarController : MonoBehaviour
{
    // ▼ 通常走行パラメータ
    public static float acceleration;      // 通常加速度
    public static float maxSpeed;         // 通常最大速度
    public float deceleration = 3f;      // 減速度

    private float currentSpeed = 0f;     // 現在の速度
    private Rigidbody2D rb;              // Rigidbody2D コンポーネント

    public Slider speedSlider;           // スピードゲージUI
    public bool canDrive = false;        // レース開始フラグ
    public AudioSource CarSound;          // Sound関数
    public AudioClip CarSoundClip;       //
    public AudioClip CarIdling;
    private bool isPlaying = false;
    private bool isAccelerating = false;
    public AudioSource boostAudio;
    public AudioClip boostClip;

    // ▼ ブースト関連パラメータ
    [Header("Boost Settings")]
    public float boostAccelerationMultiplier = 2f; // ブースト中の加速度倍率
    public float boostSpeedLimitMultiplier = 1.5f; // ブースト中の最高速倍率
    public float boostDuration = 2f;               // ブーストゲージ最大値
    public float boostCooldown = 5f;               // ブーストゲージが満タンに戻るまでの時間

    public Slider boostSlider;             // ブーストゲージUI
    private float boostTimeRemaining = 0f; // 残りブースト時間
    private bool isBoosting = false;       // ブースト中フラグ

    private void Start()
    {
        //CarSound = this.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        PlayIdlingSound();

        // --- Customize で設定された値を反映 ---
        acceleration = Customize.selectedAcceleration;
        maxSpeed = Customize.selectedMaxSpeed;

        Debug.Log($"PlayerCarController initialized → Accel:{acceleration}, MaxSpeed:{maxSpeed}");

        // --- UI 初期化 ---
        if (speedSlider)
        {
            speedSlider.minValue = 0f;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = 0f;
        }

        if (boostSlider)
        {
            boostSlider.minValue = 0f;
            boostSlider.maxValue = boostDuration;
            boostTimeRemaining = boostDuration;
            boostSlider.value = boostTimeRemaining;
        }
    }


    void Update()
    {
        Debug.Log("canDrive:" + canDrive); //デバッグ用
        // --- 車の操作が有効でない場合は停止 ---
        if (!canDrive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // --- ブーストキーの入力（Enterキー長押し） ---
        bool isBoostKeyPressed = Input.GetKey(KeyCode.Return);

        // ブースト条件：キー押下中＆ゲージが残っている
        if (isBoostKeyPressed && boostTimeRemaining > 0f)
        {
            if (!isBoosting)
            {
                StartBoostSound();
            }
            isBoosting = true;
        }
        else
        {
            if(isBoosting)
            {
                StopBoostSound();
            }
            isBoosting = false;
        }

        // --- 加速・減速処理 ---
        // ブースト中は加速度を boostAccelerationMultiplier 倍に
        float appliedAcceleration = isBoosting
            ? acceleration * boostAccelerationMultiplier
            : acceleration;

        // 加速・減速の入力処理
        if (Input.GetKey(KeyCode.W))
        { 
            currentSpeed += appliedAcceleration * Time.deltaTime; // 加速
        }
        else
        {
              currentSpeed -= deceleration * Time.deltaTime;        // 減速
        }

        // Wキー押下・離すの検出
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayDriveSound();
            isAccelerating = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            PlayIdlingSound();
            isAccelerating = false;
        }
        //else if (Input.GetKey(KeyCode.W) && IsIdling == true)
        //{
        //    CarSound.Stop();
        //    IsIdling = false;
        //}
        //else if (IsSoundStop == true)
        //{

        //    CarSound.PlayOneShot(CarIdling);
        //    IsIdling = true;
        //}
        //else
        //{
        //    CarSound.Stop();
        //    IsSoundStop = true;
        //}

        // --- 最高速度制限 ---
        // ブースト中は maxSpeed × boostSpeedLimitMultiplier に上限アップ
        float currentMaxSpeed = isBoosting
            ? maxSpeed * boostSpeedLimitMultiplier
            : maxSpeed;

        // currentSpeedを0～上限まで制限
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, currentMaxSpeed);

        // --- 実際の移動処理 ---
        rb.linearVelocity = new Vector2(0, currentSpeed);

        // --- ブーストゲージ処理 ---
        if (isBoosting)
        {
            // 使用中はゲージを減らす
            boostTimeRemaining -= Time.deltaTime;

            if (boostTimeRemaining <= 0f)
            {

                boostTimeRemaining = 0f;
           //     isBoosting = false;
            }
        }
        else
        {
            // 未使用時はゲージを回復させる
            if (boostTimeRemaining < boostDuration)
            {
                boostTimeRemaining += (boostDuration / boostCooldown) * Time.deltaTime;

                // 最大値を超えないように制限
                boostTimeRemaining = Mathf.Min(boostTimeRemaining, boostDuration);
            }
        }

        // --- UI更新 ---
        if (speedSlider != null)
        {
            speedSlider.value = currentSpeed;
        }
        if (boostSlider != null)
        {
            boostSlider.value = boostTimeRemaining;
        }
    }

    // --- 他スクリプトから呼び出す用（レース開始など） ---
    public void EnableControl()
    {
        canDrive = true;
        Debug.Log("車の操作を許可しました（canDrive = " + canDrive + ")");

    }

    // --- 操作無効化（レース前など） ---
    public void DisableControl()
    { 
        canDrive = false;
        Debug.Log("車の操作を禁止しました");
        currentSpeed = 0f;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }

        if (speedSlider != null)
            speedSlider.value = 0f;

        // ブーストもリセット
        isBoosting = false;
        boostTimeRemaining = boostDuration;

        if (boostSlider != null)
            boostSlider.value = boostTimeRemaining;
    }
    private void PlayDriveSound()
    {
        if (CarSound.clip == CarSoundClip && CarSound.isPlaying) return;
        CarSound.loop = true;
        CarSound.clip = CarSoundClip;
        CarSound.Play();
    }

    private void PlayIdlingSound()
    {
        if (CarSound.clip == CarIdling && CarSound.isPlaying) return;
        CarSound.loop = true;
        CarSound.clip = CarIdling;
        CarSound.Play();
    }

    private void StartBoostSound()
    {
        if (boostClip == null || boostAudio.isPlaying) return;
        boostAudio.clip = boostClip;
        boostAudio.volume = 1f;
        boostAudio.loop = true;
        boostAudio.Play();
    }

    private void StopBoostSound()
    {
        if (boostAudio.isPlaying)
            boostAudio.Stop();
    }

}


