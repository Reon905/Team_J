//using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


static class Constants
{
    public const float PlayerSpeed = 8.0f;     //Player�̈ړ����x
}

public class E_Player_Controller : MonoBehaviour
{
    //���̓A�N�V����
    public InputAction Interact;        //�C���^���N�g�p
    public InputAction MoveAction;      //�ړ��p
    public InputAction DashAction;      //�_�b�V���p

    Rigidbody2D rbody;                               //Rigidbody2D�^�̕ϐ��錾
    public float Speed = Constants.PlayerSpeed;      //Player�̈ړ����x
    Vector2 PlayerVector;                            //�L�[���͂̒l���i�[

    void Start()
    {
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
            Speed = Constants.PlayerSpeed * (float)1.5;   //PlayerSpeed��2�{����
        }
        else   
        {
            Speed = Constants.PlayerSpeed;       //������Ă��Ȃ��ꍇ�͌��̃X�s�[�h�ɖ߂�    
        }
    }
    void FixedUpdate()
    {
        rbody.linearVelocity = PlayerVector * Speed;    //�L�[���͂���Ƃ����x�N�g����Speed�Ɗ|���Z���Č��݂̈ړ����x�ɓ����
    }
}
