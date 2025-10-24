using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeEnter : MonoBehaviour
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
        if(GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
        {
            // �͈͓�����Enter�������ꂽ��V�[���ؑ�
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���͈͓��ɓ�������
        if (other.CompareTag("Player"))
        {
            if(GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
            {
                isPlayerInRange = true;
                if (enterText != null)
                {
                    enterText.text = "Enter�œ��݂ɖ߂�";
                    enterText.gameObject.SetActive(true);
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
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
}