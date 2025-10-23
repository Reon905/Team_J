using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public static string currentRank = "D"; //現在のランクを保持

    public Text rankText;

    void Start()
    {
        //Item,Item2,Item3の合計ポイントを取得
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //ランクを判定して設定
        currentRank = GetRank(totalPoints);

        //デバッグ表示
        Debug.Log("合計ポイント: " + totalPoints + "pt → ランク: " + currentRank);

        //画面にも表示
        if (rankText != null)
        {
            rankText.text = currentRank;
        }
        else
        {
            Debug.LogWarning("Rank Text が設定されていません！");
        }
    }

    //ポイントに応じてランクを返す関数
    string GetRank(int totalPoints)
    {
        if (totalPoints >= 250)
            return "S       おめでとう！";
        else if (totalPoints >= 130)
            return "A  おめでとうあと少し！";
        else if (totalPoints >= 80)
            return "B  いい感じ！あと一歩！";
        else if (totalPoints >= 30)
            return "C    もう少し頑張ろう！";
        else
            return "D       頑張ろう！";
    }
}
