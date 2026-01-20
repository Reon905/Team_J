//PlayerDataManager
using UnityEngine;

public static class PlayerDataManager
{
    public static float totalPoints = 0;
    /// <summary>
    /// プレイヤーがゴールしたら合計ポイントに加算する
    /// </summary>
    /// <param name="points"></param>
    public static void AddPoints(float points)
    {
        totalPoints += points;
        Debug.Log($"現在の合計ポイント: {totalPoints}");
    }
}
