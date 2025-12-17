using UnityEngine;
using UnityEngine.UI;

public class Range_Text : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private Transform carTransform;   // 距離を測る車
    [SerializeField] private Transform goalTransform;  // ゴール位置
    [SerializeField] private Text distanceText;        // UI Text

    private bool hasFinished = false; // ゴールしたかどうか

    void Update()
    {
        if (carTransform == null || goalTransform == null || distanceText == null) return;

        // ゴール済みならテキスト非表示
        if (hasFinished)
        {
            distanceText.gameObject.SetActive(false);
            return;
        }

        // ゴールまでの距離を計算
        float distance = Vector2.Distance(carTransform.position, goalTransform.position);

        // UI に表示
        distanceText.text = $"ゴールまで: {distance:F1} m";

        // 任意でゴール判定（距離が一定以下になったらゴール扱い）
        if (distance < 0.5f) // 0.5m以内ならゴール
        {
            OnGoalReached();
        }
    }

    // ゴール時の処理
    private void OnGoalReached()
    {
        hasFinished = true;
        distanceText.gameObject.SetActive(false);

        Debug.Log("ゴールしました！");
    }
}

