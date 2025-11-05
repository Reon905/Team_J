using UnityEngine;

public class Customize : MonoBehaviour
{
    public static int CarStage = 0;
    public static float selectedAcceleration = 5f;
    public static float selectedMaxSpeed = 20f;

    int totalMoney;

    private void Start()
    {
        totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;

        if (totalMoney < 3000)
        {
            CarStage = 0;
            selectedAcceleration = 5f;
            selectedMaxSpeed = 20f;
        }
        else if (totalMoney >= 3000 && totalMoney < 4000)
        {
            CarStage = 1;
            selectedAcceleration = 5.2f;
            selectedMaxSpeed = 22f;
        }
        else if (totalMoney >= 4000 && totalMoney < 5000)
        {
            CarStage = 2;
            selectedAcceleration = 5.4f;
            selectedMaxSpeed = 24f;
        }
        else if (totalMoney >= 5000 && totalMoney < 6000)
        {
            CarStage = 3;
            selectedAcceleration = 5.6f;
            selectedMaxSpeed = 26f;
        }
        else if (totalMoney >= 6000 && totalMoney < 8000)
        {
            CarStage = 4;
            selectedAcceleration = 5.8f;
            selectedMaxSpeed = 28f;
        }
        else if (totalMoney >= 8000 && totalMoney < 10000)
        {
            CarStage = 5;
            selectedAcceleration = 6f;
            selectedMaxSpeed = 30f;
        }
        else if (totalMoney >= 10000 && totalMoney < 20000)
        {
            CarStage = 6;
            selectedAcceleration = 6.2f;
            selectedMaxSpeed = 32f;
        }
        else if (totalMoney >= 20000)
        {
            CarStage = 7;
            selectedAcceleration = 6.4f;
            selectedMaxSpeed = 34f;
        }

        Debug.Log($"CarStage: {CarStage}, Accel: {selectedAcceleration}, MaxSpeed: {selectedMaxSpeed}");
    }
}
