using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    private string itemID;
    private bool isCollected = false; // �擾�ς݂��ǂ���

    void Start()
    {
        // ���ID�����i�V�[�����{�I�u�W�F�N�g���{�ʒu�j
        itemID = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_" + transform.position.ToString();

        // �擾�ς݂Ȃ��\��
        if (PlayerPrefs.GetInt(itemID, 0) == 1)
        {
            Destroy(gameObject);
            isCollected = true;
        }
    }

    public void CollectItem()
    {
        if (isCollected) return;

        isCollected = true;

        // �L�^
        PlayerPrefs.SetInt(itemID, 1);
        PlayerPrefs.Save();

        // �A�C�e���폜
        Destroy(gameObject);
    }
}