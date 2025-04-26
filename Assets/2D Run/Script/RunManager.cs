using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunManager : MonoBehaviour
{
    //��ֹ� ������
    public GameObject obstaclePrefab;
    ObstaclePrefab obstacleprefabScript;
    public GameObject obstaclePrefab_1;
    ObstaclePrefab obstacleprefabScript_1;
    public GameObject obstaclePrefab_2;
    ObstaclePrefab obstacleprefabScript_2;

    //�������
    public Player player;
    public int returnHeart;
    //�÷��̾� ó����ġ�� ����
    Transform playerTransform;

    float respon_Y;
    // �����ð� üũ ����
    float time=0;
    // �������� �ð� ����
    float gameRunningTime_Sec = 0;
    int gameRunningTime_minute = 0;

    // �����ð� , �������� ����
    public float responTime;
    //Start Trigger
    public bool isStart = false;
    //��ŸƮ ��ư
    public GameObject startButton;
    //��������� ������ ���� ��Ʈ��
    int randomAirPlane;
    //����� ����
    AudioSource audio;
    //0�� ��� ���� , 1�� ���� ���� , 2�� ���� ����
    public AudioClip[] audioPlay;

    //���� UI
    public GameObject EndUI;
    //����ð��� �����ֱ� ���� Text
    public GameObject runningTimeActive;
    Text runningTimeText;
    // ���� ���� ���� �ؽ�Ʈ , ����Ʈ �ؽ�Ʈ
    public Text gameClearText;
    public Text gamePointText;

    //����⸦ ������� ����
    GameObject AirPlane;
    public GameObject AirPlaneParent;

    // Ŭ���� �ð�(�д���)
    public int ClearTime;

    //��������¿�������
    public GameObject thirdGame;
    //LevelUI ON/OFF
    public GameObject levelUI;

    //����̹��� ü������ ���� ��� mesh
    public MeshRenderer baclGroundMesh;
    
    //0�� ��ħ 1�� ����
    [SerializeField]
    Material[] mesh;
    //��ֹ� ��ȭ�� ���� �Ұ�
    bool isNight=false;

    private void Start()
    {
        runningTimeText=runningTimeActive.GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        playerTransform = player.GetComponent<Transform>();

        //�� ������Ʈ �Ӽ��� ������ ���� ��ũ��Ʈ
        obstacleprefabScript = obstaclePrefab.GetComponent<ObstaclePrefab>();
        obstacleprefabScript_1 = obstaclePrefab_1.GetComponent<ObstaclePrefab>();
        obstacleprefabScript_2 = obstaclePrefab_2.GetComponent<ObstaclePrefab>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            //�ð�ǥ�� ����

            //����ð�
            gameRunningTime_Sec += Time.deltaTime;
            if (gameRunningTime_Sec >= 60)
            {
                gameRunningTime_minute += 1;
                gameRunningTime_Sec = 0;
                
                //1�и��� ����� �ӵ� ��
                obstacleprefabScript.speed +=0.2f;
                obstacleprefabScript_1.speed +=0.2f;
                obstacleprefabScript_2.speed +=0.2f;
                //�� Ȯ��
                Debug.Log(obstacleprefabScript.speed);
                Debug.Log(obstacleprefabScript_1.speed);
                Debug.Log(obstacleprefabScript_2.speed);

            }

            runningTimeText.text = gameRunningTime_minute + " : " + gameRunningTime_Sec.ToString("F0");

            //������Ÿ�� �ð�
            time += Time.deltaTime;

            //������ �ð�
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




            //���� ����
            if (gameRunningTime_minute >= ClearTime)
            {
                //���� Ŭ����
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
            //20��
            case 1:
                ClearTime = 2;
                player.noDamageTime=3;
                ControlManager.controlManager.winPoint = 300;
                break;
            //30��
            case 2:
                ClearTime = 3;
                player.noDamageTime = 2;
                ControlManager.controlManager.winPoint = 500;
                break;
            //40��
            case 3:
                ClearTime = 4;
                player.noDamageTime = 2;
                ControlManager.controlManager.winPoint = 1000;
                break;
        }
        //LevelUI ��Ȱ��ȭ
        levelUI.SetActive(false);
        //��ư ������ start ��ư Ȱ��ȭ
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
            gameClearText.text = "����";
            gamePointText.text = "Point = " + ControlManager.controlManager.winPoint + " ����";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.winPoint;
        }
        else
        {
            audio.clip = audioPlay[2];
            audio.Play();
            gameClearText.text = "����";
            gamePointText.text = "Point = 200 ����";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.failPoint;
        }
        EndUI.SetActive(true);
        //�ؽ�Ʈ�� �ٲ���� ǥ��
        ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint + "��";
        //�ʱ�ȭ
        Invoke("NextGame", 2f);
    }

    public void NextGame()
    {

        //�ʱ�ȭ����
        //���� UI Ȱ��ȭ
        levelUI.SetActive(true);
        //�ð� UI ���� �ʱ�ȭ
        runningTimeText.text = "0 : 0";
        gameRunningTime_Sec = 0;
        gameRunningTime_minute = 0;
        time = 0;
        //����� �ʱ�ȭ
        audio.loop = true;
        audio.clip = audioPlay[0];
        //UI�ʱ�ȭ
        EndUI.SetActive(false);
        //����ȭ�� �ʱ�ȭ
        runningTimeActive.SetActive(true);
        startButton.SetActive(true);
        player.heart = returnHeart;
        //��ġ����
        playerTransform.position = new Vector2(-4.5f,0);    
        //�����̹��� �������
        for(int i=0;i< returnHeart; i++)
        {
            player.heartImage[i].color = Color.white;
        }
        //���ȭ�� �ʱ�ȭ
        baclGroundMesh.material = mesh[0];
        //���� �˷��ִ� �Ұ� �ʱ�ȭ
        isNight =false;
        //����� �ӵ� ����ġ
        obstacleprefabScript.speed =2.7f;
        obstacleprefabScript_1.speed =3.2f;
        obstacleprefabScript_2.speed =3.7f;

        //���� ��������
        if(ControlManager.controlManager.thirdGame.activeSelf)
        {
            ControlManager.controlManager.isEndGame = true;
        }
        


    }
}
