using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSuccesful : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameStateManager.Game_Progress == 0)
            {
                SceneManager.LoadScene("Indoor Scene");
            }
            if (GameStateManager.Game_Progress == 1)
            {
                SceneManager.LoadScene("Indoor Scene 2");
            }
            else if (GameStateManager.Game_Progress == 2)
            {
                SceneManager.LoadScene("Indoor Office Scene");
            }
            else if (GameStateManager.Game_Progress == 3)
            {
                SceneManager.LoadScene("indoor Office Scene2");
            }
            else if (GameStateManager.Game_Progress == 4)
            {
                SceneManager.LoadScene("Indoor Bank Scene");
            }
           
        }
    }
}
