using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Edakura_Player_Controller : MonoBehaviour
{
    public float Speed = 10.0f;     //�v���C���[�̈ړ����x
    //���̓A�N�V�������`
    public InputAction MoveAction;        //WASD�L�[����
    public InputAction DashAction;        //Shift�L�[����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;

        MoveAction.Enable();        //MoveAction��L���ɂ���
        DashAction.Enable();        //DashAction��L���ɂ���
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();     //MoveAction��Vector2�̒l���Ƃ�
        Debug.Log(move);    //�ړ������O���o��

        //Vector2�^��position��錾���A�ړ��ʁi���݂̈ʒu+MoveAction�̃x�N�g��*�ړ����x�j��position�ɓ����
        Vector2 position = (Vector2)transform.position + move * Speed * Time.deltaTime;  //Time.deltaTime�œ�������b������̒P�ʂɂ���
        //�v���C���[�̈ʒu�Ɉړ��ʂ�����
        transform.position = position;

        //�_�b�V�����̏���
        if (DashAction.IsPressed())  //DashAction�ɒl��������(Shift�L�[�������ꂽ)��
        {
            Speed = 20.0f;          //�ړ����x��2�{����
        }
        else
        {
            Speed = 10.0f;          //����ȊO�̎��͂��̂܂܂ɂ��Ă���
        }
    }
}
