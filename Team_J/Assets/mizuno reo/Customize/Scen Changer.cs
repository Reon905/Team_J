using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRaceOnEnter : MonoBehaviour
{
    [SerializeField] private string raceSceneName = "Race scene"; // �� ���Ȃ��̃��[�X�V�[�����ɕύX�I

    void Update()
    {
        // Enter�L�[�ŃV�[���؂�ւ�
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Race scene");
        }
    }
}
