using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();//��ȡ��ǰ�����Animator 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Cube")    //����
        {
            animator.Play("landingFloor");
         
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")    //����
        {
            Debug.Log("�뿪��");
            animator.Play("goToFlyFloor");

        }
    }
}
