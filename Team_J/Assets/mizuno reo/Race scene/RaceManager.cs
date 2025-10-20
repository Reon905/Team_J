using TMPro;           // TextMeshProを使うために必要
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;  // シングルトンインスタンス

    // UI表示用のTextMeshProUGUIをInspectorでセット
    public TextMeshProUGUI messageText;

    private float raceStartTime;          // レース開始時間を記録する変数
    private bool raceOngoing = false;     // レース中かどうかのフラグ

    // Awakeはオブジェクト生成時に呼ばれるメソッド
    void Awake()
    {
        // シングルトンのセットアップ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン切り替え時に破棄しない
        }
        else
        {
            Destroy(gameObject);           // すでにInstanceがあれば自分は破棄
        }

        // 最初はメッセージ非表示にしておく
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    // 画面に文字を表示する関数（duration秒だけ表示）
    public void ShowMessage(string message, float duration = 2f)
    {
        // すでに表示中のメッセージがあれば一旦止める
        StopAllCoroutines();
        StartCoroutine(DisplayMessageCoroutine(message, duration));
    }

    // 指定秒数だけ文字を表示し、その後非表示にするコルーチン
    private System.Collections.IEnumerator DisplayMessageCoroutine(string message, float duration)
    {
        messageText.text = message;               // 表示する文字をセット
        messageText.gameObject.SetActive(true);  // テキストを画面に表示

        yield return new WaitForSeconds(duration); // 指定秒数待つ

        messageText.gameObject.SetActive(false); // テキストを非表示にする
    }

    // レース開始処理
    public void StartRace()
    {
        raceStartTime = Time.time;    // 現在時間を記録（レース開始時間）
        raceOngoing = true;           // レース中フラグを立てる
        ShowMessage("スタート！");    // スタートメッセージを表示
        Debug.Log("レース開始: " + raceStartTime);
    }

    // レース終了処理
    public void FinishRace()
    {
        if (!raceOngoing) return;     // レース中でなければ何もしない

        float finishTime = Time.time - raceStartTime; // 経過時間を計算
        raceOngoing = false;          // レース終了フラグを立てる
        ShowMessage("ゴール！");      // ゴールメッセージを表示
        Debug.Log("レース終了！タイム: " + finishTime + "秒");
    }
}
