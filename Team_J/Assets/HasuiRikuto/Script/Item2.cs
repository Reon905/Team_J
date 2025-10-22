using UnityEngine;

public class Item2 : MonoBehaviour
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;
    public static int itemCount = 0;

    private ItemManager itemManager;

    void Start()
    {
        price = Random.Range(1000, 2001);
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalMoney += price;
            totalPoints += 20;
            itemCount++;

            Debug.Log("アイテム2取得！ +" + price + "円、+20pt");

            if (itemManager != null)
                itemManager.CollectItem();
        }
    }
}