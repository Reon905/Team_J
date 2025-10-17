using UnityEngine;

public class Item3 : MonoBehaviour
{
    // アイテムの金額
    public int price;

    void Start()
    {
        // 5000円〜10000円の範囲でランダムな値を設定
        price = Random.Range(5000, 10001);

        Debug.Log("アイテムの金額は " + price + " 円です。");
    }
}