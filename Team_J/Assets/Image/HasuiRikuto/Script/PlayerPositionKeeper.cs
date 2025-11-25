using UnityEngine;
using System.Collections.Generic;

public static class PlayerPositionKeeper
{
    // ÉVÅ[ÉìÇ≤Ç∆Ç…à íuÇï€ë∂
    private static Dictionary<string, Vector3> savedPositions = new Dictionary<string, Vector3>();

    public static void SavePosition(string sceneName, Vector3 pos)
    {
        savedPositions[sceneName] = pos;
    }

    public static Vector3 GetPosition(string sceneName)
    {
        if (savedPositions.ContainsKey(sceneName))
        {
            return savedPositions[sceneName];
        }

        return Vector3.zero; // ï€ë∂Ç™Ç»Ç¢èÍçá
    }
    public static bool HasPosition(string sceneName)
    {
        return savedPositions.ContainsKey(sceneName);
    }
}
