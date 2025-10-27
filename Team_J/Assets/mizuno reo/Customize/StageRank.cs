//StageRank
using UnityEngine;
using UnityEngine.UI;

public class StageRank : MonoBehaviour
{
    [SerializeField] private Text RankText;

    private void Start()
    {//CarStageを表示
        if (RankText == null)
        {
            Debug.LogError("RankText が設定されていません! Canvas 上の Taxt をドラッグしてください。");
            return;
        }
        RankText.text = $"現在の強化レベル: {Customize.CarStage}";
    }

    private void Update()
    {//CarStageを更新した分を表示
        if (RankText == null) return;

        RankText.text = $"現在の強化レベル: {Customize.CarStage}";
    }
}
