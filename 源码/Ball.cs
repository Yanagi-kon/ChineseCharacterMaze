using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody rbody;//刚体组件

    private AudioSource Main_Audio;
    public AudioClip food_Clip;
    public AudioClip port_Clip;
    public AudioClip failClip;

    public static float speed = 600f;
    public GameObject waterInk;
    private GameObject lastInk; 
    private GameObject currentInk;
    public float inkStep = 0.2f; //每个特效在x和z轴上应间隔的最小距离
    private float currentDistance;//当前player的位置减去上一次生成特效的位置
    private int flag1 = 0;
    private int flag2 = 0;
    private int flag3 = 0;
    private int flag4 = 0;
    public GameObject hitEffect;
    public GameObject starEffect;
    Quaternion zeroRotation = Quaternion.identity;//0角度

    //撤回操作相关量
    private List<GameObject> inkList = new List<GameObject>();
    private List<GameObject> path = new List<GameObject>();
    private int index=0;
    private Vector3 lastPosition;

    //穿墙了直接重开 每关手动填入x和z的限制范围
    public GameObject wall1;   //上
    public GameObject wall2;   //下
    public GameObject wall3;   //左
    public GameObject wall4;   //右
    private int pointCount = 0;
    public static Ball player;
    private int endi = 0;
    private float endTimer=0;
    public static List<GameObject> inks = new List<GameObject>();
    private int namesCount = 0;
    void Start()
    {
        lastInk = new GameObject();
        rbody = GetComponent<Rigidbody>();
        lastInk.transform.position = transform.position;//墨水位置
        lastPosition = transform.position;//初始保存playe位置 
        inkList.Clear();
        
        
    }

    private void Awake()
    {
        player = this;
        Main_Audio = GetComponent<AudioSource>();
    }
    public void print(string s)
    {
        Debug.Log(s);
    }
    void Update()
    {
        if(transform.position.z >= wall1.transform.position.z-0.2|| transform.position.z <= wall2.transform.position.z-0.2||
            transform.position.x <= wall3.transform.position.x + 0.3|| transform.position.x>= wall4.transform.position.x - 0.3)
        {
            Destination.failFlag = true;
            if(!Main_Audio.isPlaying)
            {
                Main_Audio.PlayOneShot(failClip);
            }
        }
        
        if (rbody.velocity == Vector3.zero && DrawLine.i == 80)
        {
            flag1 = 1;   
        }
        if (rbody.velocity == Vector3.zero && flag2 == 1)
        {
            flag3 = 1;
        }

        

        if (rbody.velocity == Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (GameManager.lastCurveObject)
                {
                    Destroy(GameManager.lastCurveObject.gameObject);
                    DrawLine.i = 80;
                }
                if (flag3==1)
                {
                    flag4 = 1;
                }
                rbody.AddForce(new Vector3(speed, 0, 0));
                flag2 = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (GameManager.lastCurveObject)
                {
                    Destroy(GameManager.lastCurveObject.gameObject);
                    DrawLine.i = 80;
                }
                if (flag3 == 1)
                {
                    flag4 = 1;
                }
                rbody.AddForce(new Vector3(-speed, 0, 0));
                flag2 = 1;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (GameManager.lastCurveObject)
                {
                    Destroy(GameManager.lastCurveObject.gameObject);
                    DrawLine.i = 80;
                }
                if (flag3 == 1)
                {
                    flag4 = 1;
                }
                rbody.AddForce(new Vector3(0, 0, speed));
                flag2 = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (GameManager.lastCurveObject)
                {
                    Destroy(GameManager.lastCurveObject.gameObject);
                    DrawLine.i = 80;
                }
                if (flag3 == 1)
                {
                    flag4 = 1;
                }
                rbody.AddForce(new Vector3(0, 0, -speed));
                flag2 = 1;
            }
            else if(Input.GetKeyDown(KeyCode.R))   //撤回
            {
                for(index=0;index<inkList.Count;index++)
                {
                    if (index == inkList.Count - 1)
                    {
                        inkList[index].SetActive(false);  //保留最后一个墨迹
                    }
                    else
                    {
                        
                        inks.Remove(inkList[index]);
                        Destroy(inkList[index]);
                    }
                    transform.position = lastPosition;
                }
                for(index=0;index<path.Count;index++)
                {
                    path[index].gameObject.SetActive(true); //星星 停止块 转向块 传送门恢复
                    
                    //if(path[index].gameObject.tag=="Path")
                    //{
                    //    path[index].GetComponent<Collider>().enabled = true;  //让失效的碰撞器恢复
                    //    pointCount++;
                    //}
                }
                if (Destination.names.Count > namesCount)//如果增加了说明上一步是笔画 要清空这一笔
                {
                    Destination.names.Remove(Destination.names[Destination.names.Count - 1]);
                }

                Destination.index -= pointCount;
                path.Clear();
                pointCount = 0;
            }
        }

        if (flag1 == 1 && DrawLine.i == 0 || (flag3 == 1 && flag4 == 1)) //前一半表示下一步是笔画 后一半表示下一步是横竖 开始了下一步就清空上一步的记录
        {
            //Debug.Log("这里是清空");
            inkList.Clear();
            path.Clear();
            flag1 = 0;
            flag2 = 1;
            flag3 = 0;
            flag4 = 0;
            lastPosition = transform.position;
            namesCount = Destination.names.Count;
        }

        // 在最小范围外才生成
        currentDistance = Mathf.Sqrt(Mathf.Pow((transform.position.x - lastInk.transform.position.x),2)+ Mathf.Pow((transform.position.z- lastInk.transform.position.z), 2));
        //Debug.Log("current distance:" + currentDistance);   
        if (currentDistance >= inkStep)       
        {
            currentInk = GameObject.Instantiate(waterInk, new Vector3(transform.position.x,transform.position.y,transform.position.z), zeroRotation);
            lastInk = currentInk;
            inkList.Add(currentInk); //将生成的墨迹加入表中(一笔的墨迹）
            inks.Add(currentInk);//全部的墨迹
            //Debug.Log("加入了：" + inks.Count);
        }
        
        if(Destination.winFlag) //胜利后让墨水升起
        {
            endTimer += Time.deltaTime;
            
            for(endi=0;endi<inks.Count;endi++)
            {
                if (endTimer > 2) //上升2秒
                {
                    return;
                }
                inks[endi].transform.Translate(Vector3.up * Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Star")
        {
            GameObject stareff= GameObject.Instantiate(starEffect, other.transform.position, other.transform.rotation);
            Destroy(stareff, 3);
            other.gameObject.SetActive(false);
            Main_Audio.PlayOneShot(food_Clip);
        }

        if (other.gameObject.tag == "Portal")
        {
            Main_Audio.PlayOneShot(port_Clip);
            GameObject heff = GameObject.Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(heff, 3);
        }
        
        if(other.gameObject.tag=="Path"|| other.gameObject.tag =="Star"|| other.gameObject.tag =="Portal"
            ||other.gameObject.tag=="StopCube"|| other.gameObject.tag == "TurnCube")  //记录过程碰到的东西记录下来
        {
           
            path.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
           
    }

}

