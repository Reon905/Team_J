using UnityEngine;

public class Reduction : MonoBehaviour
{
    // 各アイテムタイプの金額・ポイントを個別に8割減少させる
    public void ReduceAll()
    {
        // --- Item ---
        int oldMoney1 = Item.totalMoney;
        int oldPoints1 = Item.totalPoints;
        Item.totalMoney = Mathf.FloorToInt(Item.totalMoney * 0.2f);
        Item.totalPoints = Mathf.FloorToInt(Item.totalPoints * 0.2f);
        Debug.Log($"【Item】金額 {oldMoney1} → {Item.totalMoney} ／ ポイント {oldPoints1} → {Item.totalPoints}");

        // --- Item2 ---
        int oldMoney2 = Item2.totalMoney;
        int oldPoints2 = Item2.totalPoints;
        Item2.totalMoney = Mathf.FloorToInt(Item2.totalMoney * 0.2f);
        Item2.totalPoints = Mathf.FloorToInt(Item2.totalPoints * 0.2f);
        Debug.Log($"【Item2】金額 {oldMoney2} → {Item2.totalMoney} ／ ポイント {oldPoints2} → {Item2.totalPoints}");

        // --- Item3 ---
        int oldMoney3 = Item3.totalMoney;
        int oldPoints3 = Item3.totalPoints;
        Item3.totalMoney = Mathf.FloorToInt(Item3.totalMoney * 0.2f);
        Item3.totalPoints = Mathf.FloorToInt(Item3.totalPoints * 0.2f);
        Debug.Log($"【Item3】金額 {oldMoney3} → {Item3.totalMoney} ／ ポイント {oldPoints3} → {Item3.totalPoints}");

        Debug.Log("✅ 各アイテムの金額・ポイントを8割減少させました。");
    }
}