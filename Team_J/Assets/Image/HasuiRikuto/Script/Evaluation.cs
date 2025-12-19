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
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints+Item4.totalPoints+Item5.totalPoints+Item6.totalPoints+Item7.totalPoints+Item8.totalPoints+Item9.totalPoints+Item10.totalPoints+Item11.totalPoints+Item12.totalPoints;
        int racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        totalPoints += racePoints;
        Money.Instance.DayPoint += racePoints;

        //ランクを判定して設定
        currentRank = GetRank(Money.Instance.DayPoint);

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
        if (Money.Instance.DayPoint >= 1715)
        {
            audioSource.PlayOneShot(seRankS);
            return "S 最高評価です  おめでとう！";
        }
        else if (Money.Instance.DayPoint <= 1714 && Money.Instance.DayPoint >= 1030)
        {
            audioSource.PlayOneShot(seRankA);
            return "A  おめでとう あと少し！";
        }
        else if (Money.Instance.DayPoint <= 1029 && Money.Instance.DayPoint >= 650)
        {
            audioSource.PlayOneShot(seRankB);
            return "B  いい感じ！ あと一歩！";
        }
        else if (Money.Instance.DayPoint <= 649 && Money.Instance.DayPoint >= 0)
        {
            audioSource.PlayOneShot(seRankC);
            return "C    もう少し頑張ろう！";
        }
        else
            audioSource.PlayOneShot(seRankD);
            return "D       頑張ろう！";
    }
}
