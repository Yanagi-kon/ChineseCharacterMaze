using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody rbody;//�������

    private AudioSource Main_Audio;
    public AudioClip food_Clip;
    public AudioClip port_Clip;
    public AudioClip failClip;

    public static float speed = 600f;
    public GameObject waterInk;
    private GameObject lastInk; 
    private GameObject currentInk;
    public float inkStep = 0.2f; //ÿ����Ч��x��z����Ӧ�������С����
    private float currentDistance;//��ǰplayer��λ�ü�ȥ��һ��������Ч��λ��
    private int flag1 = 0;
    private int flag2 = 0;
    private int flag3 = 0;
    private int flag4 = 0;
    public GameObject hitEffect;
    public GameObject starEffect;
    Quaternion zeroRotation = Quaternion.identity;//0�Ƕ�

    //���ز��������
    private List<GameObject> inkList = new List<GameObject>();
    private List<GameObject> path = new List<GameObject>();
    private int index=0;
    private Vector3 lastPosition;

    //��ǽ��ֱ���ؿ� ÿ���ֶ�����x��z�����Ʒ�Χ
    public GameObject wall1;   //��
    public GameObject wall2;   //��
    public GameObject wall3;   //��
    public GameObject wall4;   //��
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
        lastInk.transform.position = transform.position;//īˮλ��
        lastPosition = transform.position;//��ʼ����playeλ�� 
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
            else if(Input.GetKeyDown(KeyCode.R))   //����
            {
                for(index=0;index<inkList.Count;index++)
                {
                    if (index == inkList.Count - 1)
                    {
                        inkList[index].SetActive(false);  //�������һ��ī��
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
                    path[index].gameObject.SetActive(true); //���� ֹͣ�� ת��� �����Żָ�
                    
                    //if(path[index].gameObject.tag=="Path")
                    //{
                    //    path[index].GetComponent<Collider>().enabled = true;  //��ʧЧ����ײ���ָ�
                    //    pointCount++;
                    //}
                }
                if (Destination.names.Count > namesCount)//���������˵����һ���Ǳʻ� Ҫ�����һ��
                {
                    Destination.names.Remove(Destination.names[Destination.names.Count - 1]);
                }

                Destination.index -= pointCount;
                path.Clear();
                pointCount = 0;
            }
        }

        if (flag1 == 1 && DrawLine.i == 0 || (flag3 == 1 && flag4 == 1)) //ǰһ���ʾ��һ���Ǳʻ� ��һ���ʾ��һ���Ǻ��� ��ʼ����һ���������һ���ļ�¼
        {
            //Debug.Log("���������");
            inkList.Clear();
            path.Clear();
            flag1 = 0;
            flag2 = 1;
            flag3 = 0;
            flag4 = 0;
            lastPosition = transform.position;
            namesCount = Destination.names.Count;
        }

        // ����С��Χ�������
        currentDistance = Mathf.Sqrt(Mathf.Pow((transform.position.x - lastInk.transform.position.x),2)+ Mathf.Pow((transform.position.z- lastInk.transform.position.z), 2));
        //Debug.Log("current distance:" + currentDistance);   
        if (currentDistance >= inkStep)       
        {
            currentInk = GameObject.Instantiate(waterInk, new Vector3(transform.position.x,transform.position.y,transform.position.z), zeroRotation);
            lastInk = currentInk;
            inkList.Add(currentInk); //�����ɵ�ī���������(һ�ʵ�ī����
            inks.Add(currentInk);//ȫ����ī��
            //Debug.Log("�����ˣ�" + inks.Count);
        }
        
        if(Destination.winFlag) //ʤ������īˮ����
        {
            endTimer += Time.deltaTime;
            
            for(endi=0;endi<inks.Count;endi++)
            {
                if (endTimer > 2) //����2��
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
            ||other.gameObject.tag=="StopCube"|| other.gameObject.tag == "TurnCube")  //��¼���������Ķ�����¼����
        {
           
            path.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
           
    }

}

