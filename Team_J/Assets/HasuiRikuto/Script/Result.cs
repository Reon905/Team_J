using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        //�e�A�C�e���̃|�C���g�����Z  ���[�X���ʂł��炦��|�C���g�����Z
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        //�e�A�C�e���̋��z�����Z 
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;

        //�A�C�e���̎擾��
        int itemCount = Item.itemCount + Item2.itemCount + Item3.itemCount;


        //��ʂɕ\��
        resultText.text =

             itemCount + "��\n" +
             totalMoney + "�~\n" +
             totalPoints + "pt\n";

        //�\��
        Debug.Log("���v�A�C�e��: " + itemCount + "��");
        Debug.Log("���v�|�C���g: " + totalPoints + "pt");
        Debug.Log("���v���z: " + totalMoney + "�~");
    }
}
