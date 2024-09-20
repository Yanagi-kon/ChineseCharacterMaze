using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public Camera followCamera;
    public GameObject player;
    Rigidbody rbody;
    public GameObject door;
    Animator openDoor;
    private AudioSource doorAudio;
    public AudioClip doorClip;
    public AudioClip bounceClip;
    void Start()
    {
        player = Mode2Player.player.gameObject;
        rbody = player.GetComponent<Rigidbody>();
        openDoor = door.GetComponent<Animator>();
        doorAudio = door.GetComponent<AudioSource>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorAudio.PlayOneShot(doorClip);
            doorAudio.PlayOneShot(bounceClip);
            rbody.velocity = new Vector3(0, 10, 0);
            this.gameObject.SetActive(false);
            openDoor.SetTrigger("open");

            //ø™√≈“Ù–ß
          
            

        }
    }
}
