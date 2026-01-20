using UnityEngine;

public class MPReset : MonoBehaviour
{
  
   
    public bool resetDataOnLoad = true;    // データをリセットするか（Inspectorで切り替え可能）

    void Update()
    {
       
           if (resetDataOnLoad)
           {
           
            PlayerPrefs.DeleteAll();

            //タイマーリセット
            ChangeSceneAfterTime.timer = 0f;

                Debug.Log("リセットしました（アイテム・時間）");
           }
    }
}

