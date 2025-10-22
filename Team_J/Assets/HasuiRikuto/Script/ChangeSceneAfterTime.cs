using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneAfterTime : MonoBehaviour
{
    // ���̃V�[����
    public string nextSceneName = "NextScene";

    // �c�莞�Ԃ�\������UI�e�L�X�g
    public Text timerText;

    // �o�ߎ��Ԃ�static�ɂ��ĕێ�
    public static float timer = 0f;

    // �V�[���؂�ւ��܂ł̎��ԁi5����300�b�j
    public float changeTime = 300f;

    void Update()
    {
        // �o�ߎ��Ԃ����Z
        timer += Time.deltaTime;

        // �c�莞�Ԃ��v�Z
        float remainingTime = Mathf.Max(changeTime - timer, 0f);

        // ���E�b�ɕϊ�
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // �e�L�X�g�X�V
        if (timerText != null)
        {
            timerText.text = $"�c�莞�� : {minutes:00}:{seconds:00}";
        }

        // ���Ԃ�������V�[���؂�ւ�
        if (timer >= changeTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}