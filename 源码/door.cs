using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Animator openDoor;
    private AudioSource doorAudio;
    public AudioClip doorClip;
    public bool hasOpened=false;
    // Start is called before the first frame update
    void Start()
    {
        openDoor = GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.tag=="Player")
        {
            openDoor.SetTrigger("open");
            if (!hasOpened)
                doorAudio.PlayOneShot(doorClip);
            hasOpened = true;
            
        }
    }
}
