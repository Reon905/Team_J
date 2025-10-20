using UnityEngine;

public class Item3 : MonoBehaviour
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
        //5000円〜10000円の範囲でランダムな値を設定
        price = Random.Range(5000, 10001);
        Debug.Log("アイテムの金額は " + price + " 円です、ポイント 60pt");
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーに触れたら処理
        if (other.CompareTag("Player"))
        {
            //金額を加算
            totalMoney += price;

            //固定の60ポイントを加算
            totalPoints += 60;

            itemCount++;

            Debug.Log("アイテムを取得！ 金額 +" + price + "円、ポイント +60pt");
            Debug.Log("現在の合計：金額 " + totalMoney + "円 ／ ポイント " + totalPoints + "pt");

            //このアイテムを削除
            Destroy(gameObject);
        }
    }
}