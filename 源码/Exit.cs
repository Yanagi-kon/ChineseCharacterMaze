using UnityEngine;

public class Exit : MonoBehaviour
{
    public void exit()
    {
        /*��״̬����false�����˳���Ϸ*/
        LevelManager.Save();

        Application.Quit();
    }
}
