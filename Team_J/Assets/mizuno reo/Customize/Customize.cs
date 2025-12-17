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
        //lMoney + Item5.totalMoney + Item6.totalMoney + Item7.totalMoney + Item8.totalMoney + Item9.totalMoney + Item10.totalMoney + Item11.totalMoney + Item12.totalMoney;

        if (Money.Instance.totalMoney < 3000)
        {
            CarStage = 0;
            selectedAcceleration = 5f;
            selectedMaxSpeed = 20f;
            nextLevel = 3000;
        }
        else if (Money.Instance.totalMoney >= 3000 && Money.Instance.totalMoney < 4000)
        {
            CarStage = 1;
            selectedAcceleration = 5.2f;
            selectedMaxSpeed = 22f;
            nextLevel = 4000;
        }
        else if (Money.Instance.totalMoney >= 4000 && Money.Instance.totalMoney < 5000)
        {
            CarStage = 2;
            selectedAcceleration = 5.4f;
            selectedMaxSpeed = 24f;
            nextLevel = 5000;
        }
        else if (Money.Instance.totalMoney >= 5000 && Money.Instance.totalMoney < 6000)
        {
            CarStage = 3;
            selectedAcceleration = 5.6f;
            selectedMaxSpeed = 26f;
            nextLevel = 6000;
        }
        else if (Money.Instance.totalMoney >= 6000 && Money.Instance.totalMoney < 8000)
        {
            CarStage = 4;
            selectedAcceleration = 5.8f;
            selectedMaxSpeed = 28f;
            nextLevel = 8000;
        }
        else if (Money.Instance.totalMoney >= 8000 && Money.Instance.totalMoney < 10000)
        {
            CarStage = 5;
            selectedAcceleration = 6f;
            selectedMaxSpeed = 30f;
            nextLevel = 10000;
        }
        else if (Money.Instance.totalMoney >= 10000 && Money.Instance.totalMoney < 20000)
        {
            CarStage = 6;
            selectedAcceleration = 6.2f;
            selectedMaxSpeed = 32f;
            nextLevel = 20000;
        }
        else if (Money.Instance.totalMoney >= 20000 && Money.Instance.totalMoney < 30000)
        {
            CarStage = 7;
            selectedAcceleration = 6.4f;
            selectedMaxSpeed = 34f;
            nextLevel = 30000;
        }
        else if (Money.Instance.totalMoney >= 30000 && Money.Instance.totalMoney < 40000)
        {
            CarStage = 8;
            selectedAcceleration = 6.6f;
            selectedMaxSpeed = 36f;
            nextLevel = 40000;
        }
        else if (Money.Instance.totalMoney >= 40000 && Money.Instance.totalMoney < 60000)
        {
            CarStage = 9;
            selectedAcceleration = 6.8f;
            selectedMaxSpeed = 38f;
            nextLevel = 60000;
        }
        else if (Money.Instance.totalMoney >= 60000 && Money.Instance.totalMoney < 100000)
        {
            CarStage = 10;
            selectedAcceleration = 7.0f;
            selectedMaxSpeed = 40f;
            nextLevel = 100000;
        }
        else if (Money.Instance.totalMoney >= 100000 && Money.Instance.totalMoney < 120000)
        {
            CarStage = 11;
            selectedAcceleration = 7.2f;
            selectedMaxSpeed = 40f;
            nextLevel = 120000;
        }
        else if (Money.Instance.totalMoney >= 120000 && Money.Instance.totalMoney < 150000)
        {
            CarStage = 12;
            selectedAcceleration = 7.4f;
            selectedMaxSpeed = 44f;
            nextLevel = 150000;
        }
        else if (Money.Instance.totalMoney >= 150000 && Money.Instance.totalMoney < 230000)
        {
            CarStage = 13;
            selectedAcceleration = 7.6f;
            selectedMaxSpeed = 46f;
            nextLevel = 230000;
        }
        else if (Money.Instance.totalMoney >= 230000 && Money.Instance.totalMoney < 350000)
        {
            CarStage = 14;
            selectedAcceleration = 7.8f;
            selectedMaxSpeed = 48f;
            nextLevel = 350000;
        }
        else if (Money.Instance.totalMoney >= 350000 && Money.Instance.totalMoney < 400000)
        {
            CarStage = 15;
            selectedAcceleration = 8.0f;
            selectedMaxSpeed = 50f;
            nextLevel = 400000;
        }
        else if (Money.Instance.totalMoney >= 400000 && Money.Instance.totalMoney < 450000)
        {
            CarStage = 16;
            selectedAcceleration = 8.2f;
            selectedMaxSpeed = 52f;
            nextLevel = 450000;
        }
        else if (Money.Instance.totalMoney >= 450000 && Money.Instance.totalMoney < 500000)
        {
            CarStage = 17;
            selectedAcceleration = 8.4f;
            selectedMaxSpeed = 54f;
            nextLevel = 500000;
        }
        else if (Money.Instance.totalMoney >= 500000 && Money.Instance.totalMoney < 550000)
        {
            CarStage = 18;
            selectedAcceleration = 8.6f;
            selectedMaxSpeed = 56f;
            nextLevel = 550000;
        }
        else if (Money.Instance.totalMoney >= 550000 && Money.Instance.totalMoney < 600000)
        {
            CarStage = 19;
            selectedAcceleration = 8.8f;
            selectedMaxSpeed = 58f;
            nextLevel = 600000;
        }
        else if (Money.Instance.totalMoney >= 600000 && Money.Instance.totalMoney < 700000)
        {
            CarStage = 20;
            selectedAcceleration = 8.8f;
            selectedMaxSpeed = 58f;
            nextLevel = 700000;
        }
        else if (Money.Instance.totalMoney >= 700000 && Money.Instance.totalMoney < 750000)
        {
            CarStage = 21;
            selectedAcceleration = 9.0f;
            selectedMaxSpeed = 60f;
            nextLevel = 750000;
        }
        else if (Money.Instance.totalMoney >= 750000 && Money.Instance.totalMoney < 800000)
        {
            CarStage = 22;
            selectedAcceleration = 9.2f;
            selectedMaxSpeed = 62f;
            nextLevel = 800000;
        }
        else if (Money.Instance.totalMoney >= 800000 && Money.Instance.totalMoney < 850000)
        {
            CarStage = 23;
            selectedAcceleration = 9.4f;
            selectedMaxSpeed = 64f;
            nextLevel = 850000;
        }
        else if (Money.Instance.totalMoney >= 850000 && Money.Instance.totalMoney < 900000)
        {
            CarStage = 24;
            selectedAcceleration = 9.6f;
            selectedMaxSpeed = 66f;
            nextLevel = 900000;
        }
        else if (Money.Instance.totalMoney >= 900000 && Money.Instance.totalMoney < 950000)
        {
            CarStage = 25;
            selectedAcceleration = 9.6f;
            selectedMaxSpeed = 66f;
            nextLevel = 950000;
        }
        else if (Money.Instance.totalMoney >= 950000 && Money.Instance.totalMoney < 1000000)
        {
            CarStage = 26;
            selectedAcceleration = 9.8f;
            selectedMaxSpeed = 68f;
            nextLevel =     1000000;
        }
        else if (Money.Instance.totalMoney >= 1000000)
        {
            CarStage = 27;
            selectedAcceleration = 10.0f;
            selectedMaxSpeed = 70f;
            nextLevel ++;
        }

        Debug.Log($"CarStage: {CarStage}, Accel: {selectedAcceleration}, MaxSpeed: {selectedMaxSpeed}");
    }
}
