using UnityEngine;

public class DetectionMusic : MonoBehaviour
{
    public static DetectionMusic instance;
    AudioSource DmusicSource;

    private bool Played = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // シーンを切り替えてもこのオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 既にインスタンスが存在する場合、重複を破棄
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DmusicSource = GetComponent<AudioSource>();

        //MusicStop();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameStateManager.instance.currentPlayerState == PlayerState.Detection)
        {
            if (DmusicSource.isPlaying == false)
            {
                DmusicSource.Play();
            }
        }
        else if (GameStateManager.instance.currentPlayerState == PlayerState.NoDetection)
        {
            if(DmusicSource.isPlaying == true) 
            { 
            DmusicSource.Stop();
            } 
        }

    }

    //void MusicStop()
    //{
    //    if (Played == false)
    //    {
    //        if (DmusicSource.isPlaying == true)
    //        {
    //            DmusicSource.Stop();
    //        }
    //        Played = true;
    //    }

    //}
}
