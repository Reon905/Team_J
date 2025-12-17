using UnityEngine;

public class SceneStartManager : MonoBehaviour
{
    void Start()
    {
        Money.Instance.SceneMoney = 0;
        Money.Instance.ScenePoint = 0;
        Money.Instance.SceneItemCount = 0;
    }
}