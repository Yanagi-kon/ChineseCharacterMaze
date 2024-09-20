using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCamera : MonoBehaviour
{
    Animation animation;
    AnimatorStateInfo info;
    public Camera MainCamera;
    public GameObject character;
    float timer;
    bool flag=false;
    public GameObject mani;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (timer >= 1.0f)
            {
                this.gameObject.SetActive(false);
                character.GetComponent<GirlControl>().enabled = true;
                MainCamera.gameObject.SetActive(true);
                mani.SetActive(true);
                flag = false;
            }
            timer += Time.deltaTime;
        }

    }

    public void ButtonStart()
    {
        animation.Play();
        if (animation.isPlaying)
        {
            flag = true;
        }       
    }
}
