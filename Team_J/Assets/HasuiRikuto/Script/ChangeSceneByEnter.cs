using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneByEnter : MonoBehaviour
{
    // ���̃V�[������Inspector����w��ł���悤�ɂ���
    public string nextSceneName = "NextScene";

    void Update()
    {
        // Enter�iReturn�j�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // �w�肳�ꂽ�V�[���ɐ؂�ւ�
            SceneManager.LoadScene(nextSceneName);
        }
    }
}