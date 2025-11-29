// Constants.cs
using UnityEngine;

// MonoBehaviourを継承しない静的クラス
public static class Constants
{
    //NPC
    public static float DEFAULT_SIGHT_ANGLE = 40.0f;    //NPCの視界範囲
    public static float DEFAULT_DETECTION_VALUE = 0.0f; //初期発覚値
    public static float MAX_DETECTION_VALUE = 2.0f;     //最大発覚値

    public static float CHASE_TIMER = 10.0f;    //チェイス時間
}
