using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image : MonoBehaviour
{
    public RawImage fly;
    public float speed = 0.003f;
    //͸����alphaֵ��ֵ����0-1
    private float ColorAlpha = 0;
    //һ������ʱ��Ĳ���
    private float timer = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        fly.GetComponent<RawImage>().color = new Color(255, 255, 255, ColorAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mode2Player.score>=mode2Manager.maxScore) //ʤ��
        {
            timer += Time.deltaTime;
           
            if (timer < 3.5)
                return;
            if(timer>3.6)
            {
                if (ColorAlpha > 0.95)
                    return;

                ColorAlpha += speed;
                timer = 3.5f;
            }
            
        }
        if (!mode2Manager.winFlag2)
        {
            fly.GetComponent<RawImage>().color = new Color(255, 255, 255, ColorAlpha);
            Debug.Log("winfalg=false");
        }
        if(mode2Manager.winFlag2)
            Destroy(fly);

    }

}
