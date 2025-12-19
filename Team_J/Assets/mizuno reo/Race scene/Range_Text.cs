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

        // ゴールまでの距離を計算
        float distance = Vector2.Distance(carTransform.position, goalTransform.position);

        // ★② ここに書く！
        if (distance <= 0.1f)
        {
            distanceText.text = "ゴールまで: 0.0 m";
            hasFinished = true;
            return; // これ以上 Update を進めない
        }

        // ゴール後は更新しない
        if (hasFinished) return;

        // 通常表示
        distanceText.text = $"ゴールまで: {distance:F1} m";
    }


    // ゴール時の処理
    private void OnGoalReached()
    {
        hasFinished = true;
        distanceText.gameObject.SetActive(false);

        Debug.Log("ゴールしました！");
    }
}

