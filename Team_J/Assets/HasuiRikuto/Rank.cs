using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public static string currentRank = "D"; //���݂̃����N��ێ�

    public Text rankText;

    void Start()
    {
        //Item,Item2,Item3�̍��v�|�C���g���擾
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //�����N�𔻒肵�Đݒ�
        currentRank = GetRank(totalPoints);

        //�f�o�b�O�\��
        Debug.Log("���v�|�C���g: " + totalPoints + "pt �� �����N: " + currentRank);

        //��ʂɂ��\��
        if (rankText != null)
        {
            rankText.text = currentRank;
        }
        else
        {
            Debug.LogWarning("Rank Text ���ݒ肳��Ă��܂���I");
        }
    }

    //�|�C���g�ɉ����ă����N��Ԃ��֐�
    string GetRank(int totalPoints)
    {
        if (totalPoints >= 250)
            return "S";
        else if (totalPoints >= 130)
            return "A";
        else if (totalPoints >= 80)
            return "B";
        else if (totalPoints >= 30)
            return "C";
        else
            return "D";
    }
}
