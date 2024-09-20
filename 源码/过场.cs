using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 过场 : MonoBehaviour
{
  
    Animator ani;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
           
            ani.SetTrigger("guo");
            if(timer>=1.49f)
            {
                this.gameObject.SetActive(false);
                timer = 0;
            }
            timer += Time.deltaTime;
            
        }
    }
}
