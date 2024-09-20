using UnityEngine;
using System.Collections;   
using System.Collections.Generic;

public class DrawCurve : MonoBehaviour
{

    public List<Transform> gameOjbet_tran = new List<Transform>();
    private List<Vector3> point = new List<Vector3>();
    public GameObject CurveCube;
    public static DrawCurve dl;
    public int i = 0;
    public int j = 0;
    public float num = 100f;

    private bool flag1 = true;
    public GameObject parent;
    public void Init()
    {
        i = 0;
        j = 0;
        point = new List<Vector3>();
        for (int i = 0; i < num; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(gameOjbet_tran[0].position, gameOjbet_tran[1].position, i / num);
            Vector3 pos2 = Vector3.Lerp(gameOjbet_tran[1].position, gameOjbet_tran[2].position, i / num);
            Vector3 pos3 = Vector3.Lerp(gameOjbet_tran[2].position, gameOjbet_tran[3].position, i / num);
            Vector3 pos4 = Vector3.Lerp(gameOjbet_tran[3].position, gameOjbet_tran[4].position, i / num);


            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / num);
            var pos1_1 = Vector3.Lerp(pos2, pos3, i / num);
            var pos1_2 = Vector3.Lerp(pos3, pos4, i / num);

            //三
            var pos2_0 = Vector3.Lerp(pos1_0, pos1_1, i / num);
            var pos2_1 = Vector3.Lerp(pos1_1, pos1_2, i / num);

            //四
            Vector3 find = Vector3.Lerp(pos2_0, pos2_1, i / num);

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
            if (flag1)
            {
                if (i >= num)
                    return;
                GameObject.Instantiate(CurveCube, point[i], Quaternion.Euler(0, 0, 0), parent.transform);
            }
        }
        flag1 = false;
    }

    void Awake()
    {
        Init();
        dl = this;
        
    }
    private void Start()
    {
        

    }


    void Update()
    {
        
    }



}
