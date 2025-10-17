using UnityEngine;

public class Item : MonoBehaviour
{
    // アイテムの金額
    public int price;

    void Start()
    {
        // 2000円〜4000円の範囲でランダムな値を設定
        price = Random.Range(2000, 4001);

        Debug.Log("アイテムの金額は " + price + " 円です。");
    }
}