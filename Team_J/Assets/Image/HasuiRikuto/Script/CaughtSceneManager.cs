using UnityEngine;

public class CaughtSceneManager : MonoBehaviour
{
    void Start()
    {
        Money.Instance.OnCaughtInScene();
    }
}
