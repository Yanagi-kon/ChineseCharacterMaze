using UnityEngine;
using System.Collections;
public class Sound : MonoBehaviour
{
    public AudioClip[] music = new AudioClip[3];// �����ļ�
    bool boo = true;// �������ֵĿ��� 
    int count = 0;//Ҫ���ŵ������ļ�
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