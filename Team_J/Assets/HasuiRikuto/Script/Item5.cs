using UnityEngine;

public class Item5 : MonoBehaviour
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
        price = 10000;
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalMoney += price;
            totalPoints += 50;
            itemCount++;

            Debug.Log("アイテム5取得！ +" + price + "円、+50pt");

            SoundPlayer.instance.PlaySE(itemSound);

            // ここでItemManagerに「取得済み」と伝える
            if (itemManager != null)
                itemManager.CollectItem();
        }
    }
}