using UnityEngine;
using System.Collections.Generic;

public static class PlayerPositionKeeper
{
    private static Dictionary<string, Vector3> savedPositions = new Dictionary<string, Vector3>();

    // ˆÊ’u‚ð•Û‘¶
    public static void SavePosition(string sceneName, Vector3 pos)
    {
        savedPositions[sceneName] = pos;
    }

    // •Û‘¶‚ª‚ ‚é‚©
    public static bool HasPosition(string sceneName)
    {
        return savedPositions.ContainsKey(sceneName);
    }

    // •Û‘¶ˆÊ’u‚ðŽæ“¾
    public static Vector3 GetPosition(string sceneName)
    {
        if (savedPositions.ContainsKey(sceneName))
            return savedPositions[sceneName];

        return Vector3.zero;
    }
}