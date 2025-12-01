using UnityEngine;

public class Customize : MonoBehaviour
{
    public static int CarStage = 0;
    public static float selectedAcceleration = 5f;
    public static float selectedMaxSpeed = 20f;
    public static int  nextLevel = 0;
    public static 
    int totalMoney;


    private void Awake()
    {
        totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney + Item4.totalMoney + Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney + Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;

        if (totalMoney < 3000)
        {
            CarStage = 0;
            selectedAcceleration = 5f;
            selectedMaxSpeed = 20f;
            nextLevel = 3000;
        }
        else if (totalMoney >= 3000 && totalMoney < 4000)
        {
            CarStage = 1;
            selectedAcceleration = 5.2f;
            selectedMaxSpeed = 22f;
            nextLevel = 4000;
        }
        else if (totalMoney >= 4000 && totalMoney < 5000)
        {
            CarStage = 2;
            selectedAcceleration = 5.4f;
            selectedMaxSpeed = 24f;
            nextLevel = 5000;
        }
        else if (totalMoney >= 5000 && totalMoney < 6000)
        {
            CarStage = 3;
            selectedAcceleration = 5.6f;
            selectedMaxSpeed = 26f;
            nextLevel = 6000;
        }
        else if (totalMoney >= 6000 && totalMoney < 8000)
        {
            CarStage = 4;
            selectedAcceleration = 5.8f;
            selectedMaxSpeed = 28f;
            nextLevel = 8000;
        }
        else if (totalMoney >= 8000 && totalMoney < 10000)
        {
            CarStage = 5;
            selectedAcceleration = 6f;
            selectedMaxSpeed = 30f;
            nextLevel = 10000;
        }
        else if (totalMoney >= 10000 && totalMoney < 20000)
        {
            CarStage = 6;
            selectedAcceleration = 6.2f;
            selectedMaxSpeed = 32f;
            nextLevel = 20000;
        }
        else if (totalMoney >= 20000 && totalMoney < 30000)
        {
            CarStage = 7;
            selectedAcceleration = 6.4f;
            selectedMaxSpeed = 34f;
            nextLevel = 30000;
        }
        else if (totalMoney >= 30000 && totalMoney < 40000)
        {
            CarStage = 8;
            selectedAcceleration = 6.6f;
            selectedMaxSpeed = 36f;
            nextLevel = 40000;
        }
        else if (totalMoney >= 40000 && totalMoney < 60000)
        {
            CarStage = 9;
            selectedAcceleration = 6.8f;
            selectedMaxSpeed = 38f;
            nextLevel = 60000;
        }
        else if (totalMoney >= 60000 && totalMoney < 100000)
        {
            CarStage = 10;
            selectedAcceleration = 7.0f;
            selectedMaxSpeed = 40f;
            nextLevel = 100000;
        }
        else if (totalMoney >= 100000 && totalMoney < 120000)
        {
            CarStage = 11;
            selectedAcceleration = 7.2f;
            selectedMaxSpeed = 40f;
            nextLevel = 120000;
        }
        else if (totalMoney >= 120000 && totalMoney < 150000)
        {
            CarStage = 12;
            selectedAcceleration = 7.4f;
            selectedMaxSpeed = 44f;
            nextLevel = 150000;
        }
        else if (totalMoney >= 150000 && totalMoney < 230000)
        {
            CarStage = 13;
            selectedAcceleration = 7.6f;
            selectedMaxSpeed = 46f;
            nextLevel = 230000;
        }
        else if (totalMoney >= 230000 && totalMoney < 350000)
        {
            CarStage = 14;
            selectedAcceleration = 7.8f;
            selectedMaxSpeed = 48f;
            nextLevel = 350000;
        }
        else if (totalMoney >= 350000)
        {
            CarStage = 14;
            selectedAcceleration = 8.0f;
            selectedMaxSpeed = 50f;
            nextLevel++;
        }

        Debug.Log($"CarStage: {CarStage}, Accel: {selectedAcceleration}, MaxSpeed: {selectedMaxSpeed}");
    }
}
