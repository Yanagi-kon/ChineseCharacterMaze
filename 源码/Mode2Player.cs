using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode2Player : MonoBehaviour
{
    private Rigidbody myRigidbody;
    public Camera MyCamera;
    public Camera AidCamera;
    public GameObject starEffect;
    public float speed = 5;           //人物运动速度
    private float currSpeed;
    private float currUpSpeed;
    private Vector3 offset;
    public float initAngle;  //初始玩家的y角度
    public float jumpSpeed = 10;

    private bool ground;

    private AudioSource Main_Audio;
    public AudioClip food_Clip;
    public AudioClip fallGroundClip;
    public AudioClip flyClip1;
    public AudioClip flyClip2;
    public AudioClip colorClip;
    public AudioClip failClip;
    public static Mode2Player player;
    public static int score = 0;
    Animator animator;
    Animator hatAnimator;
    public bool isDoorMode = false;
    public bool isFlyMode = false;
    public bool isColorMode = false;
    public bool isLongMode = false;
    public bool isDuckMode = false;
    public List<GameObject> cylinderList = new List<GameObject>();
    public float upSpeed = 7;    //上升速度
    private float timer = 0;
    private void Awake()
    {
        player = this;
        Main_Audio = GetComponent<AudioSource>();
    }
  

    private void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();

        offset = this.transform.position;//记录人物初始位置

        ground = true;

        animator = GetComponent<Animator>();//获取当前对象的Animator 

        if (isColorMode)
            hatAnimator = this.transform.Find("CowboyHat").GetComponent<Animator>();
        else
            hatAnimator = null;
    }



    private void FixedUpdate()
    {


        timer += Time.deltaTime;
        //人物当前运动速度
        Vector3 vel = myRigidbody.velocity;

        currUpSpeed = vel.y;
        if (currUpSpeed < -7)
            currUpSpeed = 0;


        float v = Input.GetAxis("Vertical");

        float h = Input.GetAxis("Horizontal");

        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)//判断：如果不是误触键盘则执行下一语句
        {
            currSpeed = Mathf.Lerp(currSpeed, speed, 0.9f);
            transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime, AidCamera.transform);
            if(v>=0)
                transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan(h / v) * 360 / (2 * 3.1415f) + MyCamera.transform.eulerAngles.y + initAngle, 0));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan(h / v) * 360 / (2 * 3.1415f) + MyCamera.transform.eulerAngles.y + initAngle + 180, 0));
            if (hatAnimator)
                hatAnimator.Play("hat");
        }
        else
        {
            currSpeed = 0;
            if (hatAnimator)
                hatAnimator.Play("New State");
        }
        animator.SetFloat("Speed", Mathf.Sqrt((h * currSpeed) * (h * currSpeed) + (v * currSpeed) * (v * currSpeed))+ currUpSpeed);

        //flymode
        if (isFlyMode && !ground && (Mathf.Sqrt((h * currSpeed) * (h * currSpeed) + (v * currSpeed) * (v * currSpeed)) + currUpSpeed)>1.0f &&timer>0.65f)
        {
            if (timer > 1.3f)
            {
                Main_Audio.PlayOneShot(flyClip1);
                timer = 0;

                return;
            }
            if (timer < 0.67f)
            {
                Main_Audio.PlayOneShot(flyClip2);
            }

        }
    }

    private void LateUpdate()
    {
        if (!isDoorMode)
        {
            if (isFlyMode)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, upSpeed, 0);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (ground == true)
                    {
                        GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
                        ground = false;

                    }
                }
            }
        
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Respawn" && isDuckMode)|| collision.transform.tag == "Cube" || collision.transform.tag == "BlueCube"|| collision.transform.tag == "YellowCube"|| collision.transform.tag == "OrangeCube")
        {
            ground = true;
            //if(!isDoorMode)
                //Main_Audio.PlayOneShot(fallGroundClip);
        }
        if (collision.gameObject.tag == "Water" && isLongMode)//长关 落下后回到圆柱上
        {
            if (cylinderList.Count > 0 && cylinderList[cylinderList.Count - 1].gameObject.activeSelf)
                this.gameObject.transform.position = new Vector3(cylinderList[cylinderList.Count - 1].transform.position.x,
                    cylinderList[cylinderList.Count - 1].transform.position.y+3, cylinderList[cylinderList.Count - 1].transform.position.z);
            if(cylinderList.Count > 0 && !cylinderList[cylinderList.Count - 1].gameObject.activeSelf&& Mode2Player.score<mode2Manager.maxScore)//如果落到地上而且上一个碰到的柱子已经消失了就失败
            {
                //失败flag
                mode2Manager.failFlag2 = true;
                this.gameObject.SetActive(false);
                Main_Audio.PlayOneShot(failClip);
            }
        }

        if((collision.gameObject.tag=="Water" && !isLongMode && Mode2Player.score < mode2Manager.maxScore) || (collision.gameObject.tag=="Respawn" && Mode2Player.score < mode2Manager.maxScore))
        {
            Debug.Log(isDuckMode);
            if (!isDuckMode)
            {
                mode2Manager.failFlag2 = true;
                this.gameObject.SetActive(false);
                Main_Audio.PlayOneShot(failClip);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Cube" && isFlyMode)
            ground = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Star")
        {
            GameObject stareff = GameObject.Instantiate(starEffect,other.transform.position,other.transform.rotation);//实例化吃星星特效
            score += 1;
            other.gameObject.tag = "Destination";
            Destroy(other.gameObject);
            Main_Audio.PlayOneShot(food_Clip);
            Destroy(stareff, 3);
        }
        if (other.gameObject.tag == "Cube" && isLongMode)
        {
            cylinderList.Add(other.gameObject);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(isColorMode && other.gameObject.tag=="BlueCube"|| other.gameObject.tag == "YellowCube"|| other.gameObject.tag == "OrangeCube")
        {
            Main_Audio.PlayOneShot(colorClip);
        }
    }

}


