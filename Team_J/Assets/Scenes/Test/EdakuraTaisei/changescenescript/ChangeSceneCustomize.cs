using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCustomize : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Scen_Customize");
        }
    }
}
