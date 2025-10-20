using UnityEngine;

public static class NPC_Constants
{
    public static float DEFAULT_SIGHT_ANGLE = 30.0f;    //NPCの視界範囲
    public static float DEFAULT_DETECTION_VALUE = 0.0f; //初期発覚値
    public static float MAX_DETECTION_VALUE = 3.5f;     //最大発覚値
}
public class E_NPC_Controller : MonoBehaviour
{
    // 視界の対象とするレイヤー（Playerや障害物など）
    public LayerMask m_TargetLayer; // これを設定することにより、自身(NPC)のコライダーに反応しなくなる

    public   float m_fSightAngle;     // 前方視界範囲
    public   float Detection_Value;   // 発覚値(視界内に入ると上昇)
    private  bool m_IsDetection;
    Vector2  posDelta;              // NPCからプレイヤーへのベクトル
    private  float TargetAngle;    //
    private  float TimeElapsed;    // 経過時間
    private  float TimeOut;        //

    Transform playerTr; // プレイヤーのTransform
    [SerializeField] float NPC_Speed = 2.0f; // 敵の動くスピード

    private void Start()
    {
        // プレイヤーのTransformを取得（プレイヤーのタグをPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        m_fSightAngle = NPC_Constants.DEFAULT_SIGHT_ANGLE;
        Detection_Value = NPC_Constants.DEFAULT_DETECTION_VALUE;
        TimeOut = 0.02f;
    }

    private void Update()
    {
        TimeElapsed += Time.deltaTime;


        //プレイヤーへの移動
        if (m_IsDetection == true)
        {
            // プレイヤーとの距離が0.1f未満になったらそれ以上実行しない
            if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            {
                m_IsDetection = false;
                return;
            }

            // プレイヤーに向けて進む
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(playerTr.position.x, playerTr.position.y),
                NPC_Speed * Time.deltaTime);
        }
    }

    //NPCの視界判定
    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))     //PlayerタグのColliderだけが真
        {
            
            if (TimeElapsed >= TimeOut)
            {


                RaycastHit2D hit;   //rayが当たったコライダー判別用

                //視野の設定
                posDelta = other.transform.position - this.transform.position;  //NPCからPlayerへの方向ベクトル
                TargetAngle = Vector2.Angle(this.transform.right, posDelta);    //NPCからPlayerの角度
                                                                                //PlayerがNPCの視界に入っているか確認（障害物は無視）
                if (TargetAngle < m_fSightAngle)     //targetAngleがm_SightAngleに収まっているかどうか
                {
                    //Rayを飛ばして、間に障害物がないかを判定する
                    //                                      始点                 方向        プレイヤーまでの距離　  感知レイヤー　
                    if (hit = Physics2D.Raycast(this.transform.position, posDelta.normalized, posDelta.magnitude,   m_TargetLayer))
                    {
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            Detection_Value += 0.1f;
                            Debug.Log("発覚値上昇");
                            if (Detection_Value > NPC_Constants.MAX_DETECTION_VALUE)
                            {
                                Detection_Value = 0.0f;

                                m_IsDetection = true;

                                Debug.Log("障害物なし、視界範囲内");
                            }
                        }
                        else if (hit.collider == null)
                        {
                            Debug.Log("なんもなし");
                        }
                        else
                        {
                            Debug.Log("障害物あり" + hit.collider.name);
                        }
                    }
                }
                TimeElapsed = 0.0f;
            }
            
        } 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (TargetAngle < m_fSightAngle)
            {
                Detection_Value = 0.0f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手のタグが "Player" だったら
        if (collision.gameObject.CompareTag("Player"))
        {
            // 接触した時の処理
            Debug.Log("Playerと接触");
        }
    }
}