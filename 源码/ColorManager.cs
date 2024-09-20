using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;

    private GameObject body;
    private new Renderer renderer;
    public Material blue;
    public Material yellow;
    public Material orange;
    public static string color = "none";
    
    void Start()
    {
        player = this.gameObject;
        renderer=player.transform.Find("CowboyHat").GetComponent<Renderer>();
        Debug.Log(color);

    }

    void Update()
    {
          
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="BlueCube" )
        {
            Debug.Log("À¶µÄ");
            renderer.material = blue;
            color = "blue";
        }
        if (collision.gameObject.tag == "YellowCube")
        {
            Debug.Log("»ÆµÄ");
            renderer.material = yellow;
            color = "yellow";
        }
        if (collision.gameObject.tag == "OrangeCube")
        {
            Debug.Log("³ÈµÄ");
            renderer.material = orange;
            color = "orange";
        }
    }
    private void OnCollisionExit(Collision collision)
    {
      
    }


}
