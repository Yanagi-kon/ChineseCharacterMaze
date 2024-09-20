using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpeedControl : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y<-70.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, -70, this.transform.position.z);
        }
    }
}
