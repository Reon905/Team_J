using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private string itemID;
    private bool isCollected = false; // 取得済みかどうか

    void Start()
    {
        // 一意ID生成（シーン名＋オブジェクト名＋位置）
        itemID = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_" + transform.position.ToString();

        // 取得済みなら非表示
        if (PlayerPrefs.GetInt(itemID, 0) == 1)
        {
            Destroy(gameObject);
            isCollected = true;
        }
    }

    public void CollectItem()
    {
        if (isCollected) return;

        isCollected = true;

        // 記録
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();

        // アイテム削除
        Destroy(gameObject);
    }
}