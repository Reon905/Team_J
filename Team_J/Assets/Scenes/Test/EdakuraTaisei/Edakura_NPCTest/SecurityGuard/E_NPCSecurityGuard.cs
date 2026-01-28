using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class E_NPCSecurityGuard : MonoBehaviour
{
    public Text ChaseTimeText;  //UnityからText表示場所を入れる

    // 視界の対象とするレイヤー（Playerや障害物など）
    public LayerMask m_TargetLayer; // これを設定することにより、自身(NPC)のコライダーに反応しなくなる

    public float m_fSightAngle;    // 前方視界範囲
    public float Detection_Value;  // 発覚値(視界内に入ると上昇)
    private bool isDetection;

    private Vector2 posDelta;        // NPCからプレイヤーへのベクトル

    Rigidbody2D NPC_rbody;

    private float TargetAngle;    // Playerへの角度      TargetAngleとChaseAngleを統一する
    private float TimeElapsed;    // 経過時間
    private float TimeOut;        // 実行間隔



    public enum NPC_State { Patrol, Chase };

    public float P_moveSpeed = 2f;      // Patrol移動速度
    public float P_waitTime = 2f;       // Patrol待機時間
    public float TurnSpeed = 32f;      // 旋回速度

    private int currentPointIndex = 0;    // 次の目的地を示すインデックス
    private bool isWaiting = false; // 停止中フラグ

    [SerializeField] float Chase_Speed = 2.0f; // 敵の追跡速度
    public float ChaseTimer;     // Chase時間(屋内屋外共有)


    NavMeshAgent2D agent;               //NavMeshAgent2Dを使用するための変数
    [SerializeField] Transform target;  //追跡するターゲット
    public Transform[] patrolPoints;    // 巡回地点を格納する配列


    // 初期状態をPatrolにしておく
    public NPC_State _state = NPC_State.Patrol;

    AudioSource DetectionSource;
    public AudioClip DetectionClip;

    //アニメーション用
    Animator Police_animator;
    string stopAnime = "PoliceStop";
    string moveAnime = "PoliceMove";
    string nowAnime = "";
    string oldAnime = "";

    private void Start()
    {
        Police_animator = this.GetComponent<Animator>();    //Animatorコンポーネントを取る
        nowAnime = stopAnime;   //停止状態から開始
        oldAnime = stopAnime;   //停止状態から開始
        NPC_rbody = GetComponent<Rigidbody2D>();
        DetectionSource = GetComponent<AudioSource>();

        //各種変数を初期化
        m_fSightAngle = Constants.DEFAULT_SIGHT_ANGLE;        //30.0f
        Detection_Value = Constants.DEFAULT_DETECTION_VALUE;  //2.5f
        ChaseTimer = Constants.CHASE_TIMER; //10.0f
        isDetection = false;
        TimeOut = 0.02f;

        agent = GetComponent<NavMeshAgent2D>(); //agentにNavMeshAgent2Dを取得
        agent.speed = P_moveSpeed;  //巡回速度に合わせる

    }

    private void Update()
    {
        // タイム加算
        TimeElapsed += Time.deltaTime;

        // 状態がPatrolの場合
        if (_state == NPC_State.Patrol)
        {
            PatrolUpdate();
        }//Chaseの場合
        else if (_state == NPC_State.Chase)
        {
            ChaseUpdate();
        }
    }

    void FixedUpdate()
    {
        //アニメーション更新
        if(agent.speed == 0)
        {
            nowAnime = stopAnime;   //停止中
        }
        else
        {
            nowAnime = moveAnime;   //移動

        }

        if (nowAnime != oldAnime)
        {
            //Debug.Log(nowAnime);
            oldAnime = nowAnime;
            Police_animator.Play(nowAnime);    //アニメーション再生
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
                            if (_state == NPC_State.Patrol)
                            {
                                Detection_Value += 0.1f;    // 発覚値を上昇させる
                                Debug.Log("発覚値上昇");
                            }
                            else if (_state == NPC_State.Chase)
                            {
                                // 追跡中は発覚値を増やさない
                            }

                        // 発覚値がMAX_DETECTION_VALUEを超えたら
                        if (Detection_Value > Constants.MAX_DETECTION_VALUE)
                            {
                                Detection_Value = 0.0f;     // 発覚値を0に

                                // Playerの状態が未発覚であれば
                                if (GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
                                {
                                    // Playerの状態をDetectionにする
                                    GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                                    DetectionSource.PlayOneShot(DetectionClip);
                                }

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

                GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                _state = NPC_State.Chase;       // 状態をChaseに切り替え
            }
            else if (_state == NPC_State.Chase)     // 状態がChaseの場合
            {

                    // Chase中に衝突したらSceneを切り替える
                    SceneManager.LoadScene("Caught Scene");
                    // 次のプレイのためにプレイヤーの状態をNoDetectionにしておく
                    GameStateManager.instance.currentPlayerState = PlayerState.NoDetection;

                
            }
        }
    }
    /// <summary>
    /// Waypointを巡回するための関数
    /// </summary>
    private void PatrolUpdate()
    {

        if (isWaiting) return; // 停止中は何もしない

        // 現在位置と目的の巡回ポイント座標の取得
        Vector2 currentPos = transform.position;
        Vector2 patrolPos = patrolPoints[currentPointIndex].position;

        // NavMeshAgent2Dの移動速度を巡回速度に設定
        agent.speed = P_moveSpeed;

        // 現在の目的地の設定
        agent.destination = patrolPos;

        // 巡回ポイントへのベクトルを計算
        Vector2 diff = patrolPos - currentPos;
        Vector2 moveDirection = diff.normalized;

        // 到着判定
        // 一定距離以下になったら次の地点へ行く前に待機
        if (Vector2.Distance(currentPos, patrolPos) < 0.1f)
        {
            // 待機コルーチン開始
            StartCoroutine(WaitBeforeNextPoint());
        }

        // 移動方向に合わせてなめらかに回転
        if (moveDirection != Vector2.zero)
        {
            // 移動方向へのベクトルを角度に変換
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * TurnSpeed
                );
        }
    }

    
    /// <summary>
    /// 巡回時Waypoint到達時の一時停止関数
    /// </summary>
    private IEnumerator WaitBeforeNextPoint()
    {
        // 待機中フラグ
        isWaiting = true;
        agent.speed = 0; // 移動停止（物理的に止まる）

        // 経過時間を初期化
        float Elapsed = 0.0f;

        // P_waitTime = Random.Range(2.0f, 2.5f);
        // 指定時間経過するまで待機
        while (Elapsed < P_waitTime)
        {
            // もし途中でChase状態になったら待機を中断
            if (_state == NPC_State.Chase)
            {
                isWaiting = false;

                yield break; //コルーチンを即終了
            }

            // 経過時間を加算して次のフレームまで待つ
            Elapsed += Time.deltaTime;
            yield return null;

        }

        // 待機完了後　次のポイントへ
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        // スピードを元に戻す
        agent.speed = P_moveSpeed;
        isWaiting = false;
    }

    /// <summary>
    ///  プレイヤーを見つけた時のチェイス用関数
    /// </summary>
    private void ChaseUpdate()
    {
        // 追跡速度に設定
        agent.speed = Chase_Speed;

        // Agentの目的地をプレイヤーの現在位置に設定
        agent.destination = target.position;
        // チェイス時間を加算
        ChaseTimer -= Time.deltaTime;

        // ChaseTimerが０以下になったら
        if (ChaseTimer < 0)
        {
            // 状態がDetectionの場合
            if (GameStateManager.instance.currentPlayerState == PlayerState.Detection)
            {
                // Playerの状態をNoDetectionにする
                GameStateManager.instance.currentPlayerState = PlayerState.NoDetection;
            }

            Debug.Log("NoDetection!");
            // Patrolへ変更
            _state = NPC_State.Patrol;
            // ChaseTimerを初期化
            ChaseTimer = Constants.CHASE_TIMER;
        }

        //プレイヤー方向のベクトルを計算
        Vector2 moveDirection = (target.position - transform.position).normalized;

        // 移動方向を向くように回転
        if (moveDirection != Vector2.zero)
        {
            // ベクトルから角度へ変換
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            // 回転
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * TurnSpeed
                );
        }
    }
}
