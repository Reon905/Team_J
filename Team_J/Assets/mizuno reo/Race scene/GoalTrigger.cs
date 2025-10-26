//GoalTrigger
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // PlayerまたはRivalタグのみ通す
        if (other.CompareTag("Player") || other.CompareTag("Rival"))
        {
            if (RaceManager.Instance != null)
            {
                RaceManager.Instance.RegisterFinish(other.gameObject);
            }
        }
    }
}
