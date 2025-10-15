using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
public void OnStartButton()
    {
        SceneManager.LoadScene("Race scene");//ゲームシーンの部分はシーンの名前を変更
    }
}

