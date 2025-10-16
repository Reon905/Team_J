using Unity.VisualScripting;
using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    public float m_fSightAngle = 45.0f;     //前方視界範囲
    Vector2 posDelta;
    public float targetAngle;

    //NPCの視界判定
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")     //PlayerタグのColliderだけが真
        {
            posDelta = other.transform.position - this.transform.position;
            targetAngle = Vector2.Angle(this.transform.right, posDelta);
            //PlayerがNPCの視界に入っているか確認（障害物は無視）
            if(targetAngle < m_fSightAngle)     //targetAngleがm_SightAngleに収まっているかどうか
            {
                Debug.Log("範囲内＆視界範囲内");
                //Rayを飛ばして、間に障害物がないかを判定する

            }
        }
    }
}