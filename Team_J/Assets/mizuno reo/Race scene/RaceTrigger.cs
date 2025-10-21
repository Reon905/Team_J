using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    // スタートかゴールか選ぶための列挙型
    public enum TriggerType { Start, Goal }
    public TriggerType triggerType;

    // プレイヤーがトリガーに入った時に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーのタグを持つオブジェクトだけ処理
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Start)
            {
                RaceManager.Instance.StartRace();   // スタート処理呼び出し
            }
            else if (triggerType == TriggerType.Goal)
            {
                RaceManager.Instance.Finish();  // ゴール処理呼び出し
            }
        }
    }
}
