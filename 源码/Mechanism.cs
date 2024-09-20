using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public float speed = 1f;
    private float timer = 0;
    public int type=1;
    private Vector3 initialPosition;
    private int end=0;
    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = transform.position;
        //Debug.Log(this.gameObject+" initialPosition:"+initialPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mode2Player.score >= mode2Manager.maxScore && end == 0)
        {
            end = 1;
            transform.position = initialPosition;
        }
        if (end == 1) return;   
        if (type == 1)
        {
            timer += Time.deltaTime;
            if (timer < 2)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (timer > 4)
                timer = 0;
        }
        if(type==2)
        {
            timer += Time.deltaTime;
            if (timer < 2.5)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (timer > 5)
                timer = 0;
        }
        if (type == 3)
        {
            timer += Time.deltaTime;
            if (timer < 3)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (timer > 6)
                timer = 0;
        }
        if (type==4)
        {
            timer += Time.deltaTime;
            if (timer < 3.5)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (timer > 7)
                timer = 0;
        }

        
    }
}
