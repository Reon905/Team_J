using UnityEngine;
using UnityEngine.UI;

public class ChaseDisplay : MonoBehaviour
{
    public Text ChaseDisplayText;

    // Update is called once per frame
    void Update()
    {
        // 状態が未発覚の場合、盗めテキスト表示
        if (GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
        {
            ChaseDisplayText.text = "盗め…";
        }
        else// 状態が発覚の場合、逃げ切れテキスト表示
        {
            ChaseDisplayText.text = "逃げきれ！";
        }
    }
}
