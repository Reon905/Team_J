using UnityEngine;

public class Item2 : BaseItem
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;

    private ItemManager itemManager;
    private AudioSource audioSource;
    public AudioClip itemSound;

    void Start()
    {
        price = Random.Range(1000, 2001);//金額設定
        itemManager = GetComponent<ItemManager>();
    }

    /// <summary>
    /// 金額ポイントアイテムカウント設定・サウンド再生関数
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            itemCount++;

            Money.Instance.DayMoney += price;
            Money.Instance.DayPoint += 40;

            Money.Instance.SceneMoney += price;
            Money.Instance.ScenePoint += 40;
            Money.Instance.SceneItemCount++;

            Debug.Log("アイテム2取得！ +" + price + "円、+20pt");
            Debug.Log("アイテム1取得！ +" + BaseItem.itemCount + "個");
            SoundPlayer.instance.PlaySE(itemSound);

            if (itemManager != null)
                itemManager.CollectItem();
        }
    }
    public static void Reset()
    {
        totalMoney = 0;
        totalPoints = 0;
    }
}