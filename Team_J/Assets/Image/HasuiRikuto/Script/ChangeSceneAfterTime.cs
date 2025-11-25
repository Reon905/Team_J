using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneAfterTime : MonoBehaviour
{
    public string nextSceneName = "NextScene";
    public Text timerText;

    public static float timer = 0f;
    public float changeTime = 300f;

    // ▼ 30秒警告サウンド（ループ）
    public AudioSource audioSource;
    public AudioClip warningClip;
    private bool hasStartedLoop = false;   // 1回だけループ開始するフラグ

    void Update()
    {
        timer += Time.deltaTime;

        float remainingTime = Mathf.Max(changeTime - timer, 0f);

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (timerText != null)
        {
            timerText.text = $"残り時間 : {minutes:00}:{seconds:00}";
        }

        // ▼ 残り30秒でループ再生開始（1回だけ）
        if (!hasStartedLoop && remainingTime <= 10f)
        {
            if (audioSource != null && warningClip != null)
            {
                audioSource.clip = warningClip;
                audioSource.loop = true;      // ループON
                audioSource.Play();           // 再生開始
            }
            hasStartedLoop = true;
        }

        if (timer >= changeTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}