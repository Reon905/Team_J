using UnityEngine;
using static E_Player_Controller;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    // �v���C���[�̏�Ԃ�ێ�����ϐ�
    public PlayerState currentPlayerState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // �V�[����؂�ւ��Ă����̃I�u�W�F�N�g��j�����Ȃ�
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���ɃC���X�^���X�����݂���ꍇ�A�d����j��
            Destroy(gameObject);
        }
    }
}
