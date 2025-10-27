using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;
        int racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        totalPoints += racePoints;

        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount;

        resultText.text =
            $": {itemCount}個\n" +
            $": {totalMoney}円\n" +
            $": {totalPoints}pt";

        Debug.Log($"[Result] : {itemCount}個");
        Debug.Log($"[Result] : {totalPoints}pt (レース分 {racePoints}pt 含む)");
        Debug.Log($"[Result] : {totalMoney}円");
    }
}