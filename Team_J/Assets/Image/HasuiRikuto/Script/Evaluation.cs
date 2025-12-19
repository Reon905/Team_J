using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public static string currentRank = "D";

    public Text rankText;

    [Header("Rank Sounds")]
    public AudioClip seRankS;
    public AudioClip seRankA;
    public AudioClip seRankB;
    public AudioClip seRankC;
    public AudioClip seRankD;

    public AudioSource audioSource;

    void Start()
    {
        // 1日の最終ポイント（評価専用）
        int evaluationPoints = Money.Instance.DayPoint
                             + PlayerPrefs.GetInt("TotalRacePoints", 0);

        // ランク判定
        currentRank = GetRank(evaluationPoints);

        Debug.Log($"評価ポイント: {evaluationPoints} → ランク: {currentRank}");

        if (rankText != null)
        {
            rankText.text = currentRank;
        }
    }

    string GetRank(int points)
    {
        if (points >= 1715)
        {
            audioSource.PlayOneShot(seRankS);
            return "S 最高評価です！おめでとう！";
        }
        else if (points >= 1030)
        {
            audioSource.PlayOneShot(seRankA);
            return "A おめでとう！あと少し！";
        }
        else if (points >= 650)
        {
            audioSource.PlayOneShot(seRankB);
            return "B いい感じ！";
        }
        else if (points >= 0)
        {
            audioSource.PlayOneShot(seRankC);
            return "C もう少し頑張ろう！";
        }
        else
        {
            audioSource.PlayOneShot(seRankD);
            return "D 頑張ろう！";
        }
    }
}