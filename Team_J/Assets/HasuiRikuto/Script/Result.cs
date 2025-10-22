using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        // すでに減少済みの値を表示
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount;

        resultText.text =
            $": {itemCount}個\n" +
            $": {totalMoney}円\n" +
            $": {totalPoints}pt";

        Debug.Log($"[Result] : {itemCount}個");
        Debug.Log($"[Result] : {totalPoints}pt");
        Debug.Log($"[Result] : {totalMoney}円");
    }
}