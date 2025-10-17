using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;     // �Ǐ]����Ώہi�v���C���[�j
    public Vector3 offset;       // �J�����̈ʒu�̂���i�K�v�ɉ����Ē����j

    void LateUpdate()
    {
        if (target != null)
        {
            // Z���̓J�����̐[���Ȃ̂ŌŒ�i-10�Ƃ��j
            Vector3 newPosition = target.position + offset;
            newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
    }
}
