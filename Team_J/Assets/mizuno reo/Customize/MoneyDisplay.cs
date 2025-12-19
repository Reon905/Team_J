using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    void Start()
    {
        //int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney + Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney + Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;
        moneyText.text = $"èäéùã‡: {Money.Instance.DayMoney}â~";
        Debug.Log(Money.Instance.DayMoney);
    }
}
