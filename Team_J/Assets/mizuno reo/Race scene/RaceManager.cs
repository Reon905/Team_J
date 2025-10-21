//ReceManager.cs
using TMPro;         // TextMeshPro �p�i���b�Z�[�W�\���j
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;  // �V���O���g���F���̃X�N���v�g����A�N�Z�X���₷������

    public TextMeshProUGUI messageText;      // �J�E���g�_�E����S�[���Ȃǂ̕\���e�L�X�g�i�C���X�y�N�^�[�Őݒ�j
    public PlayerCarController playerCar;    // ����Ώۂ̎ԁi�C���X�y�N�^�[�Őݒ�j

    private float raceStartTime;             // ���[�X�J�n���ԁi�^�C�}�[�Ƃ��Ďg�p�j
    private bool raceOngoing = false;        // ���[�X�����ǂ���

    private Coroutine messageCoroutine;      // ���b�Z�[�W�\���R���[�`�����Ǘ����邽�߂̕ϐ�

    void Awake()
    {
        // �V���O���g���̏�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �V�[�����܂����ł��c��
        }
        else
        {
            Destroy(gameObject);  // 2�ȏ゠������Â��ق�������
        }

        // �ŏ��̓��b�Z�[�W��\��
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Start()
    {
        // �Q�[���J�n���ɃJ�E���g�_�E�����n�߂�
        StartCoroutine(RaceStartCoroutine());
    }

    public void StartRace()
    {
        StartCoroutine(RaceStartCoroutine());
    }
    // �J�E���g�_�E�����Ă��烌�[�X�J�n���鏈��
    private System.Collections.IEnumerator RaceStartCoroutine()
    {
        // �Ԃ̑���𖳌����i�~�߂�j
        if (playerCar != null)
            playerCar.DisableControl();

        // �J�E���g�_�E���\���i1�b���j
        string[] countdownTexts = { "3", "2", "1" };

        foreach (string t in countdownTexts)
        {
            ShowMessage(t, 1f);
            yield return new WaitForSeconds(1f);
        }

        // GO�I�\��
        ShowMessage("GO!", 1f);

        // �Ԃ̑����L�����i�����ł���ƎԂ�������j
        if (playerCar != null)
        {
            playerCar.EnableControl();
        }
        else
        {
            Debug.LogError("playerCar �� RaceManager �ɃZ�b�g����Ă��܂���I");
        }

        // �^�C�}�[�J�n
        raceStartTime = Time.time;
        raceOngoing = true;
    }

    // �S�[�����ɌĂяo���i�g���K�[����j
    public void Finish()
    {
        if (!raceOngoing) return;

        float finishTime = Time.time - raceStartTime;
        raceOngoing = false;

        //�Ԃ��~�߂�(����𖳌���)
        if(playerCar != null)
        {
            playerCar.DisableControl(); //���͂��~�߂�
            StartCoroutine(SmoothStop(playerCar)); //�������~�߂�悤�ɂ���
        }
       
        ShowMessage($"Finish!!\nTime: {finishTime:F2} �b", 3f); //���b�Z�[�W�\��
        Debug.Log("���[�X�I���I�^�C��: " + finishTime + "�b");
    }

    private System.Collections.IEnumerator SmoothStop(PlayerCarController car)
    {
        Rigidbody2D rb = car.GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        while(rb.linearVelocity.magnitude > 0.1f)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity,Vector2.zero, 1f * Time.deltaTime);
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // ���b�Z�[�W����ʂɕ\������i���b�ԁj
    public void ShowMessage(string message, float duration = 2f)
    {
        // �O�̃��b�Z�[�W���c���Ă���~�߂�
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // ���b�Z�[�W�\���R���[�`���i���Ԍo�ߌ�ɔ�\���ɂ���j
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        if (messageText == null)
        {
            Debug.LogWarning("messageText ���ݒ肳��Ă��܂���I");
            yield break;
        }

        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }
}
