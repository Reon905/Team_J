using UnityEngine;
using UnityEngine.UI;

public class SuccessAmountDisplay : MonoBehaviour
{
    [SerializeField] private Text moneyText;


    void Start()
    {
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney + Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney + Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;
        int totalPoint = Money.Instance.totalPoints;
        moneyText.text = $"èäéùã‡ {totalMoney}â~\n" +
                         $"çáåvPt {Money.Instance.totalPoints}pt" ;

    }
}
