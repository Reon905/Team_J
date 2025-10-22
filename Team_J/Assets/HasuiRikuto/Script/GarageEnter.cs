using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GarageEnter : MonoBehaviour
{
    public Text enterText; // Canvas���Text��Inspector�Ńh���b�O
    public string nextSceneName = "NextScene"; // ���ɍs���V�[����
    private bool isPlayerInRange = false;

    void Start()
    {
        // �ŏ��͔�\��
        if (enterText != null)
            enterText.gameObject.SetActive(false);
    }

    void Update()
    {
        // �͈͓�����Enter�������ꂽ��V�[���ؑ�
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���͈͓��ɓ�������
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (enterText != null)
            {
                enterText.text = "Enter�ŃK���[�W�ɓ���";
                enterText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �͈͊O�ɏo�����\��
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (enterText != null)
                enterText.gameObject.SetActive(false);
        }
    }
}