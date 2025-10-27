using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public int money = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ƒV[ƒ“‚ð‚Ü‚½‚¢‚Å‚àŽc‚é
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
        }
    }
}
