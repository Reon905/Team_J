//PlayerDataManager
using UnityEngine;

public static class PlayerDataManager
{
    public static float totalPoints = 0;

    public static void AddPoints(float points)
    {
        totalPoints += points;
        Debug.Log($"���݂̍��v�|�C���g: {totalPoints}");
    }

    public static void ResetPoints()
    {
        totalPoints = 0;
    }
}
