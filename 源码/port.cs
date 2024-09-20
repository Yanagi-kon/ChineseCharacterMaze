using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class port : MonoBehaviour
{
    public GameObject pairPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = pairPoint.transform.position;
            Debug.Log(pairPoint.transform.position);
        }
    }

}
