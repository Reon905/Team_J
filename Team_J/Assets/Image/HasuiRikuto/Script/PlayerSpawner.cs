using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManager;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // •Û‘¶‚µ‚½À•W‚ğæ“¾
        Vector3 savedPos = PlayerPositionKeeper.GetPosition(sceneName);
        if (PlayerPositionKeeper.HasPosition(sceneName))
        {
            player.transform.position = savedPos;
        }
    }
}
