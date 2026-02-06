using UnityEngine;
using UnityEngine.UI;

public class IronDoor : MonoBehaviour
{
    public Sprite itemSprite;
    public Sprite openedSprite;
    private SpriteRenderer spriteRenderer;
    private ItemManager itemManager;

    private AudioSource audioSource;
    public AudioClip itemSound;

    public Text messageText;
    public float Timer = 2.0f;
    public float holdTime = 2.0f;
    private float holdTimer = 0f;
    private bool playerInRange = false;
    private bool isOpened = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && itemSprite) spriteRenderer.sprite = itemSprite;
        itemManager = GetComponent<ItemManager>();
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
                Timer -= Time.deltaTime;
                if (messageText != null) messageText.text = $"残り {Timer:F1} 秒";

                if (holdTimer >= holdTime)
                    OpenItem();
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Timer = 2.0f;
                holdTimer = 0f;
                if (messageText != null) messageText.text = "Enterを長押しで開錠";
            }
        }
    }
    /// <summary>
    /// Enterを押す前の関数
    /// </summary>
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
            Timer = 2.0f;
            if (messageText != null) messageText.text = "";
        }
    }

    /// <summary>
    /// ドアが開いたときの関数
    /// </summary>
    void OpenItem()
    {
        isOpened = true;

        // コライダーの判定を無効化
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null) col.enabled = false;

        SoundPlayer.instance.PlaySE(itemSound);

        if (spriteRenderer && openedSprite) spriteRenderer.sprite = openedSprite;
        if (messageText != null) messageText.text = "開錠完了！";

        if (itemManager != null)
            itemManager.CollectItem(); // 取得済み登録＆削除
    }
}