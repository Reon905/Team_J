//using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


static class Constants
{
    public const float PlayerSpeed = 8.0f;     //Playerの移動速度
}

public class E_Player_Controller : MonoBehaviour
{
    //入力アクション
    public InputAction Interact;        //インタラクト用
    public InputAction MoveAction;      //移動用
    public InputAction DashAction;      //ダッシュ用

    Rigidbody2D rbody;                               //Rigidbody2D型の変数宣言
    public float Speed = Constants.PlayerSpeed;      //Playerの移動速度
    Vector2 PlayerVector;                            //キー入力の値を格納

    void Start()
    {
        Application.targetFrameRate = 60;   //FPS制限(仮)
        MoveAction.Enable();                //移動(WASD)キー入力確認
        DashAction.Enable();                //ダッシュ(Shift)キー入力確認

        //Rigidbody2Dをとってくる
        rbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MoveActionのVector2の値を
        PlayerVector = MoveAction.ReadValue<Vector2>();

        //ダッシュ
        if (DashAction.IsPressed())              //Shiftキーが押されるとダッシュする
        {
            Speed = Constants.PlayerSpeed * (float)1.5;   //PlayerSpeedを2倍する
        }
        else   
        {
            Speed = Constants.PlayerSpeed;       //押されていない場合は元のスピードに戻す    
        }
    }
    void FixedUpdate()
    {
        rbody.linearVelocity = PlayerVector * Speed;    //キー入力からとったベクトルをSpeedと掛け算して現在の移動速度に入れる
    }
}
