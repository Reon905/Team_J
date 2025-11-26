using UnityEngine;

public class GameProgressAdd : MonoBehaviour
{
    private int GameProgress = GameStateManager.Game_Progress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStateManager.Game_Progress += 1;

        Debug.Log(GameProgress);
    }
}
