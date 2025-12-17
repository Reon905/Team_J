using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    public static Money Instance;

    public int DayMoney;
    public int DayPoint;
    public int racePoints;

    public int totalMoney;
    public int totalPoints;

    public int SceneMoney;
    public int ScenePoint;
    public int SceneItemCount;

    // public Text moneyText; // UI TextをInspectorにドラッグ

    void Awake()
    {
        //シングルトン
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddToTotal()
    {
        totalMoney += DayMoney;
        totalPoints += DayPoint;

        DayMoney = 0;
        DayPoint = 0;
    }
    void Start()
    {
        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
        //Money.totalPoints += racePoints;
    }

    public void OnCaughtInScene()
    {
        DayMoney -= SceneMoney;
        DayPoint -= ScenePoint;

        // ★ 個数も戻す
        Item.itemCount -= SceneItemCount;

        if (DayMoney < 0) DayMoney = 0;
        if (DayPoint < 0) DayPoint = 0;
        if (Item.itemCount < 0) Item.itemCount = 0;

        Debug.Log($"捕獲：-{SceneMoney}円 / -{ScenePoint}pt / -{SceneItemCount}個");
    }
}