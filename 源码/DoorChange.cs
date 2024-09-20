using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChange : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject changeEffect;
    private AudioSource doorAudio;
    public AudioClip doorSwitch;
    private float timer = 0;
    public float cycleTime = 5f;
    public int currentPosition;   //1 2 3  表示初始位置 并不会更新变化
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false;
   // private bool effectFlag = false;
    void Start()
    {
        doorAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer>cycleTime-0.5f && timer<cycleTime-0.485f && !flag1)
        {
            GameObject.Instantiate(changeEffect, position1.transform.position, position1.transform.rotation);
            GameObject.Instantiate(changeEffect, position2.transform.position, position2.transform.rotation);
            GameObject.Instantiate(changeEffect, position3.transform.position, position3.transform.rotation);
            Debug.Log("shenghcnegl");
        }
        if (timer > 2*cycleTime - 0.5f && timer < 2*cycleTime - 0.485f && !flag2)
        {
            GameObject.Instantiate(changeEffect, position1.transform.position, position1.transform.rotation);
            GameObject.Instantiate(changeEffect, position2.transform.position, position2.transform.rotation);
            GameObject.Instantiate(changeEffect, position3.transform.position, position3.transform.rotation);
        }
        if (timer > 3*cycleTime - 0.5f && timer < 3*cycleTime - 0.485f && !flag3)
        {
            GameObject.Instantiate(changeEffect, position1.transform.position, position1.transform.rotation);
            GameObject.Instantiate(changeEffect, position2.transform.position, position2.transform.rotation);
            GameObject.Instantiate(changeEffect, position3.transform.position, position3.transform.rotation);
        }
        if (timer>=cycleTime&&!flag1)
        {
            if (currentPosition == 1)
            {
                this.transform.position = position2.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                doorAudio.PlayOneShot(doorSwitch);
            }
            if (currentPosition == 2)
            {
                this.transform.position = position3.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
               //Debug.Log("2换1");
            }
            if (currentPosition == 3)
            {
                this.transform.position = position1.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            flag1 = true;
        }
        if(timer >= 2*cycleTime && !flag2)
        {
            if (currentPosition == 1)
            {
                this.transform.position = position3.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                doorAudio.PlayOneShot(doorSwitch);
            }
            if (currentPosition == 2)
            {
                this.transform.position = position1.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                
            }
            if (currentPosition == 3)
            {
                this.transform.position = position2.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            flag2 = true;
        }
        if (timer >= 3*cycleTime && !flag3)
        {
            if (currentPosition == 1)
            {
                this.transform.position = position1.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                doorAudio.PlayOneShot(doorSwitch);
            }
            if (currentPosition == 2)
            {
                this.transform.position = position2.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
        
            }
            if (currentPosition == 3)
            {
                this.transform.position = position3.transform.position;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
           // effectFlag = true;
            timer = 0;
            flag1 = false;
            flag2 = false;
            flag3 = false;
        }
    }
}
