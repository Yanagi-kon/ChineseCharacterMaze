using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject player;
    public Material finalMaterial;

    public GameObject[] pathPoints = new GameObject[12];
    public static GameObject[] realPath = new GameObject[20];
    public static List<string> names = new List<string>();
    public string[] rightNames = new string[5];
    public static int index=0;
    private new Renderer renderer;
    public static bool winFlag;
    public static bool failFlag;
    private int count=0;
    private bool reachDestination=false;
    Rigidbody rbody;
    public static int length;
    private float timer = 0;
    public static bool desFlag = false;
    private AudioSource desAudio;
    public AudioClip desClip;
    public AudioClip winClip;
    public AudioClip failClip;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        rbody = player.GetComponent<Rigidbody>();
        winFlag = false;
        length = pathPoints.Length;
        names.Clear();
        desAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {  
        if(reachDestination)
            rbody.velocity = Vector3.zero;//让物体的速度变为0
        if (Destination.winFlag && Destination.desFlag)
        {
            renderer.material = finalMaterial;
        }

        }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reachDestination = true;
            rbody.velocity = Vector3.zero;//让物体的速度变为0
            if (!desAudio.isPlaying)
                desAudio.PlayOneShot(desClip);
            if (names.Count == rightNames.Length)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    Debug.Log("names[i]:" + names[i] + " rightNames[i]:" + rightNames[i]);
                    if (names[i] == rightNames[i])
                        count++;
                }
                if (count == rightNames.Length)
                {
                    winFlag = true;
                   
                    desAudio.PlayOneShot(winClip);

                }
                else
                {
                    failFlag = true;
                    //if (!desAudio.isPlaying)
                    desAudio.PlayOneShot(failClip);
                }

            }
            else
            {
                failFlag = true;
                //if (!desAudio.isPlaying)
                desAudio.PlayOneShot(failClip);
            }
            desFlag = true;
            names.Clear();
        }
    }

}
