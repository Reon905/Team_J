using UnityEngine;

public class Item2 : MonoBehaviour
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
        price = Random.Range(1000, 2001);
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // totalMoney += price;
           // totalPoints += 20;
            itemCount++;

            Money.Instance.DayMoney += price;
            Money.Instance.DayPoint += 40;

            Money.Instance.SceneMoney += price;
            Money.Instance.ScenePoint += 40;
            Money.Instance.SceneItemCount++;

            Debug.Log("アイテム2取得！ +" + price + "円、+20pt");

            SoundPlayer.instance.PlaySE(itemSound);

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