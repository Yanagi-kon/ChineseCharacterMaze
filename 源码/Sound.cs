using UnityEngine;
using System.Collections;
public class Sound : MonoBehaviour
{
    public AudioClip[] music = new AudioClip[3];// 音乐文件
    bool boo = true;// 控制音乐的开关 
    int count = 0;//要播放的声音文件
    float timer;
    public float JianGeTimer=3.0f;
    AudioSource AudioSource;

    void Start()
    {

            AudioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (timer >= JianGeTimer)
        {
            PlayerMusic();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    public void PlayerMusic()
    {
        if (!AudioSource.isPlaying)
            if (count < music.Length)
            {
                AudioSource.clip = music[count];
                AudioSource.Play();
                count++;
            }
            else
            {
                count = 0;
            }
    }
}