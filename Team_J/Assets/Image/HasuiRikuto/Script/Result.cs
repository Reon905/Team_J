using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        Money.AddToTotal();

        int racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        Money.totalPoints += racePoints;

        // --- アイテム数合計 ---
        int itemCount =
            Item.itemCount + Item2.itemCount + Item3.itemCount + Item4.itemCount +
            Item5.itemCount + Item6.itemCount + Item7.itemCount + Item8.itemCount +
            Item9.itemCount + Item10.itemCount + Item11.itemCount + Item12.itemCount;

        // --- 金額を各 Item から合計 ---
        int totalMoney =
            Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney +
            Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney +
            Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;

        // --- 表示 ---
        resultText.text =
            $": {itemCount}個\n" +
            $": {totalMoney}円\n" +
            $": {Money.totalPoints}pt";

        Debug.Log($"[Result] totalMoney = {totalMoney}円");
        Debug.Log($"[Result] totalPoints = {Money.totalPoints}pt");
        Debug.Log($"[Result] itemCount = {itemCount}個");
    }
}