//using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;



public class E_Player_Controller : MonoBehaviour
{
    //入力アクション
    public InputAction Interact;        //インタラクト用
    public InputAction MoveAction;      //移動用
    public InputAction DashAction;      //ダッシュ用

    Rigidbody2D rbody;              //Rigidbody2D型の変数宣言
    public float Speed = 5.0f;      //Playerの移動速度
    public int WalkDelay = 1;       //歩行音用
    private bool FirstWalk = false;

    public Vector2 PlayerVector;    //キー入力の値を格納

    AudioSource WalkAudio;          //
    public AudioClip WalkAudioClip; //

    //アニメーション用
    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMoveAnimation";
    public string nowAnime = "";
    public string oldAnime = "";

    void Start()
    {
        Application.targetFrameRate = 60;   //FPS制限(仮)
        MoveAction.Enable();                //移動(WASD)キー入力確認
        DashAction.Enable();                //ダッシュ(Shift)キー入力確認

        //Rigidbody2Dをとってくる
        rbody = this.GetComponent<Rigidbody2D>();
        WalkAudio = this.GetComponent<AudioSource>();

        animator = this.GetComponent<Animator>();
        nowAnime = stopAnime;       //停止から開始
        oldAnime = stopAnime;       //停止から開始
    }

    void Update()
    {
        //MoveActionのVector2の値を
        PlayerVector = MoveAction.ReadValue<Vector2>();

        //ダッシュ
        if (DashAction.IsPressed())      //Shiftキーが押されるとダッシュする
        {
            Speed = 5.0f * 1.5f;   //PlayerSpeedを1.5倍する
            if (PlayerVector.x != 0.0f || PlayerVector.y != 0.0f)
            {
                WalkDelay++;
            }
        }
        else   
        {
            Speed = 5.0f;       //押されていない場合は元のスピードに戻す    
        }


        //向きの調整
        if (PlayerVector.x > 0.0f)
        {
            //Debug.Log("右移動");
            transform.rotation = Quaternion.Euler(0, 0, 90);
            WalkDelay++;
        }
        else if (PlayerVector.x < 0.0f)
        {
            //Debug.Log("左移動");
            transform.rotation = Quaternion.Euler(0, 0, -90);
            WalkDelay++;
        }
        else if (PlayerVector.y > 0.0f)
        {
            //Debug.Log("上移動");
            transform.rotation = Quaternion.Euler(0, 0, -180);
            WalkDelay++;
        }
        else if (PlayerVector.y < 0.0f)
        {
            //Debug.Log("下移動");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            WalkDelay++;
        }
        if (WalkDelay > 30)
        {
            //Debug.Log("歩行音再生");
            WalkAudio.PlayOneShot(WalkAudioClip);
            WalkDelay = 0;
        }
}
    void FixedUpdate()
    {
        //アニメーション更新
        if (PlayerVector.x == 0.0f && PlayerVector.y == 0.0f )
        {
            nowAnime = stopAnime;
        }
        else
        {
            nowAnime = moveAnime;
        }

        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
            rbody.linearVelocity = PlayerVector * Speed;    //キー入力からとったベクトルをSpeedと掛け算して現在の移動速度に入れる
    }
}
