using UnityEngine;

public class Item : MonoBehaviour
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
        //2000円〜4000円の範囲でランダムな値を設定
        price = Random.Range(2000, 4001);
        Debug.Log("アイテムの金額は " + price + " 円です。");
    }

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーに触れたら処理
        if (other.CompareTag("Player"))
        {
            //金額を加算
            totalMoney += price;

            //固定の40ポイントを加算
            totalPoints += 40;

            itemCount++;

            Debug.Log("アイテムを取得！ 金額 +" + price + "円、ポイント +60pt");
            Debug.Log("現在の合計：金額 " + totalMoney + "円 ／ ポイント " + totalPoints + "pt");

            // このアイテムを削除
            Destroy(gameObject);
        }
    }
}