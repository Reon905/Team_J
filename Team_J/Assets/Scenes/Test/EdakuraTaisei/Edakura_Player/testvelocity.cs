using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //インスペクターで設定する
    public float speed;

    //プライベート変数
    private Rigidbody2D rb = null;

    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //キー入力されたら行動する
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