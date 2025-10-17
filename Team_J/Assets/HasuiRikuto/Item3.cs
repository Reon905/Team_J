using UnityEngine;

public class Item3 : MonoBehaviour
{
    // アイテムの金額
    public int price;

    void Start()
    {
        // 1000円〜2000円の範囲でランダムな値を設定（2000を含めたい場合は +1）
        price = Random.Range(5000, 10001);

        Debug.Log("アイテムの金額は " + price + " 円です。");
    }
}