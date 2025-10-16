using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    // 視界の対象とするレイヤー（Playerや障害物など）
    // これを設定することにより、自身のコライダーに反応しなくなる
    public LayerMask m_TargetLayer; 

    public float m_fSightAngle;     //前方視界範囲
    Vector2 posDelta;   //NPCからプレイヤーへのベクトル
    public float targetAngle;
    public int time;

    private void Start()
    {
        m_fSightAngle = 45.0f;
    }

    private void Update()
    {

    }

    //NPCの視界判定
    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))     //PlayerタグのColliderだけが真
        {
            RaycastHit2D hit;   //rayが当たったコライダー判別用

            //視野の設定
            posDelta = other.transform.position - this.transform.position;  //NPCからPlayerへの方向ベクトル
            targetAngle = Vector2.Angle(this.transform.right, posDelta);    //
            //PlayerがNPCの視界に入っているか確認（障害物は無視）
            if (targetAngle < m_fSightAngle)     //targetAngleがm_SightAngleに収まっているかどうか
            {       
                //Rayを飛ばして、間に障害物がないかを判定する
                //                                      始点                  方向      プレイヤーまでの距離　 レイヤーマスク　
                if (hit = Physics2D.Raycast(this.transform.position, posDelta.normalized,posDelta.magnitude,   m_TargetLayer))
                {
                    
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("障害物なし、視界範囲内");
                    }
                    else if(hit.collider == null)
                    {
                        Debug.Log("なんもなし");
                    }
                    else
                    {
                        Debug.Log("障害物あり" + hit.collider.name);
                    }
                    
                }
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