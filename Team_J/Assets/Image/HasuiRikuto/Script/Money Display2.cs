using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay2 : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    void Start()
    {
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        moneyText.text = $"{totalMoney}‰~";

    }
}
