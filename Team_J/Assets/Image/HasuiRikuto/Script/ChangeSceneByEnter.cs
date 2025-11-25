using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneByEnter : MonoBehaviour
{
    // 次のシーン名をInspectorから指定できるようにする
    public string nextSceneName = "NextScene";

    void Update()
    {
        // Enter（Return）キーが押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 指定されたシーンに切り替え
            SceneManager.LoadScene(nextSceneName);
        }
    }
}