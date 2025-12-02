using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static int DayMoney;
    public static int DayPoint;
    public static int racePoints;

    public static int totalMoney;
    public static int totalPoints;

    public Text moneyText; // UI TextをInspectorにドラッグ

    void Start()
    {
        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        //Money.totalPoints += racePoints;
    }
    void Update()
    {
        // 各アイテムスクリプトの合計金額を取得
        DayMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney + Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney + Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;

        // 各アイテムスクリプトの合計ポイントを取得
        DayPoint = Item.totalPoints + Item2.totalPoints + Item3.totalPoints + Item4.totalPoints + Item5.totalPoints + Item6.totalPoints + Item7.totalPoints + Item8.totalPoints + Item9.totalPoints + Item10.totalPoints + Item11.totalPoints + Item12.totalPoints + racePoints;
        
        
        moneyText.text =
           "合計金額: " + DayMoney + "円\n" +
           "合計ポイント: " + DayPoint + "pt";
    }
    public static void AddToTotal()
    {
        totalMoney += DayMoney;
        totalPoints += DayPoint;

        Debug.Log($"[Result] : {totalPoints}pt");
        Debug.Log($"[Result] : {totalMoney}円");
    }
}