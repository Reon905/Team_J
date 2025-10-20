using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    // �X�^�[�g���S�[�����I�Ԃ��߂̗񋓌^
    public enum TriggerType { Start, Goal }
    public TriggerType triggerType;

    // �v���C���[���g���K�[�ɓ������Ƃ��̏���
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�̃^�O�����I�u�W�F�N�g��������
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Start)
            {
                // �X�^�[�g���C���ɓ������烌�[�X�J�n�������Ă�
                RaceManager.Instance.StartRace();
            }
            else if (triggerType == TriggerType.Goal)
            {
                // �S�[�����C���ɓ������烌�[�X�I���������Ă�
                RaceManager.Instance.FinishRace();
            }
        }
    }
}
