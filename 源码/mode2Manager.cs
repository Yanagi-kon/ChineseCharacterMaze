using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class mode2Manager : MonoBehaviour
{
    public GameObject player;
    private AudioSource ManagerAudio;
    public AudioClip winClip;
    
    public static bool Failed=false;
    public static bool win = false;
    //[SerializeField]
    //public List<GameObject> stars = new List<GameObject>();
    public bool isColorMode = false;
    
    private bool isBegin=true;
    private float timer = 0;
    private float timer2 = 0;
    public int MaxScore; //在面板给静态的score赋值
    Animator animator1;
    Animator animator2;
    Animator animator3;
    Animator animator4;
    /// <summary>
    /// 建立四个gameobject，用来获取他们的Animator
    /// </summary>
    public GameObject anim1;
    public GameObject anim2;
    public GameObject anim3;
    public GameObject anim4;

    public string str1;
    public string str2;
    public string str3;
    public string str4;

    public GameObject[] ToBeDestroy = new GameObject[10];
    public Text starGot;
    public static int maxScore;
    public float waitTime;
    public GameObject birthdaySpark;
    public static bool winFlag2 = false;
    public static bool failFlag2 = false;
    void Start()
    {
        ManagerAudio = GetComponent<AudioSource>();
        maxScore = MaxScore;
        player = Mode2Player.player.gameObject;
        if(anim1)  animator1 = anim1.GetComponent<Animator>();
        if (anim2) animator2 = anim2.GetComponent<Animator>();
        if (anim3) animator3 = anim3.GetComponent<Animator>();
        if (anim4) animator4 = anim4.GetComponent<Animator>();
        if (isColorMode && isBegin)
        {
            CameraController.isfollow = false;
            player.gameObject.SetActive(false);
            if (str1.Length > 0)
                animator1.SetTrigger(str1);  //色字的分离+圆柱下落动画 
            if (str2.Length > 0)
                animator2.SetTrigger(str2);
            if (str3.Length > 0)
                animator3.SetTrigger(str3);
            if (str4.Length > 0)
                animator4.SetTrigger(str4);
        }
    }

    void Update()
    {
        starGot.text = Mode2Player.score + " / "+ maxScore;
        if (isColorMode)
        {
            timer += Time.deltaTime;
            if (timer > 5.0f && timer2<0.03f)
            {
                CameraController.isfollow = true;
                player.gameObject.SetActive(true);
                timer2 = 0.03f;
            }
            
            if (Mode2Player.score >= maxScore)//游戏胜利 
            {
                timer2 += Time.deltaTime;
                
            }
            if (timer2 > 1.0f)
            {
                
                ManagerAudio.Play();
                CameraController.isfollow = true;
                winFlag2 = true;
            }
        }
        if (!isColorMode)
        {
            if (Mode2Player.score >= maxScore)//游戏胜利
            {
                timer2 += Time.deltaTime;
                if (timer2 > 1.5f)
                {
                    if (str1.Length > 0)
                        animator1.SetTrigger(str1);
                    if (str2.Length > 0)
                        animator2.SetTrigger(str2);
                    if (str3.Length > 0)
                        animator3.SetTrigger(str3);
                    if (str4.Length > 0)
                        animator4.SetTrigger(str4);
                }

                int i = 0;
                while (ToBeDestroy[i] != null) //销毁要销毁的物体
                {
                    GameObject spark = GameObject.Instantiate(birthdaySpark, ToBeDestroy[i].transform.position, ToBeDestroy[i].transform.rotation);
                    Destroy(spark, 5);
                    Destroy(ToBeDestroy[i]);
                    i++;
                }
                if (timer2 > waitTime && timer2<waitTime+0.03f) //等待时间后播放胜利音乐
                {
                    ManagerAudio.clip = winClip;
                    ManagerAudio.Play();
                    //胜利flag
                    winFlag2 = true;
                }
            }
        }
        if (player!=null && player.transform.position.y<-100) //player高度低于-100就重开
        {
            failFlag2 = true;
        }
        
    }

    
}
