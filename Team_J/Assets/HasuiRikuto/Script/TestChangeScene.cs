using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要

public class TestChangeScene : MonoBehaviour
{
    public string nextSceneName = ""; // 次に切り替えるシーン名

    void Update()
    {
        // スペースキーが押されたらシーン切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
