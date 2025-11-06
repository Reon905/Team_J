using UnityEngine;

public class Reduction : MonoBehaviour
{
    // 各アイテムタイプ（Item ～ Item12）の金額・ポイントを個別に8割減少させる
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

        // --- Item4 ---
        int oldMoney4 = Item4.totalMoney;
        int oldPoints4 = Item4.totalPoints;
        Item4.totalMoney = Mathf.FloorToInt(Item4.totalMoney * 0.2f);
        Item4.totalPoints = Mathf.FloorToInt(Item4.totalPoints * 0.2f);
        Debug.Log($"【Item4】金額 {oldMoney4} → {Item4.totalMoney} ／ ポイント {oldPoints4} → {Item4.totalPoints}");

        // --- Item5 ---
        int oldMoney5 = Item5.totalMoney;
        int oldPoints5 = Item5.totalPoints;
        Item5.totalMoney = Mathf.FloorToInt(Item5.totalMoney * 0.2f);
        Item5.totalPoints = Mathf.FloorToInt(Item5.totalPoints * 0.2f);
        Debug.Log($"【Item5】金額 {oldMoney5} → {Item5.totalMoney} ／ ポイント {oldPoints5} → {Item5.totalPoints}");

        // --- Item6 ---
        int oldMoney6 = Item6.totalMoney;
        int oldPoints6 = Item6.totalPoints;
        Item6.totalMoney = Mathf.FloorToInt(Item6.totalMoney * 0.2f);
        Item6.totalPoints = Mathf.FloorToInt(Item6.totalPoints * 0.2f);
        Debug.Log($"【Item6】金額 {oldMoney6} → {Item6.totalMoney} ／ ポイント {oldPoints6} → {Item6.totalPoints}");

        // --- Item7 ---
        int oldMoney7 = Item7.totalMoney;
        int oldPoints7 = Item7.totalPoints;
        Item7.totalMoney = Mathf.FloorToInt(Item7.totalMoney * 0.2f);
        Item7.totalPoints = Mathf.FloorToInt(Item7.totalPoints * 0.2f);
        Debug.Log($"【Item7】金額 {oldMoney7} → {Item7.totalMoney} ／ ポイント {oldPoints7} → {Item7.totalPoints}");

        // --- Item8 ---
        int oldMoney8 = Item8.totalMoney;
        int oldPoints8 = Item8.totalPoints;
        Item8.totalMoney = Mathf.FloorToInt(Item8.totalMoney * 0.2f);
        Item8.totalPoints = Mathf.FloorToInt(Item8.totalPoints * 0.2f);
        Debug.Log($"【Item8】金額 {oldMoney8} → {Item8.totalMoney} ／ ポイント {oldPoints8} → {Item8.totalPoints}");

        // --- Item9 ---
        int oldMoney9 = Item9.totalMoney;
        int oldPoints9 = Item9.totalPoints;
        Item9.totalMoney = Mathf.FloorToInt(Item9.totalMoney * 0.2f);
        Item9.totalPoints = Mathf.FloorToInt(Item9.totalPoints * 0.2f);
        Debug.Log($"【Item9】金額 {oldMoney9} → {Item9.totalMoney} ／ ポイント {oldPoints9} → {Item9.totalPoints}");

        // --- Item10 ---
        int oldMoney10 = Item10.totalMoney;
        int oldPoints10 = Item10.totalPoints;
        Item10.totalMoney = Mathf.FloorToInt(Item10.totalMoney * 0.2f);
        Item10.totalPoints = Mathf.FloorToInt(Item10.totalPoints * 0.2f);
        Debug.Log($"【Item10】金額 {oldMoney10} → {Item10.totalMoney} ／ ポイント {oldPoints10} → {Item10.totalPoints}");

        // --- Item11 ---
        int oldMoney11 = Item11.totalMoney;
        int oldPoints11 = Item11.totalPoints;
        Item11.totalMoney = Mathf.FloorToInt(Item11.totalMoney * 0.2f);
        Item11.totalPoints = Mathf.FloorToInt(Item11.totalPoints * 0.2f);
        Debug.Log($"【Item11】金額 {oldMoney11} → {Item11.totalMoney} ／ ポイント {oldPoints11} → {Item11.totalPoints}");

        // --- Item12 ---
        int oldMoney12 = Item12.totalMoney;
        int oldPoints12 = Item12.totalPoints;
        Item12.totalMoney = Mathf.FloorToInt(Item12.totalMoney * 0.2f);
        Item12.totalPoints = Mathf.FloorToInt(Item12.totalPoints * 0.2f);
        Debug.Log($"【Item12】金額 {oldMoney12} → {Item12.totalMoney} ／ ポイント {oldPoints12} → {Item12.totalPoints}");

        Debug.Log("✅ 各アイテム（Item～Item12）の金額・ポイントを8割減少させました。");
    }
}