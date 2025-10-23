﻿//RivalCarController
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
    public float minSpeed = 5f;   // ライバルの最低速度
    public float maxSpeed = 10f;  // ライバルの最高速度
    public float acceleration = 3f; // 加速力
    public float deceleration = 2f; // 減速力

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
        if (canDrive)
        {
            Debug.Log($"{gameObject.name} running at {currentSpeed}");
        }
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
    public void SetRandomSpeed()
    {
        // minSpeed〜maxSpeedの範囲でランダムな目標速度を決める
        targetSpeed = Random.Range(minSpeed, maxSpeed);

        // デバッグ出力（ゲーム中はコンソールに出る）
        Debug.Log($"{gameObject.name} の目標速度: {targetSpeed:F2}");
    }
}
