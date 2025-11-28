using UnityEngine;
using UnityEngine.SceneManagement;

public class CaughtChangeScene : MonoBehaviour
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
                GameStateManager.Game_Progress += 1;
            }
            else if (GameStateManager.Game_Progress == 2)
            {
                GameStateManager.Game_Progress += 1;
            }




            
            if (GameStateManager.Game_Progress == 0)     //進行度０
            {
                SceneManager.LoadScene("Indoor Scene");
            }
            else if (GameStateManager.Game_Progress == 1)//進行度１
            {

                Debug.Log(GameStateManager.Game_Progress);
                SceneManager.LoadScene("Scen_Customize");
            }
            else if (GameStateManager.Game_Progress == 2)//進行度２
            {
                SceneManager.LoadScene("Indoor Office Scene");
            }
            else if (GameStateManager.Game_Progress == 3)//進行度３
            {

                Debug.Log(GameStateManager.Game_Progress);
                SceneManager.LoadScene("Scen_Customize");
            }
            else if (GameStateManager.Game_Progress == 4)//進行度４
            {
                GameStateManager.Game_Progress += 1;
                Debug.Log(GameStateManager.Game_Progress);
                SceneManager.LoadScene("Scen_Customize");
            }
            else if (GameStateManager.Game_Progress == 5)//進行度５
            {
                SceneManager.LoadScene("Result Scene");
            }

        }
    }
}
