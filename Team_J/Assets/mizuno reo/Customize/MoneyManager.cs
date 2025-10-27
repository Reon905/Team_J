using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int money = 0; // Œ»İ‚Ì‚¨‹à

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ƒV[ƒ“‚ğ‚Ü‚½‚¢‚Å‚à”jŠü‚³‚ê‚È‚¢
        }
        else
        {
            Destroy(gameObject); // Šù‚ÉInstance‚ª‚ ‚ê‚Îd•¡íœ
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
