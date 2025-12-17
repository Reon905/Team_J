using UnityEngine;

public class Item9 : MonoBehaviour
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;
    public static int itemCount = 0;

    private ItemManager itemManager;

    private AudioSource audioSource;
    public AudioClip itemSound;

    void Start()
    {
        price = 30000;
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalMoney += price;
            totalPoints += 20;
            itemCount++;

            Money.DayMoney += price;
            Money.DayPoint += 40;

            Money.SceneMoney += price;
            Money.ScenePoint += 40;
            Money.SceneItemCount++;

            Debug.Log("アイテム4取得！ +" + price + "円、+70pt");

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
        itemCount = 0;
    }
}