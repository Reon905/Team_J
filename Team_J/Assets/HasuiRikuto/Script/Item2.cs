using UnityEngine;

public class Item2 : MonoBehaviour
{
    //アイテムの金額
    public int price;

    //プレイヤーの合計金額
    public static int totalMoney = 0;

    //プレイヤーの合計ポイント
    public static int totalPoints = 0;

    //アイテムカウント
    public static int itemCount = 0;

    void Start()
    {
        //1000円〜2000円の範囲でランダムな値を設定
        price = Random.Range(1000, 2001);
        Debug.Log("アイテムの金額は " + price + " 円です、ポイント 20pt");
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーに触れたら処理
        if (other.CompareTag("Player"))
        {
            //金額を加算
            totalMoney += price;

            //固定の20ポイントを加算
            totalPoints += 20;

            itemCount++;

            Debug.Log("アイテムを取得！ 金額 +" + price + "円、ポイント +20pt");
            Debug.Log("現在の合計：金額 " + totalMoney + "円 ／ ポイント " + totalPoints + "pt");

            //このアイテムを削除
            Destroy(gameObject);
        }
    }
}