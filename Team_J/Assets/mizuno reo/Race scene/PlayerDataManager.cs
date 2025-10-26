//PlayerDataManager
using UnityEngine;

public static class PlayerDataManager
{
    public static float totalPoints = 0;

    public static void AddPoints(float points)
    {
        totalPoints += points;
        Debug.Log($"現在の合計ポイント: {totalPoints}");
    }

    public static void ResetPoints()
    {
        totalPoints = 0;
    }
}
