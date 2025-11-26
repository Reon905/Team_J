using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (PlayerPositionKeeper.HasPosition(sceneName))
        {
            player.transform.position = PlayerPositionKeeper.GetPosition(sceneName);
        }
    }
}