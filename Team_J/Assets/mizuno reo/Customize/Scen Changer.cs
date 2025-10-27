using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToRaceOnEnter : MonoBehaviour
{
    [SerializeField] private string raceSceneName = "Race scene"; // ← あなたのレースシーン名に変更！

    void Update()
    {
        // Enterキーでシーン切り替え
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Race scene");
        }
    }
}
