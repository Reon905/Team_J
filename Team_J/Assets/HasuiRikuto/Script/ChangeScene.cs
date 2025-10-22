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

                //タイマーもリセット
                ChangeSceneAfterTime.timer = 0f;

                Debug.Log("全データをリセットしました（アイテム・金額・時間）");
            }

            //シーン切り替え
            SceneManager.LoadScene(nextSceneName);
        }
    }
}