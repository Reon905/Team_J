//StageRank
using UnityEngine;
using UnityEngine.UI;

public class StageRank : MonoBehaviour
{
    [SerializeField] private Text RankText;

    private void Start()
    {//CarStage��\��
        if (RankText == null)
        {
            Debug.LogError("RankText ���ݒ肳��Ă��܂���! Canvas ��� Taxt ���h���b�O���Ă��������B");
            return;
        }
        RankText.text = $"���݂̋������x��: {Customize.CarStage}";
    }

    private void Update()
    {//CarStage���X�V��������\��
        if (RankText == null) return;

        RankText.text = $"���݂̋������x��: {Customize.CarStage}";
    }
}
