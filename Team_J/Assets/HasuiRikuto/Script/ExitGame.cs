using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        // Escキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ゲームを終了
            QuitGame();
        }
    }

    void QuitGame()
    {
        // エディタ上では停止
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後の実行ファイルではアプリを終了
        Application.Quit();
#endif
    }
}
