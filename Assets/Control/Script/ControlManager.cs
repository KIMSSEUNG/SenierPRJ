using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    //게임변환불값
    public bool isFirstGame = false;
    public bool isSecondGame = false;
    public bool isThirdGame = false;
    public bool isEndGame = false;

    //게임이 처음시작인지 아닌지 구분문(0처음 , 1처음아님)
    public int isGameFirstStart;

    //싱글톤
    public static ControlManager controlManager;

    //게임오브젝트
    public GameObject firstGame;
    public GameObject secondGame;
    public GameObject thirdGame;
    
    //각각의 컨트롤러 스크립트
    RunManager runManager;
    GameManager gameManager;

    Timer timer;


    //컨트롤 
    public GameObject mainView;
    //승리 , 실패 보상금액
    public int winPoint;
    public int failPoint;

    //금액 저장 int
    public int storagePoint;
    //금액 view Text
    public Text viewPoint;
    //일시중지 UI ON/OFF
    public GameObject pouseUI;

    //스토어 체크박스 
    public StoreCheckBox storeCheckBox;

    //저장확인 gameobject
    public GameObject saveConfirm;


    //게임에서 메인으로 돌아올때 다음게임으로 넘어가는것을 막는 불값
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
        
        
        //장착상태부 설정
        storeCheckBox.Setting();
        
        
        if(!(PlayerPrefs.HasKey("firstGameState"))){
            //처음게임 이후라는걸 알기 위한 int값
            isGameFirstStart=1;
            //초기 금액 초기화
            storagePoint=100000;
            
        }
        else{
            //금액 불러오기
            storagePoint= PlayerPrefs.GetInt("storagePoint");
            //재시작시 마운트 되어있는것들 장착
            
        }
        viewPoint.text = storagePoint+"원";


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
            Debug.Log("인식");
            pouseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // 첫화면 버튼클릭
    public void GameStart()
    {
        isFirstGame = true;
    }

    public void StoreStart()
    {
        StoreManager.storeManager.storeUI.SetActive(true);
    }

    //일시중단 게임버튼
    public void Restart()
    {
        pouseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save()
    {
        //장착상태 저장
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
