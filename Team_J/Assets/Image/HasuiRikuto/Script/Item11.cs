using UnityEngine;

public class Item11 : BaseItem
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;

    private ItemManager itemManager;
    private AudioSource audioSource;
    public AudioClip itemSound;

    void Start()
    {
        price = 10000;//金額設定
        itemManager = GetComponent<ItemManager>();

        audioSource = GetComponent<AudioSource>();
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

            Debug.Log("アイテム4取得！ +" + price + "円、+70pt");
            Debug.Log("アイテム1取得！ +" + BaseItem.itemCount + "個");
            SoundPlayer.instance.PlaySE(itemSound);

            // ここでItemManagerに「取得済み」と伝える
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