using UnityEngine;
public class E_NPC_Controller : MonoBehaviour
{
    public float m_fSightAngle = 45.0f;     //�O�����E�͈�
    Vector2 posDelta;
    float targetAngle;

    //NPC�̎��E����
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")     //Player�^�O��Collider�������^
        {
            posDelta = other.transform.position - this.transform.position;
            targetAngle = Vector2.Angle(this.transform.forward, posDelta);
            if(targetAngle < m_fSightAngle)     //targetAngle��m_SightAngle�Ɏ��܂��Ă��邩�ǂ���
            {
                Debug.Log("���E�͈͓̔�&���E�̊p�x��");
            }
        }
    }
}