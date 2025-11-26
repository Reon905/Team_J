using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeOnEnterUnderBank : MonoBehaviour
{
    public string nextSceneName; // 次のシーン名
    public Text messageText;     // 「Enterで外に出る」表示用Text
    private bool isPlayerInRange = false;
    private bool hasTriggered = false;

    public AudioClip enterClip;

    void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return) && !hasTriggered)
        {
            hasTriggered = true;

            // シーン切り替え
            SceneManager.LoadScene(nextSceneName);

            // シーン切り替え後に音を鳴らす
            if (SoundPlayer.instance != null && enterClip != null)
            {
                SoundPlayer.instance.PlaySE(enterClip);
                // 3秒後に消したい場合は SoundPlayer 側で対応
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageText != null)
            {
                messageText.text = "Enterで地下から出る";
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