using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction DashAction;
    public float Speed = 10f;    //プレイヤーの移動速度
    Rigidbody2D rbody = default;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();    //Rigidbody2Dコンポーネントを取ってくる
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");     //水平方向の入力
        float h = Input.GetAxisRaw("Horizontal");   //垂直方向の入力
        rbody.linearVelocity = new Vector2(h, v).normalized * Speed;

        if(DashAction.IsPressed())
        {
            Speed = Speed * 2;
        }
        else
        {
            Speed = 10.0f;
        }
    }
}