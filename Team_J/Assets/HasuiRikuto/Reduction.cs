using UnityEngine;

public class Reduction : MonoBehaviour
{
    void Start()
    {
        //各アイテムスクリプトの合計を計算
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //減らす割合（8割・4割
        int lostMoney = Mathf.FloorToInt(totalMoney * 0.8f);
        int lostPoints = Mathf.FloorToInt(totalPoints * 0.4f);

        //減算後の値
        totalMoney -= lostMoney;
        totalPoints -= lostPoints;

        //0未満にならないように補正
        if (totalMoney < 0) totalMoney = 0;
        if (totalPoints < 0) totalPoints = 0;

        //結果をそれぞれに反映
        Item.totalMoney = totalMoney;
        Item2.totalMoney = totalMoney;
        Item3.totalMoney = totalMoney;

        Item.totalPoints = totalPoints;
        Item2.totalPoints = totalPoints;
        Item3.totalPoints = totalPoints;

       //デバック
        Debug.Log("捕まった後のシーン：金額 -" + lostMoney + "円、ポイント -" + lostPoints + "pt");
        Debug.Log("現在の合計：金額 " + totalMoney + "円 ／ ポイント " + totalPoints + "pt");
    }
}