using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGroundSound : MonoBehaviour
{
    private AudioSource Main_Audio;
    public AudioClip enterClip;
    private Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Main_Audio = GetComponent<AudioSource>();
        myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube" || other.gameObject.tag == "Respawn" && Mathf.Abs(myRigidbody.velocity.y) > 0.1f)
        {
            Main_Audio.PlayOneShot(enterClip);
        }
    }
}
