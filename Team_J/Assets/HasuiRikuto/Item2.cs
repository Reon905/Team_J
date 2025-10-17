using UnityEngine;

public class Item2 : MonoBehaviour
{
    // アイテムの金額
    public int price;

    void Start()
    {
        // 1000円〜2000円の範囲でランダムな値を設定
        price = Random.Range(1000, 2001);

        Debug.Log("アイテムの金額は " + price + " 円です。");
    }
}
