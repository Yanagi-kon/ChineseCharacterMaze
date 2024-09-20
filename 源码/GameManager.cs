using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UI1;
    
    //private GameObject getMouseText=new GameObject(); 
    public static GameManager instance;
    public GameObject gouUI;
    public GameObject dianUI;
    public GameObject tiUI;
    public GameObject naUI;
    public GameObject pieUI;

    public static GameObject lastCurveObject;
    private CurveData selectedCurve;
    public CurveData Curve1;//(������
    public CurveData Curve2;//���
    public CurveData Curve3;//�ҵ�
    public CurveData Curve4;//ƽ��
    public CurveData Curve5;//б��
    public CurveData Curve6;//ƽ��
    public CurveData Curve7;//��б��
    public CurveData Curve8;//����
    public CurveData Curve9;//��бƲ
    public CurveData Curve10;//��бƲ
    public CurveData Curve11;//ƽƲ
    public CurveData Curve12;//��Ʋ
    public CurveData Curve13; //����Ʋ
    public CurveData Curve14;//(�ᣩ��
    public CurveData Curve15;//��б��
    public CurveData Curve16;//�����бƲ
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        player = Ball.player.gameObject;
    }
    private void Update()
    {
        
        if(selectedCurve!=null)
        {
            //if(lastSelectedCurve.curvePrefab.gameObject)
            //    Destroy(lastSelectedCurve.curvePrefab.gameObject);
            if(lastCurveObject)
            {
                Destroy(lastCurveObject.gameObject);
            }
            lastCurveObject=GameObject.Instantiate(selectedCurve.curvePrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);  
            selectedCurve = null;
        }
        
        if (GetOverUI(UI1))
        {
            if (GetOverUI(UI1).name == "��")
            {
                if (gouUI.activeInHierarchy == false)
                    gouUI.SetActive(true);
            }
            else
                gouUI.SetActive(false);

            if (GetOverUI(UI1).name == "��")
            {
                if (dianUI.activeInHierarchy == false)
                    dianUI.SetActive(true);
            }
            else
                dianUI.SetActive(false);
            if (GetOverUI(UI1).name == "��")
            {
                if (tiUI.activeInHierarchy == false)
                    tiUI.SetActive(true);
            }
            else
                tiUI.SetActive(false);
            if (GetOverUI(UI1).name == "��")
            {
                if (naUI.activeInHierarchy == false)
                    naUI.SetActive(true);
            }
            else
                naUI.SetActive(false);
            if (GetOverUI(UI1).name == "Ʋ")
            {
                if (pieUI.activeInHierarchy == false)
                    pieUI.SetActive(true);
            }
            else
                pieUI.SetActive(false);
        }
        if (gouUI.activeInHierarchy == true && GetOverUI(gouUI))
            gouUI.SetActive(true);
        if((GetOverUI(UI1) && GetOverUI(UI1).name != "��") ||!GetOverUI(UI1) &&!GetOverUI(gouUI))
            gouUI.SetActive(false);

        if (dianUI.activeInHierarchy == true && GetOverUI(dianUI))
            dianUI.SetActive(true);
        if ((GetOverUI(UI1) && GetOverUI(UI1).name != "��") || !GetOverUI(UI1) && !GetOverUI(dianUI))
            dianUI.SetActive(false);

        if (tiUI.activeInHierarchy == true && GetOverUI(tiUI))
            tiUI.SetActive(true);
        if ((GetOverUI(UI1) && GetOverUI(UI1).name != "��") || !GetOverUI(UI1) && !GetOverUI(tiUI))
            tiUI.SetActive(false);

        if (naUI.activeInHierarchy == true && GetOverUI(naUI))
            naUI.SetActive(true);
        if ((GetOverUI(UI1) && GetOverUI(UI1).name != "��") || !GetOverUI(UI1) && !GetOverUI(naUI))
            naUI.SetActive(false);

        if (pieUI.activeInHierarchy == true && GetOverUI(pieUI))
        {
            pieUI.SetActive(true);
        }
        if ((GetOverUI(UI1) && GetOverUI(UI1).name != "Ʋ") || !GetOverUI(UI1) && !GetOverUI(pieUI))
            pieUI.SetActive(false);
    }

    

    

    //public void OnButtonRetry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    Destination.index = 0;
    //    Destination.realPath = new GameObject[12];
    //}


    public void OnButtonshugou() //����   
    {
        selectedCurve = Curve1;
    }
    public void OnButtonyoudian()  //���
    {
        selectedCurve = Curve2;
    }
    public void OnButtonzuodian() //�ҵ�
    {
        selectedCurve = Curve3;
    }
    public void OnButtonpingti() //ƽ��
    {
        selectedCurve = Curve4;
    }
    public void OnButtonxieti() //б��
    {
        selectedCurve = Curve5;
    }
    public void OnButtonpingna() //ƽ��
    {
        selectedCurve = Curve6;
    }
    public void OnButtonduanxiena() //��б��
    {
        selectedCurve = Curve7;
    }
    public void OnButtonfanna() //����
    {
        selectedCurve = Curve8;
    }
    public void OnButtonduanxiepie() //��бƲ
    {
        selectedCurve = Curve9;
    }
    public void OnButtonchangxiepie() //��бƲ
    {
        selectedCurve = Curve10;
    }
    public void OnButtonpingpie() //ƽƲ
    {
        selectedCurve = Curve11;
    }
    public void OnButtonshupie() //��Ʋ
    {
        selectedCurve = Curve12;
    }
    public void OnButtonshuwanpie() //����Ʋ
    {
        selectedCurve = Curve13;
    }
    public void OnButtonhenggou() //�Ṵ
    {
        selectedCurve = Curve14;
    }
    public void OnButtonchangxiena() //��б��
    {
        selectedCurve = Curve15;
    }
    public void OnButtonqiupie() //��б��
    {
        selectedCurve = Curve16;
    }


    /// <summary>
    /// ��ȡ���ͣ����UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            //Debug.Log(results[0].gameObject+" "+results[0].gameObject.transform.position);
            return results[0].gameObject;
        }

        return null;
    }

}
