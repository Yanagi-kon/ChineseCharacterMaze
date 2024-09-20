using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ïà»ú¸úËæ
public class SimpleCamera : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    private Vector2 angleOffset;
    public Camera fullCamera;
    public bool isFollow = true;

    void Start()
    {
        offset = transform.position - this.playerTransform.position;
        angleOffset = transform.eulerAngles;
        this.enabled= isFollow;
        fullCamera.enabled = !isFollow;
    }


    void Update()
    {
        if (this.enabled == true)
        {
            if (playerTransform)
            {
                this.transform.position = playerTransform.position + offset;
                Vector3 pos = playerTransform.position + offset;
                this.transform.position = pos;
            }
        }
        if(Destination.winFlag)
        {
            this.enabled = false;
            fullCamera.enabled = true;
        }
    }
}
