using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // シーンを切り替えるメソッド
    public void ChangeScene(string Racescene)
    {
        SceneManager.LoadScene(Racescene);
    }
}
