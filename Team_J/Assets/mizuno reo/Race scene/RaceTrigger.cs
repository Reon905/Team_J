//RaceTrigger
using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    // スタート or ゴールを選択する
    public enum TriggerType { Start, Goal }
    public TriggerType triggerType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ----------------------------
        // 🏁 スタート地点
        // ----------------------------
        if (triggerType == TriggerType.Start)
        {
            // プレイヤーが通過し、まだレースが始まっていない場合のみ開始
            if (other.CompareTag("Player") && !RaceManager.Instance.IsRaceStarted())
            {
                RaceManager.Instance.StartRace();
                Debug.Log("レース開始！");
            }
        }

        // ----------------------------
        // 🏆 ゴール地点
        // ----------------------------
        else if (triggerType == TriggerType.Goal)
        {
            // プレイヤー or ライバルがゴールラインを通過した場合
            if (other.CompareTag("Player") || other.CompareTag("Rival"))
            {
                RaceManager.Instance.RegisterFinish(other.gameObject);
                Debug.Log($"{other.name} がゴールしました！");
            }
        }
    }
}
