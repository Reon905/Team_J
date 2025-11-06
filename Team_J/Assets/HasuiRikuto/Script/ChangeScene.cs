using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nextSceneName = "";      // 次のシーン名
    public bool resetDataOnLoad = true;    // データをリセットするか（Inspectorで切り替え可能）

    void Update()
    {
        // Enterキーが押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (resetDataOnLoad)
            {
                //PlayerPrefs（アイテム取得情報）リセット
                PlayerPrefs.DeleteAll();

                //アイテム合計金額とポイントをリセット
                Item.totalMoney = 0;
                Item.totalPoints = 0;
                Item.itemCount = 0;

                Item2.totalMoney = 0;
                Item2.totalPoints = 0;
                Item2.itemCount = 0;

                Item3.totalMoney = 0;
                Item3.totalPoints = 0;
                Item3.itemCount = 0;

                Item4.totalMoney = 0;
                Item4.totalPoints = 0;
                Item4.itemCount = 0;

                Item5.totalMoney = 0;
                Item5.totalPoints = 0;
                Item5.itemCount = 0;

                Item6.totalMoney = 0;
                Item6.totalPoints = 0;
                Item6.itemCount = 0;

                Item7.totalMoney = 0;
                Item7.totalPoints = 0;
                Item7.itemCount = 0;

                Item8.totalMoney = 0;
                Item8.totalPoints = 0;
                Item8.itemCount = 0;

                Item9.totalMoney = 0;
                Item9.totalPoints = 0;
                Item9.itemCount = 0;

                Item10.totalMoney = 0;
                Item10.totalPoints = 0;
                Item10.itemCount = 0;

                Item11.totalMoney = 0;
                Item11.totalPoints = 0;
                Item11.itemCount = 0;

                Item12.totalMoney = 0;
                Item12.totalPoints = 0;
                Item12.itemCount = 0;

                Money.totalMoney = 0;
                Money.totalPoints = 0;

                //タイマーもリセット
                ChangeSceneAfterTime.timer = 0f;

                Debug.Log("全データをリセットしました（アイテム・金額・時間）");
            }

            //シーン切り替え
            SceneManager.LoadScene(nextSceneName);
        }
    }
}