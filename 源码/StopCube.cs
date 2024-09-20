using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCube : MonoBehaviour
{

    public GameObject player;
    Rigidbody rbody;
    public bool isWall = false;
    // Start is called before the first frame update
    void Start()
    {
        rbody = player.GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            rbody.velocity = Vector3.zero;//让物体的速度变为0
            if (!isWall)
                this.gameObject.SetActive(false);
        }
    }
}
