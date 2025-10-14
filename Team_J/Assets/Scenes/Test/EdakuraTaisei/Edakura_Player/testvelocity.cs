using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�C���X�y�N�^�[�Őݒ肷��
    public float speed;

    //�v���C�x�[�g�ϐ�
    private Rigidbody2D rb = null;

    void Start()
    {
        //�R���|�[�l���g�̃C���X�^���X��߂܂���
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //�L�[���͂��ꂽ��s������
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
            
        }
        else
        {
            xSpeed = 0.0f;
        }
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
    }
    private void FixedUpdate()
    {
        
    }
}