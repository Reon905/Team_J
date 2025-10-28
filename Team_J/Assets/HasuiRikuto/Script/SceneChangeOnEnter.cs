using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // �� Text���g�����߂ɕK�v

public class SceneChangeOnEnter : MonoBehaviour
{
    public string nextSceneName; // ���̃V�[����
    public Text messageText;     // �uEnter�ŊO�ɏo��v�\���pText
    private bool isPlayerInRange = false;

    void Start()
    {
        // �ŏ��͔�\��
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        // �͈͓�����Enter�L�[�������ꂽ��V�[���؂�ւ�
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageText != null)
            {
                messageText.text = "Enter�ŃJ�X�^����ʂ�";
                messageText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }
    }
}