//PlaiyerCarController 
//using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCarController : MonoBehaviour
{
    // ▼ 通常走行パラメータ
    public static float acceleration;      // 通常加速度
    public static float maxSpeed;         // 通常最大速度
    public float deceleration = 5f;      // 減速度

    private float currentSpeed = 0f;     // 現在の速度
    private Rigidbody2D rb;              // Rigidbody2D コンポーネント

    public Slider speedSlider;             // スピードゲージUI
    public bool canDrive = false;          // レース開始フラグ
    public AudioSource CarSound;           //車の通常サウンド用AudioSource
    public AudioClip CarSoundClip;         //加速時の音
    public AudioClip CarIdling;            //アイドリング(減速)時の音
    private bool isPlaying = false;        //ブースト用AudioSource
    private bool isAccelerating = false;   //ブースト音
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
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        //CarSound = this.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        PlayIdlingSound(); //ゲーム開始時に呼び出し

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
        //Enterキーが押されていて、かつブーストゲージが残っている場合に再生
        bool isBoostKeyPressed = Input.GetKey(KeyCode.Return);

        // ブースト条件：キー押下中＆ゲージが残っている

        if (isBoostKeyPressed && boostTimeRemaining > 0f)
        {
            if (!isBoosting)
            {
                StartBoostSound();//ブースト音を再生開始
            }
            isBoosting = true;
        }
        else
        {
            //ブースト解除時に音を止める
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
        //Wを押したときに走行音へ切り替え
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayDriveSound();//加速音を再生
            isAccelerating = true;
        }
        //Wキーを離したときにアイドリング音へ戻す
        else if (Input.GetKeyUp(KeyCode.W))
        {
            PlayIdlingSound();//アイドリング音を再生
            isAccelerating = false;
        }

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
    //走行音の再生処理
    private void PlayDriveSound()
    {
        if (CarSound.clip == CarSoundClip && CarSound.isPlaying) return;
        CarSound.loop = true;           //ループ再生ON
        CarSound.clip = CarSoundClip;   //再生する音を設定
        CarSound.Play();                //再生開始
    }
    //アイドリング音(減速時)の再生処理
    private void PlayIdlingSound()
    {
        if (CarSound.clip == CarIdling && CarSound.isPlaying) return;
        CarSound.loop = true;          //繰り返し再生ON
        CarSound.clip = CarIdling;     //再生する音を「アイドリング音」に設定
        CarSound.Play();               //再生開始
    }
    //ブースト音を流す処理
    private void StartBoostSound()
    {
        //ブースト音が設定されていない or すでに再生中ならスキップ
        if (boostClip == null || boostAudio.isPlaying) return;
        boostAudio.clip = boostClip;   //再生する音を設定
        boostAudio.volume = 1f;        //音量設定(最大)
        boostAudio.loop = true;        //ループ再生ON
        boostAudio.Play();             //再生開始
    }
    //ブースト音を止める処理
    private void StopBoostSound()
    {
        //再生中なら停止する
        if (boostAudio.isPlaying)
            boostAudio.Stop();
    }
    public void StopAllCarSounds()
    {
        if (CarSound != null && CarSound.isPlaying)
            CarSound.Stop();

        if(boostAudio != null && boostAudio.isPlaying)
            boostAudio.Stop();
    }
}