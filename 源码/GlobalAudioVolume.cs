using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KenshinGame
{
    /// <summary>
    /// 全局音量控制
    /// </summary>
    public class GlobalAudioVolume : MonoBehaviour
    {
        public Slider slider;
        [Header("鼠标左键松开隐藏滑动条")]
        public bool hideOnLeftButtonUp = true;
        bool onSliderAdjust = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void SetVolume()
        {
            AudioListener.volume = slider.value;
            onSliderAdjust = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!hideOnLeftButtonUp)
            {
                return;
            }

            if(onSliderAdjust)
            {
                if(Input.GetMouseButtonUp(0))
                {
                    onSliderAdjust = false;

                    slider.gameObject.SetActive(false);
                }
            }
        }
    }
}

