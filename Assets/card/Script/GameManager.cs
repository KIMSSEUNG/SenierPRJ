using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    //카메라 조정
    public Camera ui_Camera;

    // 프리팹
    public GameObject card;
    public GameObject cardParent;
    public int Count1;
    public int Count2;
    public int Count3;
    public int Count4;
    public int Count5;
    SpriteRenderer sRend;
    public Sprite[] Case = new Sprite[6];

    //카드 총 갯수
    public int count_Card;

    // 카드 위치
    Vector2 cardPosition;
    public int raw;
    public int colunm;

    // 싱글톤 사용
    public static GameManager gameManager;

    // 랜덤값 사용 (현재 10)
    public int maxCardNumber;
    List<int> cardIndex;
    //카드 종류별 갯수
    int CardCount;

    //Reverse 변수
    public int reverseTime;
    bool isReverse = true;

    
    SpriteRenderer[] cardSprite;
    public Transform cardCollection;

    //비교를 할 변수
    public SpriteRenderer check1;
    public SpriteRenderer check2;
    public bool isFirst =false;
    public bool isWait = false;
    
    int winCount=0;

    //클리어 게임매니져
    public GameObject clearBar;
    //화면 표시 Text
    Text TopText;
    Text middleText;

    //button 삭제
    public GameObject startButton;

    //남은 횟수표시
    int count;
    public Text remainingCount;

    //게임 성공 실패 구분문
    bool isGameClear;

    //클리어시 반복멈추기 위한 구문
    bool isRelay=false;

    //오디오 클립 및 소스
    AudioSource audio;
    //0번 성공 1번 실패
    public AudioClip[] audioEffect;

    //start 버튼 ON/OFF를 위한 변수

    //Level UI 
    public GameObject levelUI;

    private void Awake()
    {
        //싱글톤 패턴 생성
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameManager);
        }

        
    }

    void Start()
    {

        audio = GetComponent<AudioSource>();
        Debug.Log("실행");
        TopText = clearBar.GetComponentsInChildren<Text>()[0];
        middleText = clearBar.GetComponentsInChildren<Text>()[1];
        Debug.Log(Case[2].name);

    }

    float raw1=0;
    float colunm1=0;
    float size = 0.7f;

    public void LevelSetting(int _level)
    {
        switch (_level)
        {
            //20개
            case 1:
                raw = 4;
                colunm = 5;
                raw1 = 4* size;
                colunm1 = 5* size;
                count = 17;
                reverseTime = 5;
                ControlManager.controlManager.winPoint = 300;
                break;
            //30개
            case 2:
                raw = 6 ;
                colunm =5 ;
                raw1 = 6 * size;
                colunm1 = 5 * size;
                count = 27;
                reverseTime = 7;
                ControlManager.controlManager.winPoint = 500;
                break;
            //40개
            case 3:
                raw = 8 ;
                colunm = 5 ;
                raw1 = 8 * size;
                colunm1 = 5 * size;
                count = 37;
                reverseTime = 10;
                ControlManager.controlManager.winPoint = 1000;
                break;
        }
        //LevelUI 비활성화
        levelUI.SetActive(false);
        //버튼 누른후 start 버튼 활성화
        startButton.SetActive(true);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (isRelay)
        {
            if (winCount == raw * colunm / 2)
            {
                GameOver(true);
            }
            else if (count == 0)
            {
                GameOver(false);

            }
        }
        
        
    }

    void GameOver(bool _isGameClear)
    {
        Debug.Log("cardover실행");
        ui_Camera.depth = 1;
        isRelay = false;
        if (_isGameClear)
        {
            audio.clip = audioEffect[0];
            audio.Play();
            TopText.text = "성공";
            middleText.text = "Point = " + ControlManager.controlManager.winPoint + " 정립";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.winPoint;
        }
        else
        {
            audio.clip = audioEffect[1];
            audio.Play();
            TopText.text = "실패";
            middleText.text = "Point = 200 정립";
            ControlManager.controlManager.storagePoint += ControlManager.controlManager.failPoint;
        }
        clearBar.SetActive(true);
        ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint + "원";

        Invoke("NextGame", 3f);

        
        
        
    }

    public void NextGame()
    {
        //변수 초기화
        ResetGame();

        //게임 변환
        if(ControlManager.controlManager.secondGame.activeSelf)
        {
            ControlManager.controlManager.isThirdGame = true;
        }
        
    }

    public void ResetGame()
    {
        foreach (Transform card in cardParent.GetComponentsInChildren<Transform>())
        {
            if (!(card.name == "CardParent"))
            {
                Destroy(card.gameObject);
            }

        }
        ui_Camera.depth = 0;
        //게임종료 클리어바 비활성화
        clearBar.SetActive(false);
        //해당카운트 0으로 초기화
        winCount = 0;
        Count1 = 0;
        Count2 = 0;
        Count3 = 0;
        Count4 = 0;
        Count5 = 0;
        //레벨 UI 활성화
        levelUI.SetActive(true);
        check1 = null;
        check2 = null;
        isFirst = false;
        isWait = false;
    }


    void WaitReverse()
    {
        CardReverse();
    }


    void CardReverse()
    {
        cardSprite = cardCollection.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < cardSprite.Length; i++)
        {
            cardSprite[i].sprite = Case[0];
        }

        isReverse = true;
    }


    public void MapBuild()
    {
        startButton.SetActive(false);

        //랜덤값을 위한 초기화

        cardIndex = new List<int>();
        CardCount = raw * colunm / maxCardNumber;
        remainingCount.text = "남은 횟수 : " + count;


        for (int i = 1; i <= maxCardNumber; i++)
        {
            cardIndex.Add(i);
        }
        Debug.Log(cardIndex.Count);
        if (!(raw * colunm % maxCardNumber ==0)&& raw * colunm / maxCardNumber>1)
        {
            Debug.Log("카드생성불가");
            return;
        }
        else
        {
            for (float i = -0.7f; i <= raw1-1; i+= size)
            {
                for (float j = 1.7f; j <= colunm1+1; j+= size)
                {
                    cardPosition = new Vector2(i, j);

                    GameObject Card = Instantiate(card, cardPosition, card.transform.rotation) as GameObject;
                    Card.transform.parent = cardParent.transform;
                    sRend = Card.GetComponent<SpriteRenderer>();
                    int rndnum = cardIndex[Random.Range(0, cardIndex.Count)];


                    switch (rndnum)
                    {
                        case 1:
                            Card.tag = "Case1";
                            sRend.sprite = Case[1];
                            Count1++;
                            if (Count1 == CardCount)
                            {
                                cardIndex.Remove(1);
                            }
                            break;
                        case 2:
                            Card.tag = "Case2";
                            sRend.sprite = Case[2];
                            Count2++;
                            if (Count2 == CardCount)
                            {
                                cardIndex.Remove(2);
                            }
                            break;
                        case 3:
                            Card.tag = "Case3";
                            sRend.sprite = Case[3];
                            Count3++;
                            if (Count3 == CardCount)
                            {
                                cardIndex.Remove(3);
                            }
                            break;
                        case 4:
                            Card.tag = "Case4";
                            sRend.sprite = Case[4];
                            Count4++;
                            if (Count4 == CardCount)
                            {
                                cardIndex.Remove(4);
                            }
                            break;
                        case 5:
                            Card.tag = "Case5";
                            sRend.sprite = Case[5];
                            Count5++;
                            if (Count5 == CardCount)
                            {
                                cardIndex.Remove(5);
                            }
                            break;
                    }

                }
            }
        }
        isRelay = true;
        Invoke("WaitReverse", reverseTime);


    }
    // 레이 받았을때 스타트
   
       
    public IEnumerator Getray(Collider2D collider)
    {
        sRend = collider.GetComponent<SpriteRenderer>();

        if (sRend.sprite.name == "카드뒷면")
        {


            switch (collider.tag)
            {
                case "Case1":
                    sRend.sprite = Case[1];
                    break;

                case "Case2":
                    sRend.sprite = Case[2];
                    break;

                case "Case3":
                    sRend.sprite = Case[3];
                    break;

                case "Case4":
                    sRend.sprite = Case[4];
                    break;

                case "Case5":
                    sRend.sprite = Case[5];
                    break;

            }
            if (!isFirst)
            {
                check1 = sRend;
                isFirst = true;

            }
            else
            {

                isWait = true;
                yield return new WaitForSeconds(0.2f);
                check2 = sRend;
                if (check1.sprite.name == check2.sprite.name)
                {
                    winCount++;

                }
                else
                {

                    check1.sprite = Case[0];
                    check2.sprite = Case[0];
                }
                check1 = null;
                check2 = null;
                isFirst = false;
                isWait = false;
                // 카운트 감소 체크 
                count--;
                remainingCount.text = "남은 횟수 : " + count;
                
            }

        }
    }
    

}
