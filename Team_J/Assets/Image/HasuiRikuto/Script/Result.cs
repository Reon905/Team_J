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
       
        //表示
        resultText.text =
             $": {BaseItem.itemCount}個\n"+
             $": {Money.Instance.totalMoney}円\n" +
             $": {Money.Instance.totalPoints}pt";

        Debug.Log($"[Result] totalMoney = {Money.Instance.totalMoney}円");
        Debug.Log($"[Result] totalPoints = {Money.Instance.totalPoints}pt");
    }

}