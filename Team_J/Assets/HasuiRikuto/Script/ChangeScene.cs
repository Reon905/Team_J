using UnityEngine;
using UnityEngine.SceneManagement;        //シーンの切り替えに必要

public class ChangeScene : MonoBehaviour
{

    public string nextSceneName = "";  //切り替え先のシーン名


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
