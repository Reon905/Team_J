using UnityEngine;

public class ProgressReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateManager.Game_Progress = 0;
    }
}
