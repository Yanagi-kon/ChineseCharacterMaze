using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void OnClick()
    {
         /*将状态设置false才能退出游戏*/

        Application.Quit();
    }
}
