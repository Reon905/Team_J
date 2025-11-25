using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GarageEnter : MonoBehaviour
{
    public Text enterText; // Canvas上のTextをInspectorでドラッグ
    public string nextSceneName = "NextScene"; // 次に行くシーン名
    private bool isPlayerInRange = false;

    void Start()
    {
        // 最初は非表示
        if (enterText != null)
            enterText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 範囲内かつEnterが押されたらシーン切替
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            // ※必要ならここにSoundPlayerやGameStateManagerを戻してください
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが範囲内に入ったら
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (enterText != null)
            {
                enterText.text = "Enterでガレージに入る";
                enterText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 範囲外に出たら非表示
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (enterText != null)
                enterText.gameObject.SetActive(false);
        }
    }
}