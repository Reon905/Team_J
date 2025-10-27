using UnityEngine;

public class Customize : MonoBehaviour
{
    public static int CarStage = 0;
    //float num = PlayerCarController.acceleration;

    int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;


    private void Start()
    {
        if (totalMoney < 3000)
        {
            CarStage = 0;
            PlayerCarController.acceleration = 5f;
            PlayerCarController.maxSpeed = 20;
        }
        else if (totalMoney >= 3000 && totalMoney < 4000)
        {
            CarStage = 1;
            PlayerCarController.acceleration = 5.2f;
            PlayerCarController.maxSpeed = 22;
        }
        else if (totalMoney < 4000 && totalMoney < 5000)
        {
            CarStage = 2;
            PlayerCarController.acceleration = 5.4f;
            PlayerCarController.maxSpeed = 24;
        }
        else if (totalMoney >= 5000 && totalMoney < 6000)
        {
            CarStage = 3;
            PlayerCarController.acceleration = 5.6f;
            PlayerCarController.maxSpeed = 26;
        }
        else if (totalMoney >= 6000 && totalMoney < 8000)
        {
            CarStage = 4;
            PlayerCarController.acceleration = 5.8f;
            PlayerCarController.maxSpeed = 28;
        }
        else if (totalMoney >= 8000 && totalMoney < 10000)
        {
            CarStage = 5;
            PlayerCarController.acceleration = 6f;
            PlayerCarController.maxSpeed = 30;
        }
        else if (totalMoney >= 10000 && totalMoney < 20000)
        {
            CarStage = 6;
            PlayerCarController.acceleration = 6.2f;
            PlayerCarController.maxSpeed = 32;

        }
        else if (totalMoney >= 20000)
        {
            CarStage = 7;
            PlayerCarController.acceleration = 6.4f;
            PlayerCarController.maxSpeed = 34;
        }

        Debug.Log(PlayerCarController.acceleration);
        Debug.Log(PlayerCarController.maxSpeed);
    }
}
