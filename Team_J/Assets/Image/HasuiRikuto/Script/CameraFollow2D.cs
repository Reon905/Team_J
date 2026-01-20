using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("プレイヤーを自動で探す")]
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);
   
    void Start()
    {
        // 自動でプレイヤーを探す（タグがPlayerのオブジェクト）
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                Debug.Log("プレイヤーを自動設定: " + player.name);
            }
            else
            {
                Debug.LogError("プレイヤーが見つかりません。タグが 'Player' のオブジェクトを確認してください。");
            }
        }
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // プレイヤーを追従
        Vector3 targetPos = player.position + offset;
        transform.position = targetPos;

        //Debug.Log("カメラ位置更新中 → " + transform.position);
    }
}