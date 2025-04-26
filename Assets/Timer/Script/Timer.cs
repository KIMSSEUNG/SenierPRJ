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

    //�������ý� UI ����
    public GameObject levelUI;

    public Animator TimerAnim;

    //����� UI
    public GameObject gameEndUI;
    public Text gameTopUI;
    public Text pointUI;

    public Text pressedTime;



    //������Ҹ�
    AudioSource audio;
    //�˶��Ҹ� 1 �帣�� �Ҹ� 2 ���мҸ� 3 ��������Ҹ�
    public AudioClip[] alarm;

    // Start is called before the first frame update
    private void Start()
    {
        //������Ʈ �ʱ�ȭ
        audio = GetComponent<AudioSource>();

    }

    //�ܰ� ����
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
        Debug.Log("�ð����� "+ (timeLine + timeRange) +" ~ "+ (timeLine-timeRange));
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

        Debug.Log("�ð�����");
        Debug.Log(curTime);
        TimerAnim.SetBool("isClockOn", false);
        isTimeStart = true;
        TimeTranslate();
        audio.Play();
        //��ư �¿���
        startButton.SetActive(false);
        endButton.SetActive(true);

    }

    public void GetTimeEnd()
    {
        isTimeStart = false;
        pressedTime.text = "�����ð� : "+ curTime.ToString("F1");
        endButton.SetActive(false);
        
        Debug.Log("����");

        if (curTime <= FistCheck && curTime >= LastCheck)
        {
            audio.clip = alarm[2];
            audio.loop = false;
            audio.Play();
            gameTopUI.text = "���� ����";
            pointUI.text = "Point = "+ControlManager.controlManager.winPoint+" ����";
            //����Ʈ ����
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.winPoint;
            Debug.Log("Ŭ����");

        }
        else
        {
            audio.clip = alarm[1];
            audio.Play();
            gameTopUI.text = "���� ����";
            pointUI.text = "Point = 200 ����";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.failPoint;
            Debug.Log("����");
            TimerAnim.SetBool("isClockOn",true);

        }
        curTime = 0f;
        ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint + "��";
        gameEndUI.SetActive(true);

        
        Invoke("NextGame",2);


    }

    void TimeTranslate()
    {
        Debug.Log("����");
        FistCheck = timeLine + timeRange; 
        LastCheck = timeLine - timeRange; 

    }

    public void NextGame()
    {
        audio.clip = alarm[0];
        Debug.Log("���");
        //�ʱ�ȭ
        ResetGame();
        //�ι�° ���� ��ȯ
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
