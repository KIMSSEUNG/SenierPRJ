using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    //���Ӻ�ȯ�Ұ�
    public bool isFirstGame = false;
    public bool isSecondGame = false;
    public bool isThirdGame = false;
    public bool isEndGame = false;

    //������ ó���������� �ƴ��� ���й�(0ó�� , 1ó���ƴ�)
    public int isGameFirstStart;

    //�̱���
    public static ControlManager controlManager;

    //���ӿ�����Ʈ
    public GameObject firstGame;
    public GameObject secondGame;
    public GameObject thirdGame;
    
    //������ ��Ʈ�ѷ� ��ũ��Ʈ
    RunManager runManager;
    GameManager gameManager;

    Timer timer;


    //��Ʈ�� 
    public GameObject mainView;
    //�¸� , ���� ����ݾ�
    public int winPoint;
    public int failPoint;

    //�ݾ� ���� int
    public int storagePoint;
    //�ݾ� view Text
    public Text viewPoint;
    //�Ͻ����� UI ON/OFF
    public GameObject pouseUI;

    //����� üũ�ڽ� 
    public StoreCheckBox storeCheckBox;

    //����Ȯ�� gameobject
    public GameObject saveConfirm;


    //���ӿ��� �������� ���ƿö� ������������ �Ѿ�°��� ���� �Ұ�
    public bool isMain;

    private void Awake()
    {
        controlManager = this;

        runManager = thirdGame.GetComponentInChildren<RunManager>();

        gameManager = secondGame.GetComponentInChildren<GameManager>();

        timer = firstGame.GetComponentInChildren<Timer>();
    }

    private void Start()
    {
        
        
        //�������º� ����
        storeCheckBox.Setting();
        
        
        if(!(PlayerPrefs.HasKey("firstGameState"))){
            //ó������ ���Ķ�°� �˱� ���� int��
            isGameFirstStart=1;
            //�ʱ� �ݾ� �ʱ�ȭ
            storagePoint=100000;
            
        }
        else{
            //�ݾ� �ҷ�����
            storagePoint= PlayerPrefs.GetInt("storagePoint");
            //����۽� ����Ʈ �Ǿ��ִ°͵� ����
            
        }
        viewPoint.text = storagePoint+"��";


    }
    // Update is called once per frame
    void Update()
    {
        if (isFirstGame)
        {
            firstGame.SetActive(true);
            mainView.SetActive(false);
            isFirstGame = false;
        }
        else if (isSecondGame)
        {
            firstGame.SetActive(false);
            secondGame.SetActive(true);
            isSecondGame = false;


        }
        else if(isThirdGame)
        {
            secondGame.SetActive(false);
            thirdGame.SetActive(true);
            isThirdGame = false;
        }
        else if (isEndGame)
        {
            thirdGame.SetActive(false);
            mainView.SetActive(true);
            isEndGame = false;
            
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("�ν�");
            pouseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // ùȭ�� ��ưŬ��
    public void GameStart()
    {
        isFirstGame = true;
    }

    public void StoreStart()
    {
        StoreManager.storeManager.storeUI.SetActive(true);
    }

    //�Ͻ��ߴ� ���ӹ�ư
    public void Restart()
    {
        pouseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save()
    {
        //�������� ����
        for(int i =0 ; i<=2;i++){
            PlayerPrefs.SetInt("timerState"+i, (int)storeCheckBox.timerState[i]);
            PlayerPrefs.SetInt("cardState"+i, (int)storeCheckBox.cardState[i]);
            PlayerPrefs.SetInt("run2DState"+i, (int)storeCheckBox.run2DState[i]);
            Debug.Log("timerState"+i);
        }
        PlayerPrefs.SetInt("storagePoint",storagePoint);
        PlayerPrefs.SetInt("firstGameState",isGameFirstStart);
        PlayerPrefs.Save();
        saveConfirm.SetActive(true);
        
    }

    public void SaveConfirm(){
        saveConfirm.SetActive(false);
    }

    public void Main(){
        isMain=true;
        
        if (firstGame.activeSelf)
        {
            firstGame.SetActive(false);
            timer.NextGame();
        }
        else if (secondGame.activeSelf)
        {
            secondGame.SetActive(false);
            gameManager.ResetGame();
        }
        else if(thirdGame.activeSelf)
        {
            thirdGame.SetActive(false);
            runManager.NextGame();
        }
        isMain = false;
        Time.timeScale = 1;
        mainView.SetActive(true);
        pouseUI.SetActive(false);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    public void End()
    {
        Application.Quit();
    }

    
}
