using UnityEngine;

public class Reduction : MonoBehaviour
{
    void Start()
    {
        //�e�A�C�e���X�N���v�g�̍��v���v�Z
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //���炷�����i8���E4��
        int lostMoney = Mathf.FloorToInt(totalMoney * 0.8f);
        int lostPoints = Mathf.FloorToInt(totalPoints * 0.4f);

        //���Z��̒l
        totalMoney -= lostMoney;
        totalPoints -= lostPoints;

        //0�����ɂȂ�Ȃ��悤�ɕ␳
        if (totalMoney < 0) totalMoney = 0;
        if (totalPoints < 0) totalPoints = 0;

        //���ʂ����ꂼ��ɔ��f
        Item.totalMoney = totalMoney;
        Item2.totalMoney = totalMoney;
        Item3.totalMoney = totalMoney;

        Item.totalPoints = totalPoints;
        Item2.totalPoints = totalPoints;
        Item3.totalPoints = totalPoints;

       //�f�o�b�N
        Debug.Log("�߂܂�����̃V�[���F���z -" + lostMoney + "�~�A�|�C���g -" + lostPoints + "pt");
        Debug.Log("���݂̍��v�F���z " + totalMoney + "�~ �^ �|�C���g " + totalPoints + "pt");
    }
}