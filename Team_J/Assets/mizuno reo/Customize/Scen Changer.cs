using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // �V�[����؂�ւ��郁�\�b�h
    public void ChangeScene(string Racescene)
    {
        SceneManager.LoadScene(Racescene);
    }
}
