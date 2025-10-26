//ResultUI 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI totalText;

    void Start()
    {
        int rank = PlayerPrefs.GetInt("LastRank", 0);
        int points = PlayerPrefs.GetInt("LastPoints", 0);
        int total = PlayerPrefs.GetInt("TotalPoints", 0);


    }

    public void OnNextButton()
    {
        SceneManager.LoadScene("MainMenu"); // Ÿ‚ÌƒV[ƒ“–¼‚É‡‚í‚¹‚Ä•ÏX
    }
}
