using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    public GameObject player;
    public GameObject pairPortal1;
    public GameObject pairPortal2;
    public GameObject pairPortal3;
    Rigidbody rbody;
    private AudioSource qiAudioSource;
    public AudioClip qiClip;
    void Start()
    {
        player = Mode2Player.player.gameObject;
        player.tag = "Player";
        rbody = player.GetComponent<Rigidbody>();
        qiAudioSource = GetComponent<AudioSource>();
    }




    private void OnTriggerEnter(Collider other)
    {
        float RandomNumber = Random.Range(0f, 3f);
        //Debug.Log(RandomNumber);
        if(RandomNumber<1f)
            player.transform.position = pairPortal1.transform.position;
        else if(RandomNumber>=1f&&RandomNumber<2f) 
            player.transform.position = pairPortal2.transform.position;
        else if (RandomNumber >= 2f)
            player.transform.position = pairPortal3.transform.position;

        rbody.velocity = new Vector3(0, 10, 0);

        qiAudioSource.PlayOneShot(qiClip);


    }

}
