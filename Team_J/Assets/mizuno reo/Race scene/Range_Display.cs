using UnityEngine;

//車からゴールまでの残り距離を計算するコード
public class Range_Display : MonoBehaviour
{
    [Header("設定")]
    //のTransformをInspectorから持ってくる
    //GoalObjから距離を計算するため
    [SerializeField] private Transform goalTransform;
    //現在のゴールまでの距離
    public float CurrentDistance { get; private set; }
    //ゴールしたかどうかのフラグ
    public bool IsGoalReached { get; private set; }

    void Update()
    {
        //ゴールが設定されていなければ処理しない
        if (goalTransform == null) return;

        // Y座標の差だけで距離を出す
        float distance = goalTransform.position.y - transform.position.y;

        // ゴールを通り過ぎたら 0 に固定
        if (distance <= 0f)
        {
            CurrentDistance = 0f;
            IsGoalReached = true;
        }
        else
        {
            //ゴールをしてなかったら、そのまま距離を入れる
            CurrentDistance = distance;
        }
    }
}
