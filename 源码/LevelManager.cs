using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    BoxCollider BoxCollider;
    Vector3 point;
    public Button button;
    public GameObject MainCamera;
    public Camera InitCamera;
    public GameObject character;
    public Canvas UICanvas;
    public GameObject clickEffect;
    public GameObject winUI;
    public GameObject failUI;
    public GameObject settingUI;
    public GameObject pauseUI;
    public GameObject pauseButton;
    public GameObject LevelSelectUI;
    public GameObject guoChangUI;
    public GameObject passUI;
    public int levelSelect; //�ؿ�ѡ�� ��ΧΪ0-13
    public GameObject[] LevelPosition = new GameObject[14];//�������14��λ�ú�prefab
    public GameObject[] LevelPrefab = new GameObject[14];
    public GameObject[] SelectButtons = new GameObject[14];
    public GameObject[] LevelDescription = new GameObject[14];
    public GameObject[] Knowledge = new GameObject[14];
    public GameObject[] KnowledgeButton = new GameObject[14];
    public static bool hasOpened = false;
    public static bool replayFlag = false;
    private GameObject existScene;
    private float timer = 0;
    private static int maxLevel;
    public static bool isPausing = false;
    public bool hasCloseKnowledge = false;
    public static bool allEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        Load();
        button.gameObject.SetActive(false);
        for (int j = 0; j <= maxLevel; j++)
            SelectButtons[j].GetComponent<Button>().interactable = true;
        for (int j = 0; j <= maxLevel; j++)
            KnowledgeButton[j].GetComponent<Button>().interactable = true;
        passUI.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.timeScale);
        if(Input.GetKeyDown(KeyCode.Escape)&& !settingUI.activeInHierarchy && hasOpened && !mode2Manager.winFlag2 &&!mode2Manager.failFlag2&&!Destination.winFlag&&!Destination.failFlag&&Time.timeScale!=0)
        {
            Cursor.visible = true;
            PauseButton();
        }

        if (Input.GetKeyDown(KeyCode.F) && button.IsActive() && !hasOpened)
        {
            LevelSelectUI.SetActive(true);
        }
        if(hasOpened)
            LevelSelectUI.SetActive(false);

        if (Input.GetKeyDown(KeyCode.E) && !hasOpened)
        {

            BackToInit();
        }
     

        //ʤ�� ʧ�ܵ�UI��ʾ
        if (Destination.winFlag && Destination.desFlag ||mode2Manager.winFlag2)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                Win();
            }
        }
        else if (Destination.failFlag && Destination.desFlag || Destination.failFlag)
        {
                Fail();
        }

        if(mode2Manager.failFlag2) //ʧ����
        {
            Fail();
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4f);//����������
        //    point = Camera.main.ScreenToWorldPoint(point);//����Ļ�ռ�ת��������ռ�
        //    GameObject go = Instantiate(clickEffect);//������Ч
        //    go.transform.position = point;
        //    Destroy(go, 1f);
        //}

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            button.gameObject.SetActive(true);
            //Debug.Log("�����ˣ�");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            button.gameObject.SetActive(false);
            //Debug.Log("�뿪����");
        }
    }
    public void Win()
    {
        Debug.Log("win����");
        Cursor.visible = true;
        pauseButton.SetActive(false);
        if (!hasCloseKnowledge)
            Knowledge[levelSelect].SetActive(true);       //hasCloseKnowledgeΪ���levelSelect+1����������Ӧ������levelSelect-1Ϊfalse
        else
            Knowledge[levelSelect - 1].SetActive(false);
        
      
        if (hasCloseKnowledge)
            winUI.SetActive(true);
        
    }
    public void Fail()
    {
        failUI.SetActive(true);
        character.SetActive(false);
        pauseButton.SetActive(false);
        isPausing = true;
        Cursor.visible = true;
    }


    public void NextLevel()
    {
        
        Cursor.visible = true;
        hasCloseKnowledge = false;
        if (levelSelect<=13)
        {
            
            Destroy(existScene);
            for(int j=0;j<Ball.inks.Count;j++)
                Destroy(Ball.inks[j]);
            Ball.inks.Clear();
            existScene = GameObject.Instantiate(LevelPrefab[levelSelect], LevelPosition[levelSelect].transform.position, LevelPosition[levelSelect].transform.rotation);
            //Debug.Log("�����ǣ�"+levelSelect+":"+ existScene);
            LevelDescriptionButton();
            replayFlag = false;
            Mode2Player.score = 0;
            Destination.winFlag = false;
            Destination.failFlag = false;
            Destination.desFlag = false;
            mode2Manager.winFlag2 = false;
            mode2Manager.failFlag2 = false;
            Destination.index = 0;
            timer = 0;
            winUI.SetActive(false);
            failUI.SetActive(false);
            pauseButton.SetActive(true);
            isPausing = false;
            hasOpened = true;
            Destination.names.Clear();
            
            InitCamera.gameObject.SetActive(false);
        }
        guoChangUI.SetActive(false);
    }

 
    public void RePlay()  //���¿�ʼ
    {
        MainCamera.SetActive(false);
        Invoke("LevelDescriptionButton", 0.8f);
        Cursor.visible = true;
        hasCloseKnowledge = false;
        LevelSelectUI.SetActive(false);
        pauseUI.SetActive(false);
        Destroy(existScene);
        for (int j = 0; j < Ball.inks.Count; j++)
            Destroy(Ball.inks[j]);
        Ball.inks.Clear();
        existScene = GameObject.Instantiate(LevelPrefab[levelSelect], LevelPosition[levelSelect].transform.position, LevelPosition[levelSelect].transform.rotation);
        //LevelDescriptionButton();
        Mode2Player.score = 0;
        ColorManager.color = "none";
        if (levelSelect==7) //ɫ��
            CameraController.isfollow = false;
        else
            CameraController.isfollow = true;
        replayFlag = false;
        Destination.winFlag = false;
        Destination.desFlag = false;
        Destination.failFlag = false;
        mode2Manager.winFlag2 = false;
        mode2Manager.failFlag2 = false;
        Destination.index = 0;
        Destination.names.Clear();
        timer = 0;
        Time.timeScale = 1;
        winUI.SetActive(false);
        failUI.SetActive(false);
        pauseUI.SetActive(false);
        
        isPausing = false;
        hasOpened = true;
        pauseButton.SetActive(true);
        //if (levelSelect == 0 || levelSelect == 2 || levelSelect == 4 || levelSelect == 8 || levelSelect == 10 || levelSelect == 12)
        //    Cursor.visible = true;
        //else
        //    Cursor.visible = false;
        InitCamera.gameObject.SetActive(false);
    }

    public void ContinueGame() //������Ϸ
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPausing = false;
        settingUI.SetActive(false);
        if (hasOpened)
        {
            pauseButton.SetActive(true);
            if (levelSelect == 0 || levelSelect == 2 || levelSelect == 4 || levelSelect == 8 || levelSelect == 10 || levelSelect == 12)
                Cursor.visible = true;
            else
                Cursor.visible = false;
        }
    }

    public void BackToInit() //�ص���ҳ
    {
        Time.timeScale = 1;
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Ball.inks.Clear();
        Mode2Player.score = 0;
        ColorManager.color = "none";
        hasOpened = false;
        Destination.winFlag = false;
        Destination.desFlag = false;
        Destination.failFlag = false;
        mode2Manager.winFlag2 = false;
        mode2Manager.failFlag2 = false;
        hasCloseKnowledge = false;
        isPausing = false;
        Destination.index = 0;
        
        winUI.SetActive(false);
        failUI.SetActive(false);
        pauseUI.SetActive(false);
        pauseButton.SetActive(false);
        Destination.names.Clear();
        Cursor.visible = true;
        Debug.Log("max:" + maxLevel);
        for (int j = 0; j <= maxLevel; j++)
            SelectButtons[j].GetComponent<Button>().interactable = true;
        InitCamera.gameObject.SetActive(true);
    }

    public void LevelDescriptionButton()
    {
        Time.timeScale = 0;
        
        pauseUI.SetActive(false);
        LevelDescription[levelSelect].SetActive(true);
        
        Cursor.visible = true;
    }
    public void CloseLevelDescription()//�رչؿ�˵������֪���ˣ�
    {
        LevelDescription[levelSelect].SetActive(false);
        ContinueGame();
        
        //pauseUI.SetActive(false);
        //Time.timeScale = 1;

        //if (levelSelect == 0 || levelSelect == 2 || levelSelect == 4 || levelSelect == 8 || levelSelect == 10 || levelSelect == 12)
        //    Cursor.visible = true;
        //else
        //    Cursor.visible = false;
    }
    public void Setting()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }
    public void CloseSetting()
    {
        ContinueGame();


    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        isPausing = true;
    }

    public void LevelSelectButton(int num)
    {
        
        levelSelect = num;
        button.gameObject.SetActive(false);
        LevelSelectUI.SetActive(false);
        character.gameObject.SetActive(false);
        InitCamera.gameObject.SetActive(false);
        Destination.names.Clear();
        hasOpened = true;
        Invoke("RePlay", 0.8f);
        //RePlay();
    }

    public void BackToPointButton()
    {
        
        Invoke("BackToScene", 0.8f);
        Time.timeScale = 1;
        Debug.Log("����");
    }

    public void BackToScene()  //�뿪�Ծ�
    {
        Debug.Log("�뿪��");
        Time.timeScale = 1;
        MainCamera.SetActive(true);
        Destroy(existScene);
        for (int j = 0; j < Ball.inks.Count; j++)
            Destroy(Ball.inks[j]);
        Ball.inks.Clear();
        Mode2Player.score = 0;
        ColorManager.color = "none";
        hasOpened = false;
        Destination.winFlag = false;
        Destination.desFlag = false;
        Destination.failFlag = false;
        mode2Manager.winFlag2 = false;
        mode2Manager.failFlag2 = false;
        hasOpened = false;
        isPausing = false;
        hasCloseKnowledge = true;
        Destination.index = 0;
        Destination.names.Clear();
        
        timer = 0;
        winUI.SetActive(false);
        failUI.SetActive(false);
        pauseUI.SetActive(false);
        pauseButton.SetActive(false);
        character.SetActive(true);
        Cursor.visible = true;
        Debug.Log("max:" + maxLevel);
        for (int j = 0; j <= maxLevel; j++)
            SelectButtons[j].GetComponent<Button>().interactable = true;
        for (int j = 0; j <= maxLevel; j++)
            KnowledgeButton[j].GetComponent<Button>().interactable = true;
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("maxLevel", LevelManager.maxLevel);
        Debug.Log("�����" + LevelManager.maxLevel);
        PlayerPrefs.Save();
    }
    public static void Load()
    {
        LevelManager.maxLevel = PlayerPrefs.GetInt("maxLevel");
    }

    public void CloseKnowledge()
    {
        //����֪������ζ�Ž�����һ��
        if (levelSelect == 13)
        {
            allEnd = true;
            MainCamera.SetActive(false);
            Destroy(existScene);
            for (int j = 0; j < Ball.inks.Count; j++)
                Destroy(Ball.inks[j]);
            Ball.inks.Clear();
            Mode2Player.score = 0;
            ColorManager.color = "none";
            hasOpened = false;
            Destination.winFlag = false;
            Destination.desFlag = false;
            Destination.failFlag = false;
            mode2Manager.winFlag2 = false;
            mode2Manager.failFlag2 = false;
            hasOpened = false;
            isPausing = false;
            hasCloseKnowledge = true;
            Destination.index = 0;
            Destination.names.Clear();

            timer = 0;
            winUI.SetActive(false);
            failUI.SetActive(false);
            pauseUI.SetActive(false);
            pauseButton.SetActive(false);
            character.gameObject.SetActive(false);
            Cursor.visible = true;
            Debug.Log("max:" + maxLevel);
            for (int j = 0; j <= maxLevel; j++)
                SelectButtons[j].GetComponent<Button>().interactable = true;
            for (int j = 0; j <= maxLevel; j++)
                KnowledgeButton[j].GetComponent<Button>().interactable = true;
            Invoke("BackToInit", 20);
            Invoke("SetPassUI", 17);
        }
        if (levelSelect <= 12)
        {
            hasCloseKnowledge = true;
            levelSelect++;
            Debug.Log("��ǰ��" + levelSelect + "  max:" + maxLevel);
            if (levelSelect > maxLevel)
                maxLevel = levelSelect;
        }
       

    }
    public void NextKnowledge(GameObject nextUI)
    {
        Knowledge[levelSelect].SetActive(false);
        nextUI.SetActive(true);

    }

    public void SetPassUI()
    {
        passUI.SetActive(true);
        passUI.GetComponent<Animator>().SetTrigger("pass");
    }
}
