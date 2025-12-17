using UnityEngine;

public class Item6 : MonoBehaviour
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
        price = 5000;
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // totalMoney += price;
           // totalPoints += 40;
            itemCount++;

            Money.Instance.DayMoney += price;
            Money.Instance.DayPoint += 40;

            Money.Instance.SceneMoney += price;
            Money.Instance.ScenePoint += 40;
            Money.Instance.SceneItemCount++; 

            Debug.Log("アイテム4取得！ +" + price + "円、+40pt");

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