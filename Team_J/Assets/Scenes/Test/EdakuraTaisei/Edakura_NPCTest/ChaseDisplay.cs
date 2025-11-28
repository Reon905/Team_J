using UnityEngine;
using UnityEngine.UI;

public class ChaseDisplay : MonoBehaviour
{
    public Text ChaseDisplayText;

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
        {
            ChaseDisplayText.text = "“‚ßc";
        }
        else
        {
            ChaseDisplayText.text = "“¦‚°‚«‚êI";
        }
    }
}
