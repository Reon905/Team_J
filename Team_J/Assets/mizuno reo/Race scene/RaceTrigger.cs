using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    // スタートかゴールか選ぶための列挙型
    public enum TriggerType { Start, Goal }
    public TriggerType triggerType;

    // プレイヤーがトリガーに入ったときの処理
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーのタグを持つオブジェクトだけ反応
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Start)
            {
                // スタートラインに入ったらレース開始処理を呼ぶ
                RaceManager.Instance.StartRace();
            }
            else if (triggerType == TriggerType.Goal)
            {
                // ゴールラインに入ったらレース終了処理を呼ぶ
                RaceManager.Instance.FinishRace();
            }
        }
    }
}
