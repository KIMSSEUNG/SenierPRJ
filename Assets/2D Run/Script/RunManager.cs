using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunManager : MonoBehaviour
{
    //장애물 프리팹
    public GameObject obstaclePrefab;
    ObstaclePrefab obstacleprefabScript;
    public GameObject obstaclePrefab_1;
    ObstaclePrefab obstacleprefabScript_1;
    public GameObject obstaclePrefab_2;
    ObstaclePrefab obstacleprefabScript_2;

    //목숨변수
    public Player player;
    public int returnHeart;
    //플레이어 처음위치로 변경
    Transform playerTransform;

    float respon_Y;
    // 생성시간 체크 변수
    float time=0;
    // 게임진행 시간 변수
    float gameRunningTime_Sec = 0;
    int gameRunningTime_minute = 0;

    // 생성시간 , 리스폰수 갯수
    public float responTime;
    //Start Trigger
    public bool isStart = false;
    //스타트 버튼
    public GameObject startButton;
    //랜덤비행기 선택을 위한 인트값
    int randomAirPlane;
    //오디오 변수
    AudioSource audio;
    //0번 배경 음악 , 1번 성공 음악 , 2번 실패 음악
    public AudioClip[] audioPlay;

    //엔드 UI
    public GameObject EndUI;
    //진행시간을 보여주기 위한 Text
    public GameObject runningTimeActive;
    Text runningTimeText;
    // 게임 성공 실패 텍스트 , 포인트 텍스트
    public Text gameClearText;
    public Text gamePointText;

    //비행기를 담기위한 변수
    GameObject AirPlane;
    public GameObject AirPlaneParent;

    // 클리어 시간(분단위)
    public int ClearTime;

    //다음진행온오프게임
    public GameObject thirdGame;
    //LevelUI ON/OFF
    public GameObject levelUI;

    //배경이미지 체인지를 위한 배경 mesh
    public MeshRenderer baclGroundMesh;
    
    //0번 아침 1번 저녁
    [SerializeField]
    Material[] mesh;
    //장애물 강화를 위한 불값
    bool isNight=false;

    private void Start()
    {
        runningTimeText=runningTimeActive.GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        playerTransform = player.GetComponent<Transform>();

        //각 오브젝트 속성을 조정을 위한 스크립트
        obstacleprefabScript = obstaclePrefab.GetComponent<ObstaclePrefab>();
        obstacleprefabScript_1 = obstaclePrefab_1.GetComponent<ObstaclePrefab>();
        obstacleprefabScript_2 = obstaclePrefab_2.GetComponent<ObstaclePrefab>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            //시간표시 구문

            //현재시간
            gameRunningTime_Sec += Time.deltaTime;
            if (gameRunningTime_Sec >= 60)
            {
                gameRunningTime_minute += 1;
                gameRunningTime_Sec = 0;
                
                //1분마다 비행기 속도 업
                obstacleprefabScript.speed +=0.2f;
                obstacleprefabScript_1.speed +=0.2f;
                obstacleprefabScript_2.speed +=0.2f;
                //값 확인
                Debug.Log(obstacleprefabScript.speed);
                Debug.Log(obstacleprefabScript_1.speed);
                Debug.Log(obstacleprefabScript_2.speed);

            }

            runningTimeText.text = gameRunningTime_minute + " : " + gameRunningTime_Sec.ToString("F0");

            //리스폰타임 시간
            time += Time.deltaTime;

            //리스폰 시간
            if (time >= responTime)
            {
                ObstacleCreate();
                time = 0;

            }

            if (gameRunningTime_minute >= 1)
            {
                isNight = true;
            }
            //if (gameRunningTime_Sec >= 10)
            //{
            //    baclGroundMesh.material = mesh[1];
            //    isNight = true;
            //}




            //게임 성공
            if (gameRunningTime_minute >= ClearTime)
            {
                //게임 클리어
                GameEnd(true);
            }
        }



            
    }

    public void GameStart()
    {
        audio.mute = true;
        isStart = true;
        startButton.SetActive(false);
        player.heart = returnHeart;
        

    }

    public void LevelSetting(int _level)
    {
        switch (_level)
        {
            //20개
            case 1:
                ClearTime = 2;
                player.noDamageTime=3;
                ControlManager.controlManager.winPoint = 300;
                break;
            //30개
            case 2:
                ClearTime = 3;
                player.noDamageTime = 2;
                ControlManager.controlManager.winPoint = 500;
                break;
            //40개
            case 3:
                ClearTime = 4;
                player.noDamageTime = 2;
                ControlManager.controlManager.winPoint = 1000;
                break;
        }
        //LevelUI 비활성화
        levelUI.SetActive(false);
        //버튼 누른후 start 버튼 활성화
        startButton.SetActive(true);

    }

    void ObstacleCreate()
    {
        if(!isNight){
            randomAirPlane = Random.Range(1, 3);
        }
        else{
            randomAirPlane = Random.Range(2, 4);
        }
        
        switch (randomAirPlane)
        {
            case 1:
                AirPlane = Instantiate(obstaclePrefab, new Vector2(6.65f, Random.Range(-2, 3)), obstaclePrefab.transform.rotation) as GameObject;
                AirPlane.transform.parent = AirPlaneParent.transform;
                break;
            case 2:
                AirPlane = Instantiate(obstaclePrefab_1, new Vector2(6.65f, Random.Range(-2, 3)), obstaclePrefab.transform.rotation);
                AirPlane.transform.parent = AirPlaneParent.transform;
                break;
            case 3:
                AirPlane = Instantiate(obstaclePrefab_2, new Vector2(6.65f, Random.Range(-2, 3)), obstaclePrefab.transform.rotation);
                AirPlane.transform.parent = AirPlaneParent.transform;
                break;
        }
      

    }

    

    public void GameEnd(bool _gameClear)
    {
        runningTimeActive.SetActive(false);
        isStart = false;
        audio.mute = false;
        audio.loop = false;
        if (_gameClear)
        {
            audio.clip = audioPlay[1];
            audio.Play();
            gameClearText.text = "성공";
            gamePointText.text = "Point = " + ControlManager.controlManager.winPoint + " 정립";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.winPoint;
        }
        else
        {
            audio.clip = audioPlay[2];
            audio.Play();
            gameClearText.text = "실패";
            gamePointText.text = "Point = 200 정립";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.failPoint;
        }
        EndUI.SetActive(true);
        //텍스트에 바뀐숫자 표시
        ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint + "원";
        //초기화
        Invoke("NextGame", 2f);
    }

    public void NextGame()
    {

        //초기화변수
        //레벨 UI 활성화
        levelUI.SetActive(true);
        //시간 UI 변수 초기화
        runningTimeText.text = "0 : 0";
        gameRunningTime_Sec = 0;
        gameRunningTime_minute = 0;
        time = 0;
        //오디오 초기화
        audio.loop = true;
        audio.clip = audioPlay[0];
        //UI초기화
        EndUI.SetActive(false);
        //게임화면 초기화
        runningTimeActive.SetActive(true);
        startButton.SetActive(true);
        player.heart = returnHeart;
        //위치변경
        playerTransform.position = new Vector2(-4.5f,0);    
        //심장이미지 원래대로
        for(int i=0;i< returnHeart; i++)
        {
            player.heartImage[i].color = Color.white;
        }
        //배경화면 초기화
        baclGroundMesh.material = mesh[0];
        //밤을 알려주는 불값 초기화
        isNight =false;
        //비행기 속도 원위치
        obstacleprefabScript.speed =2.7f;
        obstacleprefabScript_1.speed =3.2f;
        obstacleprefabScript_2.speed =3.7f;

        //게임 종료진행
        if(ControlManager.controlManager.thirdGame.activeSelf)
        {
            ControlManager.controlManager.isEndGame = true;
        }
        


    }
}
