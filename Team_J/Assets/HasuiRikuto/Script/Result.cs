using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        //各アイテムのポイントを合算  レース順位でもらえるポイント未加算
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //各アイテムの金額を合算 
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;

        //アイテムの取得数
        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount;


        //画面に表示
        resultText.text =

             itemCount + "個\n" +
             totalMoney + "円\n" +
             totalPoints + "pt\n";

        //表示
        Debug.Log("合計アイテム: " + itemCount + "個");
        Debug.Log("合計ポイント: " + totalPoints + "pt");
        Debug.Log("合計金額: " + totalMoney + "円");
    }
}
