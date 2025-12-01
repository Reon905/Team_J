using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    
    public Transform spawnPoint;  // このドアから出た位置（次のシーンの開始地点）

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 今いるシーン名
            string currentScene = SceneManager.GetActiveScene().name;

            // プレイヤーの位置を正しく保存（ドアを通った位置）
            PlayerPositionKeeper.SavePosition(currentScene, other.transform.position);

            // 次のシーンへ
           
        }
    }
}