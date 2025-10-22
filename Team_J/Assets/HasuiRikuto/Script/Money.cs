using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneyText; // UI Text��Inspector�Ƀh���b�O

    void Update()
    {
        // �e�A�C�e���X�N���v�g�̍��v���z���擾
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;

        // �e�A�C�e���X�N���v�g�̍��v�|�C���g���擾
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        // �e�L�X�g����ʂɕ\���i���s��2�s�ɕ�����j
        moneyText.text =
            "���v���z: " + totalMoney + "�~\n" +
            "���v�|�C���g: " + totalPoints + "pt";
    }
}