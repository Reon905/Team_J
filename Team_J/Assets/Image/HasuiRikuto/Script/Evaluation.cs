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
    /// <summary>
    /// 総合評価表示関数
    /// </summary>
    string GetRank(int points)
    {
        if (points >= 1899)
        {
            audioSource.PlayOneShot(seRankS);
            rankText.color = Color.gold;
            return "総合評価：S\n最高評価です！おめでとう！";
        }
        else if (points >= 1400)
        {
            audioSource.PlayOneShot(seRankA);
            rankText.color = Color.mediumPurple;
            return "総合評価：A\nおめでとう！あと少し！";
        }
        else if (points >= 800)
        {
            audioSource.PlayOneShot(seRankB);
            rankText.color = Color.lightBlue;
            return "総合評価：B\nいい感じ！";
        }
        else if (points >= 450)
        {
            audioSource.PlayOneShot(seRankC);
            rankText.color = Color.lightGreen;
            return "総合評価：C\nもう少し頑張ろう！";
        }
        else if (points >= 0)
        {
            audioSource.PlayOneShot(seRankD);
            return "総合評価：D\n頑張ろう！";
        }
        else
        {
            return "CRIME RACER";
        }
        
    }
}