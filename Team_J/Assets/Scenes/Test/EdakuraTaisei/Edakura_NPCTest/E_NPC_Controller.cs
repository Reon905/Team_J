using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class NPC_Constants
{
    public static float DEFAULT_SIGHT_ANGLE = 30.0f;    //NPCの視界範囲
    public static float DEFAULT_DETECTION_VALUE = 0.0f; //初期発覚値
    public static float MAX_DETECTION_VALUE = 3.5f;     //最大発覚値
}
public class E_NPC_Controller : MonoBehaviour
{
    // 視界の対象とするレイヤー（Playerや障害物など）
    public LayerMask m_TargetLayer; // これを設定することにより、自身(NPC)のコライダーに反応しなくなる

    public float m_fSightAngle;    // 前方視界範囲
    public float Detection_Value;  // 発覚値(視界内に入ると上昇)

    private Vector2 posDelta;        // NPCからプレイヤーへのベクトル

    Rigidbody2D NPC_rbody;

    private float TargetAngle;    // Playerへの角度      TargetAngleとChaseAngleを統一する
    private float TimeElapsed;    // 経過時間
    private float TimeOut;        // 実行間隔


    public Transform[] patrolPoints;    // 巡回地点を格納する配列
    public enum NPC_State { Patrol, Chase };

    public float P_moveSpeed = 2f;      // Patrol移動速度
    public float P_waitTime = 1f;       // Patrol待機時間
    public float TurnSpeed = 1.5f;      // 旋回速度

    private int currentPointIndex = 0;    // 次の目的地を示すインデックス

    [SerializeField] float NPC_Speed = 2.0f; // 敵の追跡速度

    private float ChaseTargetAngle;     // Chase時のプレイヤーへの角度
    private float PatrolTargetAngle;    // WayPointへの角度
    private Vector2 Patrolvec;          // Patrol向き用の変数

    NavMeshAgent2D agent;               //NavMeshAgent2Dを使用するための変数
    [SerializeField] Transform target;  //追跡するターゲット


    // 初期状態をPatrolにしておく
    public NPC_State _state = NPC_State.Patrol;

    private void Start()
    {


        NPC_rbody = GetComponent<Rigidbody2D>();


        m_fSightAngle = NPC_Constants.DEFAULT_SIGHT_ANGLE;
        Detection_Value = NPC_Constants.DEFAULT_DETECTION_VALUE;
        TimeOut = 0.02f;

        // 巡回を開始するコルーチンを呼び出す
        StartCoroutine(PatrolRoutine(GetTransform()));

        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
    }

    private void Update()
    {
        // タイム加算
        TimeElapsed += Time.deltaTime;

        // 状態がChaseの場合
        if (_state == NPC_State.Chase)
        {
            //// Playerへの角度を求める
            //ChaseTargetAngle =  Mathf.Atan2(posDelta.y, posDelta.x) * Mathf.Rad2Deg;
            //// Quaternion.EulerでPlayerの方向を向く
            //transform.rotation = Quaternion.Euler(0, 0, ChaseTargetAngle);
            //// プレイヤーへの移動
            //NPC_rbody.linearVelocity = posDelta.normalized * NPC_Speed;

            agent.destination = target.position; //agentの目的地をtargetの座標にする
        }
    }

    // NPCのTransformコンポーネントを返す関数（PatrolRoutineの呼び出しに使う）
    private Transform GetTransform()
    {
        return transform;
    }

