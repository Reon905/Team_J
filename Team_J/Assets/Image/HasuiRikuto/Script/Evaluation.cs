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
        if (Money.Instance == null)
        {
            Debug.LogError("Money.Instance が存在しません");
            return;
        }
        int evaluationPoints = Money.Instance.resultPoint;

        Debug.Log($"[Evaluation] 評価ポイント = {evaluationPoints}");

        currentRank = GetRank(evaluationPoints);

        rankText.text = currentRank;
    }

    string GetRank(int points)
    {
        if (points >= 1899)
        {
            audioSource.PlayOneShot(seRankS);
            return "S 最高評価です！おめでとう！";
        }
        else if (points >= 1400)
        {
            audioSource.PlayOneShot(seRankA);
            return "A おめでとう！あと少し！";
        }
        else if (points >= 800)
        {
            audioSource.PlayOneShot(seRankB);
            return "B いい感じ！";
        }
        else if (points >= 450)
        {
            audioSource.PlayOneShot(seRankC);
            return "C もう少し頑張ろう！";
        }
        else if (points >= 0)
        {
            audioSource.PlayOneShot(seRankD);
            return "D 頑張ろう！";
        }
        else
        {
            return "CRIME RACER";
        }
        
    }
}