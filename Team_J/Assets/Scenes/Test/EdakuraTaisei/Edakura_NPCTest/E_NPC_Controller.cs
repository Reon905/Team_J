using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    public float m_fSightAngle = 45.0f;     //‘O•û‹ŠE”ÍˆÍ
    Vector2 posDelta;
    float targetAngle;

    //NPC‚Ì‹ŠE”»’è
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")     //Playerƒ^ƒO‚ÌCollider‚¾‚¯‚ª^
        {
            posDelta = other.transform.position - this.transform.position;
            targetAngle = Vector2.Angle(this.transform.forward, posDelta);
            if(targetAngle < m_fSightAngle)     //targetAngle‚ªm_SightAngle‚Éû‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
            {
                Debug.Log("‹ŠE‚Ì”ÍˆÍ“à&‹ŠE‚ÌŠp“x“à");
            }
        }
    }
}