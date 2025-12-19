using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;
    public static int racePoints;
    void Start()
    {
        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
    }

    void Update()
    {
        if (Money.Instance == null) return;

        moneyText.text =
            $"合計金額: {Money.Instance.DayMoney}円\n" +
            $"合計ポイント: {Money.Instance.DayPoint + racePoints}pt";
    }
}