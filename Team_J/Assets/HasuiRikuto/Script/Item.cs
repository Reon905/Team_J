using UnityEngine;

public class Item : MonoBehaviour
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;
    public static int itemCount = 0;

    private ItemManager itemManager;

    void Start()
    {
        price = Random.Range(2000, 4001);
        itemManager = GetComponent<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalMoney += price;
            totalPoints += 40;
            itemCount++;

            Debug.Log("アイテム1取得！ +" + price + "円、+40pt");

            // ここでItemManagerに「取得済み」と伝える
            if (itemManager != null)
                itemManager.CollectItem();
        }
    }
}
