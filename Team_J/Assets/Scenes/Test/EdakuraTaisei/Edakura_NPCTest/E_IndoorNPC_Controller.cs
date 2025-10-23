using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class E_Indoor_NPC_Controller : MonoBehaviour
{
    // ���E�̑ΏۂƂ��郌�C���[�iPlayer���Q���Ȃǁj
    public LayerMask m_TargetLayer; // �����ݒ肷�邱�Ƃɂ��A���g(NPC)�̃R���C�_�[�ɔ������Ȃ��Ȃ�

    public float m_fSightAngle;    // �O�����E�͈�
    public float Detection_Value;  // ���o�l(���E���ɓ���Ə㏸)

    private Vector2 posDelta;        // NPC����v���C���[�ւ̃x�N�g��

    Rigidbody2D NPC_rbody;

    private float TargetAngle;    // Player�ւ̊p�x      TargetAngle��ChaseAngle�𓝈ꂷ��
    private float TimeElapsed;    // �o�ߎ���
    private float TimeOut;        // ���s�Ԋu



    public enum NPC_State { Patrol, Chase };

    public float P_moveSpeed = 2f;      // Patrol�ړ����x
    public float P_waitTime = 2f;       // Patrol�ҋ@����
    public float TurnSpeed = 1.5f;      // ���񑬓x

    private int currentPointIndex = 0;    // ���̖ړI�n�������C���f�b�N�X
    private bool isWaiting = false; // ��~���t���O

    [SerializeField] float Chase_Speed = 2.0f; // �G�̒ǐՑ��x

    NavMeshAgent2D agent;               //NavMeshAgent2D���g�p���邽�߂̕ϐ�
    [SerializeField] Transform target;  //�ǐՂ���^�[�Q�b�g
    public Transform[] patrolPoints;    // ����n�_���i�[����z��


    // ������Ԃ�Patrol�ɂ��Ă���
    public NPC_State _state = NPC_State.Patrol;

    private void Start()
    {
        NPC_rbody = GetComponent<Rigidbody2D>();

        m_fSightAngle = Constants.DEFAULT_SIGHT_ANGLE;
        Detection_Value = Constants.DEFAULT_DETECTION_VALUE;
        TimeOut = 0.02f;

        agent = GetComponent<NavMeshAgent2D>(); //agent��NavMeshAgent2D���擾
        agent.speed = P_moveSpeed;  //���񑬓x�ɍ��킹��
    }

    private void Update()
    {
        // �^�C�����Z
        TimeElapsed += Time.deltaTime;

        // ��Ԃ�Patrol�̏ꍇ
        if (_state == NPC_State.Patrol)
        {
            PatrolUpdate();
        }//Chase�̏ꍇ
        else if (_state == NPC_State.Chase)
        {
            ChaseUpdate();
        }
    }




    // NPC�̎��E����
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))     // Player�^�O��Collider�������^
        {
            //
            if (TimeElapsed >= TimeOut)
            {
                RaycastHit2D hit;   // ray�����������R���C�_�[���ʗp

                // ����̐ݒ�
                posDelta = other.transform.position - this.transform.position;  // NPC����Player�ւ̕����x�N�g��
                TargetAngle = Vector2.Angle(this.transform.right, posDelta);    // NPC����Player�̊p�x
                                                                                // Player��NPC�̎��E�ɓ����Ă��邩�m�F�i��Q���͖����j
                if (TargetAngle < m_fSightAngle)     // targetAngle��m_SightAngle�Ɏ��܂��Ă��邩�ǂ���
                {
                    // Ray���΂��āA�Ԃɏ�Q�����Ȃ����𔻒肷��
                    //                                      �n�_                 ����        �v���C���[�܂ł̋����@  ���m���C���[�@
                    if (hit = Physics2D.Raycast(this.transform.position, posDelta.normalized, posDelta.magnitude, m_TargetLayer))
                    {
                        // �v���C���[�����E���ɂ��鎞�̏���
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            Detection_Value += 0.1f;    // ���o�l���㏸������
                            Debug.Log("���o�l�㏸");
                            // ���o�l��MAX_DETECTION_VALUE�𒴂�����
                            if (Detection_Value > Constants.MAX_DETECTION_VALUE)
                            {
                                Detection_Value = 0.0f;     // ���o�l��0��

                                //Player�̏�Ԃ�Detection�ɂ���
                                GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                                Debug.Log("Detection!!!");

                                _state = NPC_State.Chase;      // ��Ԃ�Chase�ɐ؂�ւ�

                                Debug.Log("��Q���Ȃ��A���E�͈͓�");
                            }
                        }
                        else if (hit.collider == null)
                        {
                            Debug.Log("�Ȃ���Ȃ�");
                        }
                        else    // ��Q��������ꍇ
                        {
                            // Debug.Log("��Q������" + hit.collider.name);
                        }
                    }
                }
                // Time��0�ɂ���
                TimeElapsed = 0.0f;
            }
        }
    }

    // ���E�͈͂���Player���������u�Ԃ̏���
    private void OnTriggerExit2D(Collider2D other)
    {
        // ������Collider�̃^�O��Player�̏ꍇ
        if (other.CompareTag("Player"))
        {
            if (TargetAngle > m_fSightAngle)
            {
                Detection_Value = 0.0f;     //Detection_Value��0.0f�ɂ���

            }
        }
    }

    // �Փ˂����u�Ԃ̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂�������̃^�O�� "Player" ��������
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_state == NPC_State.Patrol)         // ��Ԃ�Patrol�̏ꍇ
            {
                // �ڐG�������̏���
                Debug.Log("Player�ƐڐG");

                GameStateManager.instance.currentPlayerState = PlayerState.Detection;
                _state = NPC_State.Chase;       // ��Ԃ�Chase�ɐ؂�ւ�
            }
            else if (_state == NPC_State.Chase)     // ��Ԃ�Chase�̏ꍇ
            {
                //Chase���ɏՓ˂�����Scene��؂�ւ���
                SceneManager.LoadScene("Caught Scene");
                GameStateManager.instance.currentPlayerState = PlayerState.NoDetection;

            }

        }
    }

    //����p�֐�
    private void PatrolUpdate()
    {
        if (isWaiting) return; // �� ��~���͉������Ȃ��I

        Vector2 currentPos = transform.position;
        Vector2 patrolPos = patrolPoints[currentPointIndex].position;

        agent.speed = P_moveSpeed;
        agent.destination = patrolPos;

        Vector2 diff = patrolPos - currentPos;
        Vector2 moveDirection = diff.normalized;

        // ��������
        if (Vector2.Distance(currentPos, patrolPos) < 0.1f)
        {
            StartCoroutine(WaitBeforeNextPoint());
        }

        // �����ύX
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 1.5f);
        }
    }

    //����WayPoint���B���̈ꎞ��~
    private IEnumerator WaitBeforeNextPoint()
    {
        isWaiting = true;
        agent.speed = 0; // �ړ���~�i�����I�Ɏ~�܂�j

        float Elapsed = 0.0f;

        while (Elapsed < P_waitTime)
        {
            //�����r����Chase��ԂɂȂ�����ҋ@�𒆒f
            if (_state == NPC_State.Chase)
            {
                isWaiting = false;

                yield break; //�R���[�`���𑦏I��
            }

            Elapsed += Time.deltaTime;
            yield return null;

        }

        //�ҋ@������@���̃|�C���g��
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        agent.speed = P_moveSpeed;
        isWaiting = false;
    }

    //�`�F�C�X�p�֐�
    private void ChaseUpdate()
    {
        agent.speed = Chase_Speed;
        agent.destination = target.position;

        Vector2 moveDirection = (target.position - transform.position).normalized;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * TurnSpeed);
        }
    }

}
