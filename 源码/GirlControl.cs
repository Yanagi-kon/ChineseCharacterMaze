using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlControl : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public Camera MyCamera;
    public Camera AidCamera;
    public float speed = 5;           //人物运动速度
    public float jumpSpeed = 10;
    private float currSpeed;
    public bool ground;
    Animator animator;
    AnimatorStateInfo info;
    public AudioSource chAudio;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();

        ground = true;
        animator = GetComponent<Animator>();
        chAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = myRigidbody.velocity;


        float v = Input.GetAxis("Vertical");

        float h = Input.GetAxis("Horizontal");

        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)//判断：如果不是误触键盘则执行下一语句
        {
            currSpeed = Mathf.Lerp(currSpeed, speed, 0.9f);
            transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime, AidCamera.transform);
            if (v >= 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan(h / v) * 360 / (2 * 3.1415f) + MyCamera.transform.eulerAngles.y, 0));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan(h / v) * 360 / (2 * 3.1415f) + MyCamera.transform.eulerAngles.y + 180, 0));
        }
        animator.SetFloat("Speed", Mathf.Sqrt((h * currSpeed) * (h * currSpeed) + (v * currSpeed) * (v * currSpeed)));

        if(Mathf.Abs(h) > 0.3f || Mathf.Abs(v) > 0.3f)
        {
            if (!chAudio.isPlaying)
            {
                
                int ran = Random.Range(0, 3);
                if (ran < 1)
                {
                    chAudio.PlayOneShot(clip1);
                }
                if (ran >= 1 &&ran<2)
                {
                    chAudio.PlayOneShot(clip2);
                }
                if (ran >2)
                {
                    chAudio.PlayOneShot(clip3);
                }
            }
            
        }
        if (Mathf.Abs(h) < 0.3f && Mathf.Abs(v) < 0.3f)
        {
            chAudio.Stop();
        }

    }
    
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Respawn" || collision.transform.tag == "Cube" || collision.transform.tag == "BlueCube" || collision.transform.tag == "YellowCube" || collision.transform.tag == "OrangeCube")
        {
            ground = true;
            //if(!isDoorMode)
            //Main_Audio.PlayOneShot(fallGroundClip);
        }
        if(collision.transform.tag=="Water")
        {
            this.gameObject.transform.position = new Vector3(115.46f, -69.97f, -31.07f);
        }
    }
}
