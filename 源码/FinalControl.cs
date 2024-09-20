using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalControl : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject duck1;
    public GameObject duck2;
    public GameObject duck3;
    public GameObject FinalPanel;
    public GameObject FinalCamera;
    public GameObject passUI;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
  
        if(LevelManager.allEnd)
        {
            timer += Time.deltaTime;
            FinalAnimation();
            LevelManager.allEnd = false;
        
            if(timer>12)
            {
                FinalPanel.SetActive(false);
                FinalCamera.SetActive(false);

            }
            if(timer>17)
            {
                passUI.SetActive(true);
            }
            if(timer>19.8)
            {
                passUI.SetActive(false);
            }
        }
    }

    public void FinalAnimation()
    {

        duck1.transform.position = point1.transform.position;
        duck2.transform.position = point2.transform.position;
        duck3.transform.position = point3.transform.position;
    
        //manager.TransitionState(StateType.FloorToFly);
        FinalPanel.SetActive(true);
        FinalCamera.SetActive(true);
        FinalPanel.GetComponent<Animator>().SetTrigger("panel");
        FinalCamera.GetComponent<Animator>().SetTrigger("cam");
        Debug.Log("ฒฅมห");
    }

}
