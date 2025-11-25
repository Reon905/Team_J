using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneAfterTime : MonoBehaviour
{
    // 次のシーン名
    public string nextSceneName = "NextScene";

    // 残り時間を表示するUIテキスト
    public Text timerText;

    // 経過時間をstaticにして保持
    public static float timer = 0f;

    // シーン切り替えまでの時間（5分＝300秒）
    public float changeTime = 300f;

    void Update()
    {
        // 経過時間を加算
        timer += Time.deltaTime;

        // 残り時間を計算
        float remainingTime = Mathf.Max(changeTime - timer, 0f);

        // 分・秒に変換
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // テキスト更新
        if (timerText != null)
        {
            timerText.text = $"残り時間 : {minutes:00}:{seconds:00}";
        }

        // 時間が来たらシーン切り替え
        if (timer >= changeTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}