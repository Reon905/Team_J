using TMPro;           // TextMeshPro�̖��O���
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // �V���O���g���C���X�^���X�i�ǂ�����ł��A�N�Z�X�\�Ɂj
    public static RaceManager Instance;

    // �����\���pTextMeshProUGUI��Inspector�ŃZ�b�g
    public TextMeshProUGUI messageText;

    private float raceStartTime;   // ���[�X�J�n���Ԃ��L�^
    private bool raceOngoing = false;  // ���[�X�����ǂ����̃t���O

    void Awake()
    {
        // �V���O���g���̃Z�b�g�A�b�v
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �V�[���؂�ւ����ɔj�����Ȃ�
        }
        else
        {
            Destroy(gameObject);  // 2�ȏ゠�����玩����j��
        }

        // �ŏ��͕������\���ɂ��Ă���
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    // ��������ʂɕ\�����郁�\�b�h�iduration�b�����\���j
    public void ShowMessage(string message, float duration = 2f)
    {
        // ���łɃR���[�`���������Ă������~

        StopAllCoroutines();
        StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // �R���[�`���F������\�����Ĉ�莞�Ԍ�ɔ�\���ɂ���
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        messageText.text = message;               // ���b�Z�[�W���Z�b�g
        messageText.gameObject.SetActive(true);  // �\��ON

        yield return new WaitForSeconds(duration);  // duration�b�҂�

        messageText.gameObject.SetActive(false);    // �\��OFF
    }

    // ���[�X�J�n���̏���
    public void Start()
    {
        raceStartTime = Time.time;    // ���ݎ��Ԃ�ۑ�
        raceOngoing = true;           // ���[�X���t���OON
        ShowMessage("Ready,Go");    // �X�^�[�g���b�Z�[�W�\��
        Debug.Log("���[�X�J�n: " + raceStartTime);
    }

    // ���[�X�I�����̏���
    public void Finish()
    {
        if (!raceOngoing) return;     // ���[�X���łȂ���Ώ������Ȃ�

        float finishTime = Time.time - raceStartTime;  // �o�ߎ��Ԍv�Z
        raceOngoing = false;          // ���[�X���t���OOFF
        ShowMessage("Finish�I�I");      // �S�[�����b�Z�[�W�\��
        Debug.Log("���[�X�I���I�^�C��: " + finishTime + "�b");
    }
}
