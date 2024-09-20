using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetect : MonoBehaviour
{
    public string color;
    public GameObject star;
    private float timer;
    private bool rise = false;
    public GameObject colorEffect;
    private AudioSource Main_Audio;
    public AudioClip changeClip;
    void Start()
    {
        Main_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rise)
            timer += Time.deltaTime;
        if (timer < 0.6 && rise &&star!=null)
        {
            star.transform.Translate(Vector3.up * 2 * Time.deltaTime, Space.World);   
        }
        //if(exit)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= 1)
        //        this.gameObject.SetActive(false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (color == ColorManager.color)
            {
                rise = true;
            }
            else
            {
                rise = false;
            }
            Main_Audio.PlayOneShot(changeClip);
        }
    }

    private void OnTriggerExit(Collider other) //离开后色块消失
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            GameObject.Instantiate(colorEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
            if(star)
            {
                star.gameObject.SetActive(false);
            }
            
        }
    }
}
