using UnityEngine;
using System.Collections;


public class MainCamera : MonoBehaviour
{
    public float distance_v;
    public float distance_h;
    public float rotation_H_speed = 1;
    public float rotation_V_speed = 1;
    public float max_up_angle = 80;              //Խ��ͷ̧��Խ��
    public float max_down_angle = -60;            //ԽС��ͷ̧��Խ��
    public GameObject player;        //player
    private float current_rotation_H;      //ˮƽ��ת���
    private float current_rotation_V;  //��ֱ��ת���
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
            //������ת
            current_rotation_H += Input.GetAxis("Mouse X") * rotation_H_speed;
            current_rotation_V += Input.GetAxis("Mouse Y") * rotation_V_speed;
            current_rotation_V = Mathf.Clamp(current_rotation_V, max_down_angle, max_up_angle);       //���ƴ�ֱ��ת�Ƕ�
            transform.localEulerAngles = new Vector3(-current_rotation_V, current_rotation_H, 0f);

            //�ı�λ�ã��Ը��ٵ�Ŀ��Ϊ��Ұ���ģ�����Ұ������������target
            if (player)
                transform.position = player.transform.position;
            transform.Translate(Vector3.back * distance_h, Space.Self);
            transform.Translate(Vector3.up * distance_v, Space.World);          //�������������y������

            AidCamera.transform.rotation = Quaternion.Euler(new Vector3(AidCamera.transform.eulerAngles.x, this.transform.eulerAngles.y, AidCamera.transform.eulerAngles.z));
        }
    }

}
