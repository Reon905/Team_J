//using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.InputSystem;



public class E_Player_Controller : MonoBehaviour
{

    //���̓A�N�V����
    public InputAction Interact;        //�C���^���N�g�p
    public InputAction MoveAction;      //�ړ��p
    public InputAction DashAction;      //�_�b�V���p

    Rigidbody2D rbody;                               //Rigidbody2D�^�̕ϐ��錾
    public float Speed = 6.0f;                       //Player�̈ړ����x

    Vector2 PlayerVector;                            //�L�[���͂̒l���i�[

    void Start()
    {
        Debug.Log("����������");

        Application.targetFrameRate = 60;   //FPS����(��)
        MoveAction.Enable();                //�ړ�(WASD)�L�[���͊m�F
        DashAction.Enable();                //�_�b�V��(Shift)�L�[���͊m�F

        //Rigidbody2D���Ƃ��Ă���
        rbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MoveAction��Vector2�̒l��
        PlayerVector = MoveAction.ReadValue<Vector2>();

        //�_�b�V��
        if (DashAction.IsPressed())              //Shift�L�[���������ƃ_�b�V������
        {
            Speed = 6.0f * (float)1.5;   //PlayerSpeed��1.5�{����
        }
        else   
        {
            Speed = 6.0f;       //������Ă��Ȃ��ꍇ�͌��̃X�s�[�h�ɖ߂�    
        }

        

        //�����̒���
        if (PlayerVector.x > 0.0f)
        {
            //Debug.Log("�E�ړ�");
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (PlayerVector.x < 0.0f)
        {
            //Debug.Log("���ړ�");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (PlayerVector.y > 0.0f)
        {
            //Debug.Log("��ړ�");
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (PlayerVector.y < 0.0f)
        {
            //Debug.Log("���ړ�");
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }


    }
    void FixedUpdate()
    {
        rbody.linearVelocity = PlayerVector * Speed;    //�L�[���͂���Ƃ����x�N�g����Speed�Ɗ|���Z���Č��݂̈ړ����x�ɓ����
    }
}
