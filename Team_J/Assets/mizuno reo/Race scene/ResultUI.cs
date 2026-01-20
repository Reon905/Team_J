//ResultUI 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI totalText;
    
    /// <summary>
    /// リザルトのテキスト表示
    /// </summary>
    void Start()
    {

        int rank = PlayerPrefs.GetInt("LastRank", 0);
        int points = PlayerPrefs.GetInt("LastPoints", 0);
        int total = PlayerPrefs.GetInt("TotalPoints", 0);

    }

    public void OnNextButton()
    {
        SceneManager.LoadScene("MainMenu"); // 次のシーン名に合わせて変更
    }
}
