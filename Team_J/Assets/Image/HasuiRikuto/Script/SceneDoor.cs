using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    public string nextScene;
    public PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // シーン切替前に必ず位置保存
            playerController.SaveCurrentPosition();

            // シーン切替
            SceneManager.LoadScene(nextScene);
        }
    }
}