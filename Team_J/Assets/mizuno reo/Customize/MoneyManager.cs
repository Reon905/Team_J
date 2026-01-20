using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int money = 0; // 現在のお金
    /// <summary>
    /// マネーを次のシーンに渡す
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されない
        }
        else
        {
            Destroy(gameObject); // 既にInstanceがあれば重複削除
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }
}
