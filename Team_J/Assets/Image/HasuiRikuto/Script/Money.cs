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

    public int resultPoint;

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
        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);

        //ここで一度だけ合算
        //DayPoint += racePoints;
    }
    public void AddRacePointByRank(int rank)
    {
        int point = rank switch
        {
            1 => 100,
            2 => 70,
            3 => 30,
            _ => 10
        };

        DayPoint += point;
        Debug.Log($"[Race] Rank {rank} +{point}pt / DayPoint={DayPoint}");
    }

    public void AddToTotal()
    {
        totalMoney += DayMoney;
        totalPoints += DayPoint;


        DayMoney = 0;
        DayPoint = 0;
        
    }
    public void ResetAll()
    {
        //racePoints = 0;
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