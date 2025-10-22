using UnityEngine;

public class CaughtSceneManager : MonoBehaviour
{
    void Start()
    {
        // Caught シーンに入った瞬間、Reduction を探して実行
        Reduction reducer = Object.FindFirstObjectByType<Reduction>();
        if (reducer != null)
        {
            reducer.ReduceAll();
        }
        else
        {
            Debug.LogWarning(" Reduction スクリプトが Caught シーン内に見つかりません。");
        }
    }
}