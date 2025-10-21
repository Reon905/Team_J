using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    // �X�^�[�g���S�[�����I�Ԃ��߂̗񋓌^
    public enum TriggerType { Start, Goal }
    public TriggerType triggerType;

    // �v���C���[���g���K�[�ɓ��������ɌĂ΂��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[�̃^�O�����I�u�W�F�N�g��������
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Start)
            {
                RaceManager.Instance.StartRace();   // �X�^�[�g�����Ăяo��
            }
            else if (triggerType == TriggerType.Goal)
            {
                RaceManager.Instance.Finish();  // �S�[�������Ăяo��
            }
        }
    }
}
