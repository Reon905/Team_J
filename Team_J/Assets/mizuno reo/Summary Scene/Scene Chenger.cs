using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChenger : MonoBehaviour 
{
    private void Update()
    {
        //エンターを押したらシーン切り替え
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("indoor Scene");
        }
    }
}
