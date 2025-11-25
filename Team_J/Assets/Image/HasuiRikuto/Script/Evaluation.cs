using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public static string currentRank = "D"; //現在のランクを保持

    public Text rankText;

    //ランクごとにSEを鳴らす
    [Header("Rank Sounds")]
    public AudioClip seRankS;
    public AudioClip seRankA;
    public AudioClip seRankB;
    public AudioClip seRankC;
    public AudioClip seRankD;

    public AudioSource audioSource;
    void Start()
    {
        //Item,Item2,Item3,レースの合計ポイントを取得
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;
        int racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        totalPoints += racePoints;

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
        {
            audioSource.PlayOneShot(seRankS);
            return "S       おめでとう！";
        }
        else if (totalPoints >= 130)
        {
            audioSource.PlayOneShot(seRankA);
            return "A  おめでとうあと少し！";
        }
        else if (totalPoints >= 80)
        {
            audioSource.PlayOneShot(seRankB);
            return "B  いい感じ！あと一歩！";
        }
        else if (totalPoints >= 30)
        {
            audioSource.PlayOneShot(seRankC);
            return "C    もう少し頑張ろう！";
        }
        else
            audioSource.PlayOneShot(seRankD);
            return "D       頑張ろう！";
    }
}
