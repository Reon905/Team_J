using TMPro;           // TextMeshPro���g�����߂ɕK�v
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;  // �V���O���g���C���X�^���X

    // UI�\���p��TextMeshProUGUI��Inspector�ŃZ�b�g
    public TextMeshProUGUI messageText;

    private float raceStartTime;          // ���[�X�J�n���Ԃ��L�^����ϐ�
    private bool raceOngoing = false;     // ���[�X�����ǂ����̃t���O

    // Awake�̓I�u�W�F�N�g�������ɌĂ΂�郁�\�b�h
    void Awake()
    {
        // �V���O���g���̃Z�b�g�A�b�v
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���؂�ւ����ɔj�����Ȃ�
        }
        else
        {
            Destroy(gameObject);           // ���ł�Instance������Ύ����͔j��
        }

        // �ŏ��̓��b�Z�[�W��\���ɂ��Ă���
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    // ��ʂɕ�����\������֐��iduration�b�����\���j
    public void ShowMessage(string message, float duration = 2f)
    {
        // ���łɕ\�����̃��b�Z�[�W������Έ�U�~�߂�
        StopAllCoroutines();
        StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // �w��b������������\�����A���̌��\���ɂ���R���[�`��
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        messageText.text = message;               // �\�����镶�����Z�b�g
        messageText.gameObject.SetActive(true);  // �e�L�X�g����ʂɕ\��

        yield return new WaitForSeconds(duration); // �w��b���҂�

        messageText.gameObject.SetActive(false); // �e�L�X�g���\���ɂ���
    }

    // ���[�X�J�n����
    public void StartRace()
    {
        raceStartTime = Time.time;    // ���ݎ��Ԃ��L�^�i���[�X�J�n���ԁj
        raceOngoing = true;           // ���[�X���t���O�𗧂Ă�
        ShowMessage("�X�^�[�g�I");    // �X�^�[�g���b�Z�[�W��\��
        Debug.Log("���[�X�J�n: " + raceStartTime);
    }

    // ���[�X�I������
    public void FinishRace()
    {
        if (!raceOngoing) return;     // ���[�X���łȂ���Ή������Ȃ�

        float finishTime = Time.time - raceStartTime; // �o�ߎ��Ԃ��v�Z
        raceOngoing = false;          // ���[�X�I���t���O�𗧂Ă�
        ShowMessage("�S�[���I");      // �S�[�����b�Z�[�W��\��
        Debug.Log("���[�X�I���I�^�C��: " + finishTime + "�b");
    }
}
