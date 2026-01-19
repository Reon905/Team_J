using UnityEngine;

public class Item : BaseItem
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;
    //public static int itemCount = 0;

    private ItemManager itemManager;
    private AudioSource audioSource;  
    public AudioClip itemSound;       

    void Start()
    {
        price = Random.Range(2000, 4001);
        itemManager = GetComponent<ItemManager>();

        audioSource = GetComponent<AudioSource>(); // ← AudioSource取得
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //totalMoney += price;
            //totalPoints += 40;
            itemCount++;

            Money.Instance.DayMoney += price;
            Money.Instance.DayPoint += 40;

            Money.Instance.SceneMoney += price;
            Money.Instance.ScenePoint += 40;
            Money.Instance.SceneItemCount++;

            Debug.Log("アイテム1取得！ +" + price + "円、+40pt");
            Debug.Log("アイテム1取得！ +" + BaseItem.itemCount + "個");

            // 🔊 サウンド再生（Item は消えても問題なし）
            SoundPlayer.instance.PlaySE(itemSound);

            // 取得済み通知
            if (itemManager != null)
                itemManager.CollectItem();
        }
    }
    public static void Reset()
    {
        totalMoney = 0;
        totalPoints = 0;
       // itemCount = 0;
    }
}