using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal1 : MonoBehaviour
{
    public GameObject player;
    public GameObject pairPortal;
    Rigidbody rbody;

    void Start()
    {
        player = Ball.player.gameObject;
        DrawLine.i = 80;
        rbody = player.GetComponent<Rigidbody>();
        transform.GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        if (DrawLine.i < 80)
            transform.GetComponent<Collider>().enabled = false;
        else
            transform.GetComponent<Collider>().enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            Vector3 pos = new Vector3();
            pos.x = pairPortal.transform.position.x;
            pos.z = pairPortal.transform.position.z;
            pos.y = player.transform.position.y;
            player.transform.position = pos;

            rbody.velocity = Vector3.zero;//让物体的速度变为0

            this.gameObject.SetActive(false);
            Debug.Log("失效");
        }
    }

}