    // NPCの巡回
    private IEnumerator PatrolRoutine(Transform transform)
    {
        while (true)
        {
            Debug.Log("目的地を設定");
            // 目的地を設定
            Transform targetPoint = patrolPoints[currentPointIndex];


            // 目的地へのベクトルを求める
            Patrolvec = (targetPoint.position - transform.position);
            Debug.Log("ベクトルを求める"); 
    
            // 目的地への角度を求める（ラジアンを角度に変換）
            PatrolTargetAngle = Mathf.Atan2(Patrolvec.y, Patrolvec.x) * Mathf.Rad2Deg;
            Debug.Log("角度を求める");

            // Quaternion.Eulerで目的地への角度を向く
            Quaternion TargetRotation = Quaternion.Euler(0,0, PatrolTargetAngle);
            Debug.Log("目的地への角度を向く");

            

            // 目的地に近づくまで移動し続ける
            while (Vector2.Distance(transform.position, targetPoint.position) > 0.1f)
            {
                // 状態がChaseの場合
                if (_state == NPC_State.Chase)
                {
                    NPC_rbody.linearVelocity = Vector2.zero; // 念のため停止
                    yield break;
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Time.deltaTime * TurnSpeed);

                // Rigidbodyでの移動に変更
                Vector2 direction = (targetPoint.position - transform.position).normalized;
                NPC_rbody.linearVelocity = direction * P_moveSpeed;
                Debug.Log("目的地へ移動中");

                yield return null;
            }

            // 目的地に到着したら停止
            NPC_rbody.linearVelocity = Vector2.zero;
            P_waitTime = Random.Range(0.5f, 3.0f);

            Debug.Log("到着 一定時間停止");
            // 目的地に到着したら一定時間待機
            yield return new WaitForSeconds(P_waitTime);

            Debug.Log("次の目的地を設定");
            // 次の目的地を設定（最後の地点に達したら最初の地点に戻る）
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

        }
        
    }

    // NPCの視界判定
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))     // PlayerタグのColliderだけが真
        {
            //
            if (TimeElapsed >= TimeOut)
            {
                RaycastHit2D hit;   // rayが当たったコライダー判別用

                // 視野の設定
                posDelta = other.transform.position - this.transform.position;  // NPCからPlayerへの方向ベクトル
                TargetAngle = Vector2.Angle(this.transform.right, posDelta);    // NPCからPlayerの角度
                                                                                // PlayerがNPCの視界に入っているか確認（障害物は無視）
                if (TargetAngle < m_fSightAngle)     // targetAngleがm_SightAngleに収まっているかどうか
                {
                    // Rayを飛ばして、間に障害物がないかを判定する
                    //                                      始点                 方向        プレイヤーまでの距離　  感知レイヤー　
                    if (hit = Physics2D.Raycast(this.transform.position, posDelta.normalized, posDelta.magnitude, m_TargetLayer))
                    {
                        // プレイヤーが視界内にいる時の処理
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            Detection_Value += 0.1f;    // 発覚値を上昇させる
                            Debug.Log("発覚値上昇");
                            // 発覚値がMAX_DETECTION_VALUEを超えたら
                            if (Detection_Value > NPC_Constants.MAX_DETECTION_VALUE)
                            {
                                Detection_Value = 0.0f;     // 発覚値を0に

                                StopCoroutine(PatrolRoutine(GetTransform()));   //パトロール停止
                                _state = NPC_State.Chase;      // 状態をChaseに切り替え

                                Debug.Log("障害物なし、視界範囲内");
                            }
                        }
                        else if (hit.collider == null)
                        {
                            Debug.Log("なんもなし");
                        }
                        else    // 障害物がある場合
                        {
                            // Debug.Log("障害物あり" + hit.collider.name);
                        }
                    }
                }
                // Timeを0にする
                TimeElapsed = 0.0f;
            }
        }
    }

    // 視界範囲からPlayerが抜けた瞬間の処理
    private void OnTriggerExit2D(Collider2D other)
    {
        // 抜けたColliderのタグがPlayerの場合
        if (other.CompareTag("Player"))
        {
            if (TargetAngle > m_fSightAngle)
            {
                Detection_Value = 0.0f;     //Detection_Valueを0.0fにする

            }
        }
    }

    // 衝突した瞬間の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手のタグが "Player" だったら
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_state == NPC_State.Patrol)         // 状態がPatrolの場合
            {
                // 接触した時の処理
                Debug.Log("Playerと接触");

                StopCoroutine(PatrolRoutine(GetTransform())); // 巡回停止
                _state = NPC_State.Chase;       // 状態をChaseに切り替え
            }
            else if (_state == NPC_State.Chase)     // 状態がChaseの場合
            {
                // Chase中に衝突時、追跡を終了
                _state = NPC_State.Patrol;
                // Velocityを0にして速度をなくす(巡回時の停止時に移動してしまうため)
                NPC_rbody.linearVelocity = Vector2.zero;
                // 念のためコルーチンを停止
                StopCoroutine(PatrolRoutine(GetTransform()));
                // 巡回を開始するコルーチンを開始
                StartCoroutine(PatrolRoutine(GetTransform()));
            }

        }
    }


}
