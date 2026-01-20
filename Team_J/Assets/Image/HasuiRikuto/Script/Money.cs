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

    /// <summary>
    /// 一回で稼いだ金額保存関数
    /// </summary>
    public void AddToTotal()
    {
        totalMoney += DayMoney;
        totalPoints += DayPoint;


        DayMoney = 0;
        DayPoint = 0;
        
    }
    public void ResetAll()
    {

    }
    void Start()
    {
        racePoints = PlayerPrefs.GetInt("TotalRacePoints", 0);
   
    }

    /// <summary>
    /// 捕まった場合金額・ポイント・アイテムの個数リセット
    /// </summary>
    public void OnCaughtInScene()
    {
        DayMoney -= SceneMoney;
        DayPoint -= ScenePoint;

        //個数も戻す
        BaseItem.itemCount -= SceneItemCount;

        if (DayMoney < 0) DayMoney = 0;
        if (DayPoint < 0) DayPoint = 0;
        if (BaseItem.itemCount < 0) BaseItem.itemCount = 0;

        Debug.Log($"捕獲：-{SceneMoney}円 / -{ScenePoint}pt / -{SceneItemCount}個");
    }
}