using UnityEngine;
using static E_Player_Controller;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public static int Game_Progress = 0;

    // プレイヤーの状態を保持する変数
    public PlayerState currentPlayerState;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // シーンを切り替えてもこのオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 既にインスタンスが存在する場合、重複を破棄
            Destroy(gameObject);
        }
    }
}
