using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private string itemID;
    private bool isCollected = false; // 取得済みかどうか

    // --- 特定スクリプトが付いている場合は例外 ---
    private bool IsExceptionByScript()
    {
        return GetComponent<Item3>() != null ||
               GetComponent<Item7>() != null ||
               GetComponent<Item8>() != null;
    }

    void Start()
    {
        // --- 例外アイテムは Destroy しない ---
        if (IsExceptionByScript())
        {
            return;
        }

        // 一意ID生成
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

        // --- 例外スクリプト付きなら Destroy しない ---
        if (IsExceptionByScript())
        {
            return;
        }

        // アイテム削除
        Destroy(gameObject);
    }
}