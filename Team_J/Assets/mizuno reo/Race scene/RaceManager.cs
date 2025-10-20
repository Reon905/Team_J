using TMPro;           // TextMeshProの名前空間
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // シングルトンインスタンス（どこからでもアクセス可能に）
    public static RaceManager Instance;

    // 文字表示用TextMeshProUGUIをInspectorでセット
    public TextMeshProUGUI messageText;

    private float raceStartTime;   // レース開始時間を記録
    private bool raceOngoing = false;  // レース中かどうかのフラグ

    void Awake()
    {
        // シングルトンのセットアップ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーン切り替え時に破棄しない
        }
        else
        {
            Destroy(gameObject);  // 2つ以上あったら自分を破棄
        }

        // 最初は文字を非表示にしておく
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    // 文字を画面に表示するメソッド（duration秒だけ表示）
    public void ShowMessage(string message, float duration = 2f)
    {
        // すでにコルーチンが動いていたら停止

        StopAllCoroutines();
        StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // コルーチン：文字を表示して一定時間後に非表示にする
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        messageText.text = message;               // メッセージをセット
        messageText.gameObject.SetActive(true);  // 表示ON

        yield return new WaitForSeconds(duration);  // duration秒待つ

        messageText.gameObject.SetActive(false);    // 表示OFF
    }

    // レース開始時の処理
    public void Start()
    {
        raceStartTime = Time.time;    // 現在時間を保存
        raceOngoing = true;           // レース中フラグON
        ShowMessage("Ready,Go");    // スタートメッセージ表示
        Debug.Log("レース開始: " + raceStartTime);
    }

    // レース終了時の処理
    public void Finish()
    {
        if (!raceOngoing) return;     // レース中でなければ処理しない

        float finishTime = Time.time - raceStartTime;  // 経過時間計算
        raceOngoing = false;          // レース中フラグOFF
        ShowMessage("Finish！！");      // ゴールメッセージ表示
        Debug.Log("レース終了！タイム: " + finishTime + "秒");
    }
}
