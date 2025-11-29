using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ← Textを使うために必要

public class ChangeSceneIndoor1 : MonoBehaviour
{
    public string nextSceneName; // 次のシーン名
    public Text messageText;     // 「Enterで外に出る」表示用Text
    private bool isPlayerInRange = false;

    public AudioSource audioSource;
    public AudioClip enterClip;
    private bool hasPlayedSound = false;

    void Start()
    {
        // 最初は非表示
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 範囲内かつEnterキーが押されたらシーン切り替え
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            GameStateManager.instance.currentPlayerState = PlayerState.NoDetection;
            if (!hasPlayedSound && audioSource != null && enterClip != null)
            {
                audioSource.PlayOneShot(enterClip);
                DontDestroyOnLoad(audioSource.gameObject);
                hasPlayedSound = true;
            }
            SceneManager.LoadScene(nextSceneName);
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageText != null)
            {
                messageText.text = "Enterで外に出る";
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