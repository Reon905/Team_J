using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneyText; // UI TextをInspectorにドラッグ

    void Update()
    {
        // 各アイテムスクリプトの合計金額を取得
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;

        // 各アイテムスクリプトの合計ポイントを取得
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        // テキストを画面に表示（改行で2行に分ける）
        moneyText.text =
            "合計金額: " + totalMoney + "円\n" +
            "合計ポイント: " + totalPoints + "pt";
    }
}