using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    void OnDestroy()
    {
        // 現在のシーン名
        string sceneName = SceneManager.GetActiveScene().name;

        // 現在のプレイヤー位置を保存
        PlayerPositionKeeper.SavePosition(sceneName, transform.position);
    }
}