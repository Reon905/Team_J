using UnityEngine;

public class DragRaseCar : MonoBehaviour
{
    public float acceleration = 20f;       //�����x�̑���
    public float maxSpeed = 50f;           //�ō����x
    public float heatIncreaseRate = 30f;   //�Q�[�W���������鑬��(��b������)
    public float heatDecreaseRate = 15f;   //�Q�[�W���������鑬��(��b������)
    public float macHeat = 100f;           //�q�[�g�Q�[�W�̍ő�l
    public float overheatCooldwnTime = 3f; //�I�[�o�[�q�[�g���̃N�[���_�E������
    public float currentSpeed = 0f;        //���݂̑��x
    public float currentHeat = 0f;         //���݂̃q�[�g�Q�[�W�̒l
    private bool isOverheated = false;       //�I�[�o�[�q�[�g��Ԃ��ǂ���
    private float cooldownTimer = 0f;      //�N�[���_�E���̎c�莞��

    private void Update()
    {
        if (isOverheated)
        {
            //�I�[�o�[�q�[�g���̏���

            //�N�[���_�E���^�C�}�[�����炷
            cooldownTimer -= Time.deltaTime;

            //�q�[�g�Q�[�W�����X�Ɍ��炷(��p)
            currentHeat = Mathf.Max(0, currentHeat - heatDecreaseRate * Time.deltaTime);

            //�N�[���_�E�����I�������I�[�o�[�q�[�g����
            if (cooldownTimer <= 0f)
            {
                isOverheated = false;
                currentHeat = 0f;  //�M�����Z�b�g
            }

            //�I�[�o�[�q�[�g���͌���������
            currentSpeed = Mathf.Max(0, currentSpeed - acceleration * Time.deltaTime);
        }
        else
        {
            //�ʏ펞�̏���

            if (Input.GetKeyUp(KeyCode.W))     //�L�[����
            {
                //�A�N�Z���{�^���������ݒ�

                //���x������������(�ő呬�x�܂�)
                currentSpeed = Mathf.Min(maxSpeed, currentSpeed + acceleration * Time.deltaTime);

                //�q�[�g�Q�[�W�����^���ɂȂ�����I�[�o�[�q�[�g����
                if (currentHeat >= macHeat)
                {
                    isOverheated = true;
                    cooldownTimer = overheatCooldwnTime;
                    Debug.Log("�I�[�o�[�q�[�g");
                }
            }
            else
            {
                //�A�N�Z���{�^���������ĂȂ��Ƃ�

                //�q�[�g�Q�[�W���p������(����������)
                currentHeat = Mathf.Max(0, currentHeat - heatDecreaseRate * Time.deltaTime);

                //���R����
                currentSpeed = Mathf.Max(0, currentSpeed - acceleration * Time.deltaTime * 2);
            }
        }

        //�Ԃ�O����(�����낵�Ȃ̂�vector3.up)�ɑ��x�������ړ�������
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
    }
}