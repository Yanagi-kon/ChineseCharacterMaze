using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public Transform[] Father;
    private float timer = 0;
    private float timer2 = 0;
    private int index=1;
    private float threshold;
    private float thresholdIndex = 0;
    private AudioSource mapAudio;
    public AudioClip disappearClip;
    public GameObject disappearEffect;
    public GameObject appearEffect;
    private bool mapFlag=false;
    // Start is called before the first frame update
    void Start()
    {
        mapAudio = GetComponent<AudioSource>();

        threshold = 3f;
        Father=GetComponentsInChildren<Transform>(true); //获得包括父物体自身在内的所有子物体

        for(int i=9;i< Father.Length;i++)
        {
            Father[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer2 > threshold - 0.45f)  //提前0.45s产生特效
        {
            if (threshold > 0 && index < 22 && !mapFlag)
            {
                GameObject.Instantiate(appearEffect, new Vector3(Father[index + 8].transform.position.x, Father[index + 8].transform.position.y - 0.8f,
                        Father[index + 8].transform.position.z), Father[index + 8].transform.rotation);
                mapFlag = true;
            }
            
        }
        if (timer>threshold)
        {
            if (threshold > 0 && index < 30)
            {
                Father[index].gameObject.SetActive(false);
                threshold = 3 - Mathf.Log(thresholdIndex+1,2f);
                mapAudio.PlayOneShot(disappearClip);
                GameObject.Instantiate(disappearEffect, Father[index].transform.position, Father[index].transform.rotation);
                if (index < 22)
                {
                    Father[index + 8].gameObject.SetActive(true);
                    
                }
                index++;
                thresholdIndex += 0.15f;
                timer = 0;
                timer2 = 0;
                mapFlag = false;
                //Debug.Log(threshold);
            }   
        }
    }   
}
