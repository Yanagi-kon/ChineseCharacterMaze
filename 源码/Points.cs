using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Destination.index == 0)
        {
            transform.GetComponent<Collider>().enabled = true; //重加载后重新赋予collider
        }
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destination.realPath[Destination.index] = this.gameObject;
            Destination.index++;
            transform.GetComponent<Collider>().enabled = false;
            
        }
        if (Destination.index == Destination.length)
        {
            Destination.index--;
        }
        //Debug.Log(Destination.index);
    }
}
