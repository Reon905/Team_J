using UnityEngine;
using UnityEngine.UI;

public class TotalMoneyDisplay : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        //Money.AddToTotal();

        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount + Item4.itemCount + Item5.itemCount + Item6.itemCount + Item7.itemCount + Item8.itemCount + Item9.itemCount + Item10.itemCount + Item11.itemCount + Item12.itemCount;

        resultText.text = $" {Money.totalMoney}‰~\n";

        Debug.Log($"[Result] : {Money.totalMoney}‰~");
    }

}