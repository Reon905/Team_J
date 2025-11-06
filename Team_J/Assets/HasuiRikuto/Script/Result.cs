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

        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount + Item4.itemCount + Item5.itemCount + Item6.itemCount + Item7.itemCount + Item8.itemCount + Item9.itemCount + Item10.itemCount + Item11.itemCount + Item12.itemCount;

        resultText.text =
            $": {itemCount}個\n" +
            $": {Money.totalMoney}円\n" +
            $": {Money.totalPoints}pt";

        Debug.Log($"[Result] : {itemCount}個");
        Debug.Log($"[Result] : {Money.totalPoints}pt (レース分 {racePoints}pt 含む)");
        Debug.Log($"[Result] : {Money.totalMoney}円");
    }
   
}