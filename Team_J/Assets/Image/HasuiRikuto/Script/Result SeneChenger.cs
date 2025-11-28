using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSeneChenger: MonoBehaviour 
{ 
    void Update()
    {
        // Enterキーでシーン切り替え
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Office Sene");
        }
    }
}
