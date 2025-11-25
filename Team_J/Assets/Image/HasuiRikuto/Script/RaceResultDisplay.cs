using UnityEngine;
using UnityEngine.UI;

public class RaceRankDisplay : MonoBehaviour
{
    public Text rankText;  // 表示するUI Text

    void Start()
    {
        // 🔹 RaceManagerで保存された順位データを取得
        int lastRank = PlayerPrefs.GetInt("LastRank", 0);

        // 🔹 順位が未保存だった場合の対策
        if (lastRank <= 0)
        {
            rankText.text = "順位データなし";
            Debug.LogWarning("順位データが保存されていません。");
            return;
        }

        // 🔹 順位をUIに表示
        rankText.text = ": " + lastRank + " 位";

        Debug.Log("[RaceRankDisplay] 順位: " + lastRank + "位");


    }
}