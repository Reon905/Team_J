using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class E_Indoor_NPC_Controller : MonoBehaviour
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



    public enum NPC_State { Patrol, Chase };

    public float P_moveSpeed = 2f;      // Patrol移動速度
    public float P_waitTime = 2f;       // Patrol待機時間
    public float TurnSpeed = 1.5f;      // 旋回速度

    private int currentPointIndex = 0;    // 次の目的地を示すインデックス
    private bool isWaiting = false; // 停止中フラグ

    [SerializeField] float Chase_Speed = 2.0f; // 敵の追跡速度

    NavMeshAgent2D agent;               //NavMeshAgent2Dを使用するための変数
    [SerializeField] Transform target;  //追跡するターゲット
    public Transform[] patrolPoints;    // 巡回地点を格納する配列


    // 初期状態をPatrolにしておく
    public NPC_State _state = NPC_State.Patrol;

    private void Start()
    {
        NPC_rbody = GetComponent<Rigidbody2D>();

        //各種変数を初期化
        m_fSightAngle = Constants.DEFAULT_SIGHT_ANGLE;
        Detection_Value = Constants.DEFAULT_DETECTION_VALUE;
        TimeOut = 0.02f;

        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
        agent.speed = P_moveSpeed;              //巡回速度に合わせる
    }

    private void Update()
    {
        // タイム加算
        TimeElapsed += Time.deltaTime;

        // 状態がPatrolの場合
        if (_state == NPC_State.Patrol)
        {
            //パトロール関数の予備出し
            PatrolUpdate();
        }//Chaseの場合
        else if (_state == NPC_State.Chase)
        {
            //Chase関数の呼び出し
            ChaseUpdate();
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
                            if (Detection_Value > Constants.MAX_DETECTION_VALUE)
                            {
                                Detection_Value = 0.0f;     // 発覚値を0に

                                //Playerの状態をDetectionにする
                                GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                                Debug.Log("Detection!!!");

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
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 衝突した相手のタグが "Player" だったら
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_state == NPC_State.Patrol)         // 状態がPatrolの場合
            {
                // 接触した時の処理
                Debug.Log("Playerと接触");
                //Playerの状態をDetectionに切り替え
                GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                _state = NPC_State.Chase;       // 状態をChaseに切り替え
            }
            else if (_state == NPC_State.Chase)     // 状態がChaseの場合
            {
                //Chase中に衝突したらSceneを切り替える
                SceneManager.LoadScene("Caught Scene");
                //次のプレイのためにPlayerの状態をNoDetectionにする
                GameStateManager.instance.currentPlayerState = PlayerState.NoDetection;

            }

        }
    }

    //巡回用関数
    private void PatrolUpdate()
    {
        if (isWaiting) return; // 停止中は何もしない

        // 現在位置と目的の巡回ポイント座標の取得
        Vector2 currentPos = transform.position;
        Vector2 patrolPos = patrolPoints[currentPointIndex].position;

        //Agentの移動速度を巡回速度に設定
        agent.speed = P_moveSpeed;
        
        //現在の目的地の設定
        agent.destination = patrolPos;

        //巡回ポイントへのベクトルを計算
        Vector2 diff = patrolPos - currentPos;
        Vector2 moveDirection = diff.normalized;

        // 到着判定
        //一定距離以下になったら次の地点へ行く前に待機
        if (Vector2.Distance(currentPos, patrolPos) < 0.1f)
        {
            StartCoroutine(WaitBeforeNextPoint());
        }

        // 向き変更
        if (moveDirection != Vector2.zero)
        {
            // 回転処理のための角度を求める
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            //Lerp(補完処理)でなめらかに回転させる
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * 1.5f
                );
        }
    }

    //巡回時WayPoint到達時の一時停止
    private IEnumerator WaitBeforeNextPoint()
    {
        //待機中フラグ
        isWaiting = true;
        agent.speed = 0; // 移動停止（物理的に止まる）
        
        //経過時間を初期化
        float Elapsed = 0.0f;
        //指定時間が経過するまで待機
        while (Elapsed < P_waitTime)
        {
            //もし途中でChase状態になったら待機を中断
            if (_state == NPC_State.Chase)
            {
                isWaiting = false;

                yield break; //コルーチンを即終了
            }

            //経過時間を加算して次のフレームまで待つ
            Elapsed += Time.deltaTime;
            yield return null;

        }

        //待機完了後　次のポイントへ
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        // スピードを元に戻す
        agent.speed = P_moveSpeed;
        isWaiting = false;
    }

    //チェイス用関数
    private void ChaseUpdate()
    {
        //追跡速度に設定
        agent.speed = Chase_Speed;
        //Agentの目的地をプレイヤーの現在位置に設定
        agent.destination = target.position;

        //プレイヤー方向のベクトルを計算
        Vector2 moveDirection = (target.position - transform.position).normalized;
        
        //NPCが停止していない場合、移動方向を向くように回転
        if (moveDirection != Vector2.zero)
        {
            //ベクトルから角度へ変換
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            
            //なめらかに回転
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * TurnSpeed
                );
        }
    }

}
