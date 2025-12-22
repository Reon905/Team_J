//Range_Text
using UnityEngine;
using UnityEngine.UI;

public class Range_Text : MonoBehaviour
{
    //距離計算用
    [SerializeField] private Range_Display rangeDisplay;
    [SerializeField] private Text distanceText;

    void Update()
    {
        if (rangeDisplay == null || distanceText == null) return;
        //ゴールまでの距離を小数点一桁で表示
        distanceText.text = $"ゴ|ルまで: {rangeDisplay.CurrentDistance:F1} m";
    }
}
