using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    public float m_fSightAngle = 45.0f;     //‘O•û‹ŠE”ÍˆÍ

    //NPC‚Ì‹ŠE”»’è
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))     //Playerƒ^ƒO‚ÌCollider‚¾‚¯‚ª^
        {
            Vector2 posDelta = other.transform.position - transform.position;
            float targetAngle = Vector2.Angle(transform.forward, posDelta);
            if(targetAngle < m_fSightAngle)
            {
                Debug.Log("‹ŠE‚Ì”ÍˆÍ“à&‹ŠE‚ÌŠp“x“à");
            }
        }
    }
}