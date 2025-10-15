using UnityEngine;

public class DragRaseCar : MonoBehaviour
{
    public float acceleration = 20f;       //加速度の速さ
    public float maxSpeed = 50f;           //最高速度
    public float heatIncreaseRate = 30f;   //ゲージが増加する速さ(一秒あたり)
    public float heatDecreaseRate = 15f;   //ゲージが減少する速さ(一秒あたり)
    public float macHeat = 100f;           //ヒートゲージの最大値
    public float overheatCooldwnTime = 3f; //オーバーヒート時のクールダウン時間
    public float currentSpeed = 0f;        //現在の速度
    public float currentHeat = 0f;         //現在のヒートゲージの値
    private bool isOverheated = false;       //オーバーヒート状態かどうか
    private float cooldownTimer = 0f;      //クールダウンの残り時間

    private void Update()
    {
        if (isOverheated)
        {
            //オーバーヒート中の処理

            //クールダウンタイマーを減らす
            cooldownTimer -= Time.deltaTime;

            //ヒートゲージを徐々に減らす(冷却)
            currentHeat = Mathf.Max(0, currentHeat - heatDecreaseRate * Time.deltaTime);

            //クールダウンが終わったらオーバーヒート解除
            if (cooldownTimer <= 0f)
            {
                isOverheated = false;
                currentHeat = 0f;  //熱をリセット
            }

            //オーバーヒート中は減速させる
            currentSpeed = Mathf.Max(0, currentSpeed - acceleration * Time.deltaTime);
        }
        else
        {
            //通常時の処理

            if (Input.GetKeyUp(KeyCode.W))     //キー入力
            {
                //アクセルボタン押し込み中

                //速度を加速させる(最大速度まで)
                currentSpeed = Mathf.Min(maxSpeed, currentSpeed + acceleration * Time.deltaTime);

                //ヒートゲージが満タンになったらオーバーヒート判定
                if (currentHeat >= macHeat)
                {
                    isOverheated = true;
                    cooldownTimer = overheatCooldwnTime;
                    Debug.Log("オーバーヒート");
                }
            }
            else
            {
                //アクセルボタンを押してないとき

                //ヒートゲージを冷却させる(減少させる)
                currentHeat = Mathf.Max(0, currentHeat - heatDecreaseRate * Time.deltaTime);

                //自然減速
                currentSpeed = Mathf.Max(0, currentSpeed - acceleration * Time.deltaTime * 2);
            }
        }

        //車を前方向(見下ろしなのでvector3.up)に速度分だけ移動させる
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
    }
}