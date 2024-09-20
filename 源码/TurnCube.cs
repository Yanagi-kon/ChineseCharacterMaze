using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCube : MonoBehaviour
{

    public GameObject player;
    Rigidbody rbody;

    public int type = 1;  //type=1 ʩ�����ҵ���   type=2 ʩ�����µ���
    // Start is called before the first frame update
    void Start()
    {
        player = Ball.player.gameObject;
        rbody = player.GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //Debug.Log("������");
            rbody.velocity = Vector3.zero;//��������ٶȱ�Ϊ0
            if (type == 1)
            {
                rbody.AddForce(new Vector3(Ball.speed, 0, 0));
            }
            if (type == 2)
            {
                rbody.AddForce(new Vector3(0, 0, -Ball.speed));
            }
            this.gameObject.SetActive(false);
        }
    }
}
