using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        // Esc�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �Q�[�����I��
            QuitGame();
        }
    }

    void QuitGame()
    {
        // �G�f�B�^��ł͒�~
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �r���h��̎��s�t�@�C���ł̓A�v�����I��
        Application.Quit();
#endif
    }
}
