using UnityEngine;
using UnityEngine.UI;

public class Item7 : MonoBehaviour
{
    public int price;
    public static int totalMoney = 0;
    public static int totalPoints = 0;
    public static int itemCount = 0;

    public Sprite itemSprite;
    public Sprite openedSprite;
    private SpriteRenderer spriteRenderer;
    private ItemManager itemManager;

    private AudioSource audioSource;
    public AudioClip itemSound;

    public Text messageText;
    public float holdTime = 2.0f;
    private float holdTimer = 0f;
    private bool playerInRange = false;
    private bool isOpened = false;

   //開錠状態保存用
    private string saveKey; 
    

    void Start()
    {
        saveKey = "ItemOpened_Item7";

        //毎回閉じた状態にリセット
        PlayerPrefs.SetInt(saveKey, 0);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && itemSprite) spriteRenderer.sprite = itemSprite;
        itemManager = GetComponent<ItemManager>();

        price = 50000;
        if (messageText != null) messageText.text = "";

    }

    void Update()
    {
        if (playerInRange && !isOpened)
        {
            if (messageText != null && holdTimer <= 0)
                messageText.text = "Enterを長押しで開錠";

            if (Input.GetKey(KeyCode.Return))
            {
                holdTimer += Time.deltaTime;
                if (messageText != null) messageText.text = "取得中…";

                if (holdTimer >= holdTime)
                    OpenItem();
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                holdTimer = 0f;
                if (messageText != null) messageText.text = "Enterを長押しで開錠";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isOpened && messageText != null)
                messageText.text = "Enterを長押しで開錠";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            holdTimer = 0f;
            if (messageText != null) messageText.text = "";
        }
    }

    void OpenItem()
    {
        isOpened = true;
       // totalMoney += price;
       // totalPoints += 100;
        itemCount++;

        Money.DayMoney += price;
        Money.DayPoint += 40;

        Money.SceneMoney += price;
        Money.ScenePoint += 40;
        Money.SceneItemCount++;

        SoundPlayer.instance.PlaySE(itemSound);

        if (spriteRenderer && openedSprite) spriteRenderer.sprite = openedSprite;
        if (messageText != null) messageText.text = "取得完了！";

        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
        

        if (itemManager != null)
            itemManager.CollectItem(); // 取得済み登録＆削除
    }
    public static void Reset()
    {
        totalMoney = 0;
        totalPoints = 0;
        itemCount = 0;
    }
}