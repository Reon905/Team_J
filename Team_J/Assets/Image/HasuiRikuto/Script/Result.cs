using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text resultText;
    public static int racePoints;
    void Start()
    {
        Money.Instance.AddToTotal();

        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        // Money.totalPoints += racePoints;

        // --- アイテム数合計 ---
        int itemCount =
            Item.itemCount + Item2.itemCount + Item3.itemCount + Item4.itemCount +
            Item5.itemCount + Item6.itemCount + Item7.itemCount + Item8.itemCount +
            Item9.itemCount + Item10.itemCount + Item11.itemCount + Item12.itemCount;

        //// --- 金額を各 Item から合計 ---
        //int totalMoney =
        //    Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney +
        //    Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney +
        //    Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;

        //int totalPoints =
        //   Item.totalPoints + Item2.totalPoints + Item3.totalPoints + Item4.totalPoints +
        //   Item5.totalPoints + Item6.totalPoints + Item7.totalPoints + Item8.totalPoints +
        //   Item9.totalPoints + Item10.totalPoints + Item11.totalPoints + Item12.totalPoints + racePoints;

        // --- 表示 ---
        resultText.text =
             $": {itemCount}個\n"+
             $": {Money.Instance.totalMoney}円\n" +
             $": {Money.Instance.totalPoints}pt";

        Debug.Log($"[Result] totalMoney = {Money.Instance.totalMoney}円");
        Debug.Log($"[Result] totalPoints = {Money.Instance.totalPoints}pt");
    }

}