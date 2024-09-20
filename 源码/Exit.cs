using UnityEngine;

public class Exit : MonoBehaviour
{
    public void exit()
    {
        /*将状态设置false才能退出游戏*/
        LevelManager.Save();

        Application.Quit();
    }
}
