using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer instance;

    private AudioSource audioSource;

    void Awake()
    {
        // シーンが変わっても消さない
        DontDestroyOnLoad(this.gameObject);

        // すでに別の SoundPlayer が存在したら破棄（重複防止）
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
    public void PlaySEForSeconds(AudioClip clip, float seconds)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
            StartCoroutine(StopAfterSeconds(seconds));
        }
    }

    private System.Collections.IEnumerator StopAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.Stop();
    }
}