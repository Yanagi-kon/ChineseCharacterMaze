using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();//获取当前对象的Animator 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cube")    //触地
        {
            animator.Play("landingFloor");
         
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")    //触地
        {
            Debug.Log("离开了");
            animator.Play("goToFlyFloor");

        }
    }
}
