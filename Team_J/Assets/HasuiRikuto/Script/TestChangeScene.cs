using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���؂�ւ��ɕK�v

public class TestChangeScene : MonoBehaviour
{
    public string nextSceneName = ""; // ���ɐ؂�ւ���V�[����

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��V�[���؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
