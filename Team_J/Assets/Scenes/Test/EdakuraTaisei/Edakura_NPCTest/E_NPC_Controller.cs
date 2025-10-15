using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    public float m_fSightAngle = 45.0f;     //�O�����E�͈�

    //NPC�̎��E����
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))     //Player�^�O��Collider�������^
        {
            Vector2 posDelta = other.transform.position - transform.position;
            float targetAngle = Vector2.Angle(transform.forward, posDelta);
            if(targetAngle < m_fSightAngle)
            {
                Debug.Log("���E�͈͓̔�&���E�̊p�x��");
            }
        }
    }
}