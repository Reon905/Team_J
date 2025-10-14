using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction DashAction;
    public float Speed = 10f;    //�v���C���[�̈ړ����x
    Rigidbody2D rbody = default;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();    //Rigidbody2D�R���|�[�l���g������Ă���
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");     //���������̓���
        float h = Input.GetAxisRaw("Horizontal");   //���������̓���
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