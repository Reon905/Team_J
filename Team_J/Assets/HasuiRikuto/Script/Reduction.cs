using UnityEngine;

public class Reduction : MonoBehaviour
{
    void Start()
    {
        // --- テスト用に数値を代入 ---
        Item.totalMoney = 1034;
        Item2.totalMoney = 2030;
        Item3.totalMoney = 3042;

        Item.totalPoints = 60;
        Item2.totalPoints = 40;
        Item3.totalPoints = 60;

        Debug.Log("テスト初期値：");
        Debug.Log($"Item1 → {Item.totalMoney}円／{Item.totalPoints}pt");
        Debug.Log($"Item2 → {Item2.totalMoney}円／{Item2.totalPoints}pt");
        Debug.Log($"Item3 → {Item3.totalMoney}円／{Item3.totalPoints}pt");

        //アイテム8割減額 ポイント４割減少
        Item.totalMoney = Mathf.FloorToInt(Item.totalMoney * 0.2f);
        Item.totalPoints = Mathf.FloorToInt(Item.totalPoints * 0.6f);

        Item2.totalMoney = Mathf.FloorToInt(Item2.totalMoney * 0.2f);
        Item2.totalPoints = Mathf.FloorToInt(Item2.totalPoints * 0.6f);

        Item3.totalMoney = Mathf.FloorToInt(Item3.totalMoney * 0.2f);
        Item3.totalPoints = Mathf.FloorToInt(Item3.totalPoints * 0.6f);

        // --- コンソールで結果確認 ---
        Debug.Log("捕まった後（8割減額　ポイント４割減少）：");
        Debug.Log($"Item1 → {Item.totalMoney}円／{Item.totalPoints}pt");
        Debug.Log($"Item2 → {Item2.totalMoney}円／{Item2.totalPoints}pt");
        Debug.Log($"Item3 → {Item3.totalMoney}円／{Item3.totalPoints}pt");

        // --- 合計も確認 ---
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        Debug.Log($"合計：{totalMoney}円 ／ {totalPoints}pt");
    }
}