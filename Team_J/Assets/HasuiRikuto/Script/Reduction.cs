using UnityEngine;

public class Reduction : MonoBehaviour
{
    void Start()
    {
        // --- �e�X�g�p�ɐ��l���� ---
        Item.totalMoney = 1034;
        Item2.totalMoney = 2030;
        Item3.totalMoney = 3042;

        Item.totalPoints = 60;
        Item2.totalPoints = 40;
        Item3.totalPoints = 60;

        Debug.Log("�e�X�g�����l�F");
        Debug.Log($"Item1 �� {Item.totalMoney}�~�^{Item.totalPoints}pt");
        Debug.Log($"Item2 �� {Item2.totalMoney}�~�^{Item2.totalPoints}pt");
        Debug.Log($"Item3 �� {Item3.totalMoney}�~�^{Item3.totalPoints}pt");

        //�A�C�e��8�����z �|�C���g�S������
        Item.totalMoney = Mathf.FloorToInt(Item.totalMoney * 0.2f);
        Item.totalPoints = Mathf.FloorToInt(Item.totalPoints * 0.6f);

        Item2.totalMoney = Mathf.FloorToInt(Item2.totalMoney * 0.2f);
        Item2.totalPoints = Mathf.FloorToInt(Item2.totalPoints * 0.6f);

        Item3.totalMoney = Mathf.FloorToInt(Item3.totalMoney * 0.2f);
        Item3.totalPoints = Mathf.FloorToInt(Item3.totalPoints * 0.6f);

        // --- �R���\�[���Ō��ʊm�F ---
        Debug.Log("�߂܂�����i8�����z�@�|�C���g�S�������j�F");
        Debug.Log($"Item1 �� {Item.totalMoney}�~�^{Item.totalPoints}pt");
        Debug.Log($"Item2 �� {Item2.totalMoney}�~�^{Item2.totalPoints}pt");
        Debug.Log($"Item3 �� {Item3.totalMoney}�~�^{Item3.totalPoints}pt");

        // --- ���v���m�F ---
        int totalMoney = Item.totalMoney + Item2.totalMoney + Item3.totalMoney;
        int totalPoints = Item.totalPoints + Item2.totalPoints + Item3.totalPoints;

        Debug.Log($"���v�F{totalMoney}�~ �^ {totalPoints}pt");
    }
}