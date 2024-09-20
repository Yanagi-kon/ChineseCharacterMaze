using UnityEngine;
using System.Collections;   
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{

    public List<Transform> gameOjbet_tran = new List<Transform>();
    private List<Vector3> point = new List<Vector3>();

    public GameObject player;
    public static DrawLine dl;

    public static int i = 0;
    public int j = 0;
    
    bool flag = false;
 

    public void Init()
    {
        i = 0;
        j = 0;
        point = new List<Vector3>();
        for (int i = 0; i < 80; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(gameOjbet_tran[0].position, gameOjbet_tran[1].position, i / 80f);
            Vector3 pos2 = Vector3.Lerp(gameOjbet_tran[1].position, gameOjbet_tran[2].position, i / 80f);
            Vector3 pos3 = Vector3.Lerp(gameOjbet_tran[2].position, gameOjbet_tran[3].position, i / 80f);
            Vector3 pos4 = Vector3.Lerp(gameOjbet_tran[3].position, gameOjbet_tran[4].position, i / 80f);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / 80f);
            var pos1_1 = Vector3.Lerp(pos2, pos3, i / 80f);
            var pos1_2 = Vector3.Lerp(pos3, pos4, i / 80f);

            //三
            var pos2_0 = Vector3.Lerp(pos1_0, pos1_1, i / 80f);
            var pos2_1 = Vector3.Lerp(pos1_1, pos1_2, i / 80f);

            //四
            Vector3 find = Vector3.Lerp(pos2_0, pos2_1, i / 80f);

            point.Add(find);
            
        }
        //Debug.Log("point count:"+point.Count);
    }

    void OnDrawGizmos()//画线
    {
        Init();
        Gizmos.color = Color.yellow;
        for (int i = 0; i < point.Count - 1; i++)
        {
            Gizmos.DrawLine(point[i], point[i + 1]);
        }
    }

    void Awake()
    {
        Init();
        dl = this;
        
    }
    private void Start()
    {
        player = Ball.player.gameObject;

    }

    //player.transform.GetComponent<Collider>().enabled = false;
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Q) || flag)
        {
            flag = true;
            StartCoroutine("CurveMove", flag);
        }
        
    }


    IEnumerator CurveMove(bool flag)
    {
        Debug.Log("this is curvemove");
        if (i == 10)
        {
            Debug.Log("this is i=10");
            Destination.names.Add(this.gameObject.name);
            Cursor.lockState= CursorLockMode.Locked;
            for (int i = 0; i < Destination.names.Count; i++)
                Debug.Log(Destination.names[i]);
        }
        if (i >= point.Count)
        {
            Cursor.lockState = CursorLockMode.None;
            Destroy(this.gameObject);
            flag = false;
            yield return 0;
        }
        player.transform.position = point[i];
        i++;
    }


}
