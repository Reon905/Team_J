using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Edakura_Player_Controller : MonoBehaviour
{
    public float Speed = 10.0f;     //プレイヤーの移動速度
    //入力アクションを定義
    public InputAction MoveAction;        //WASDキー入力
    public InputAction DashAction;        //Shiftキー入力

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;

        MoveAction.Enable();        //MoveActionを有効にする
        DashAction.Enable();        //DashActionを有効にする
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();     //MoveActionのVector2の値をとる
        Debug.Log(move);    //移動時ログを出力

        //Vector2型のpositionを宣言し、移動量（現在の位置+MoveActionのベクトル*移動速度）をpositionに入れる
        Vector2 position = (Vector2)transform.position + move * Speed * Time.deltaTime;  //Time.deltaTimeで動きを一秒あたりの単位にする
        //プレイヤーの位置に移動量を入れる
        transform.position = position;

        //ダッシュ時の処理
        if (DashAction.IsPressed())  //DashActionに値が入った(Shiftキーが押された)時
        {
            Speed = 20.0f;          //移動速度を2倍する
        }
        else
        {
            Speed = 10.0f;          //それ以外の時はそのままにしておく
        }
    }
}
