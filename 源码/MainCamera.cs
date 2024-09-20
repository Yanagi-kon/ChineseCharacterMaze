using UnityEngine;
using System.Collections;


public class MainCamera : MonoBehaviour
{
    public float distance_v;
    public float distance_h;
    public float rotation_H_speed = 1;
    public float rotation_V_speed = 1;
    public float max_up_angle = 80;              //越大，头抬得越高
    public float max_down_angle = -60;            //越小，头抬得越低
    public GameObject player;        //player
    private float current_rotation_H;      //水平旋转结果
    private float current_rotation_V;  //垂直旋转结果
    public Camera followCamera;
    public Camera AidCamera;

    //public AudioClip lastStarClip;
    private void Start()
    {
        followCamera.enabled = true;


    }

    void LateUpdate()
    {
        if (!LevelManager.isPausing)
        {
            //控制旋转
            current_rotation_H += Input.GetAxis("Mouse X") * rotation_H_speed;
            current_rotation_V += Input.GetAxis("Mouse Y") * rotation_V_speed;
            current_rotation_V = Mathf.Clamp(current_rotation_V, max_down_angle, max_up_angle);       //限制垂直旋转角度
            transform.localEulerAngles = new Vector3(-current_rotation_V, current_rotation_H, 0f);

            //改变位置，以跟踪的目标为视野中心，且视野中心总是面向target
            if (player)
                transform.position = player.transform.position;
            transform.Translate(Vector3.back * distance_h, Space.Self);
            transform.Translate(Vector3.up * distance_v, Space.World);          //相对于世界坐标y轴向上

            AidCamera.transform.rotation = Quaternion.Euler(new Vector3(AidCamera.transform.eulerAngles.x, this.transform.eulerAngles.y, AidCamera.transform.eulerAngles.z));
        }
    }

}
