using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public float curTime=0;
    public float FistCheck;
    public float LastCheck;
    bool isTimeStart=false;
    int timeLine;
    int timeRange;
    public GameObject startButton;
    public GameObject endButton;

    //레벨선택시 UI 꺼짐
    public GameObject levelUI;

    public Animator TimerAnim;

    //종료시 UI
    public GameObject gameEndUI;
    public Text gameTopUI;
    public Text pointUI;

    public Text pressedTime;



    //오디오소리
    AudioSource audio;
    //알람소리 1 흐르는 소리 2 실패소리 3 성공보상소리
    public AudioClip[] alarm;

    // Start is called before the first frame update
    private void Start()
    {
        //컴포넌트 초기화
        audio = GetComponent<AudioSource>();

    }

    //단계 셋팅
    public void LevelSetting(int _level)
    {
        switch (_level)
        {
            case 1:
                timeLine = 60;
                timeRange = 5;
                ControlManager.controlManager.winPoint = 300;
                break;
            case 2:
                timeLine = 90;
                timeRange = 3;
                ControlManager.controlManager.winPoint = 500;
                break;
            case 3:
                timeLine = 120;
                timeRange = 2;
                ControlManager.controlManager.winPoint = 1000;
                break;
        }
        Debug.Log("시간범위 "+ (timeLine + timeRange) +" ~ "+ (timeLine-timeRange));
        levelUI.SetActive(false);
        startButton.SetActive(true);
       

    }

    // Update is called once per frame
    void Update()
    {

        if (isTimeStart)
        {
            curTime += Time.deltaTime;
            Debug.Log(curTime);


        }

    }

    public void GetTimeStart()
    {

        Debug.Log("시간시작");
        Debug.Log(curTime);
        TimerAnim.SetBool("isClockOn", false);
        isTimeStart = true;
        TimeTranslate();
        audio.Play();
        //버튼 온오프
        startButton.SetActive(false);
        endButton.SetActive(true);

    }

    public void GetTimeEnd()
    {
        isTimeStart = false;
        pressedTime.text = "눌린시간 : "+ curTime.ToString("F1");
        endButton.SetActive(false);
        
        Debug.Log("종료");

        if (curTime <= FistCheck && curTime >= LastCheck)
        {
            audio.clip = alarm[2];
            audio.loop = false;
            audio.Play();
            gameTopUI.text = "게임 성공";
            pointUI.text = "Point = "+ControlManager.controlManager.winPoint+" 정립";
            //포인트 적립
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.winPoint;
            Debug.Log("클리어");

        }
        else
        {
            audio.clip = alarm[1];
            audio.Play();
            gameTopUI.text = "게임 실패";
            pointUI.text = "Point = 200 정립";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.failPoint;
            Debug.Log("실패");
            TimerAnim.SetBool("isClockOn",true);

        }
        curTime = 0f;
        ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint + "원";
        gameEndUI.SetActive(true);

        
        Invoke("NextGame",2);


    }

    void TimeTranslate()
    {
        Debug.Log("측정");
        FistCheck = timeLine + timeRange; 
        LastCheck = timeLine - timeRange; 

    }

    public void NextGame()
    {
        audio.clip = alarm[0];
        Debug.Log("대기");
        //초기화
        ResetGame();
        //두번째 게임 전환
        if(ControlManager.controlManager.firstGame.activeSelf){
            ControlManager.controlManager.isSecondGame = true;
        }
        
    }

    void ResetGame()
    {
        timeLine = 0;
        timeRange = 0;
        curTime=0;
        isTimeStart=false;
        audio.mute = false;
        gameEndUI.SetActive(false);
        TimerAnim.SetBool("isClockOn", false);
        audio.loop = true;
        startButton.SetActive(false);
        endButton.SetActive(false);
        levelUI.SetActive(true);
        
    }
}
