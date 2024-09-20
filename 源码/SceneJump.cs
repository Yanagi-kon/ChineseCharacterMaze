using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneJump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jump1()//第一个场景 
    {
        SceneManager.LoadScene(0);
    }
    public void Jump2() //第二个场景
    {
        SceneManager.LoadScene(1);
    }

}
