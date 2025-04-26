using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{

    //상점 첫 이미지 On/Off 위한 gameobject 선언
    public GameObject mainStoreImage;
    
    //상점체크박스
    StoreCheckBox storeCheckBox;

    //상점스토어 싱글톤
    public static StoreManager storeManager;

    //스토어 UI
    public GameObject storeUI;

    public GameManager gameManager_1;

    //상품 부모 , 담을 자식
    public GameObject productCaseParent;
    Image[] productImageCase;
    Text[] productText;
    public Button[] productButton;
    
    //각 게임별 상품 이미지
    public Sprite[] run2D;
    public Sprite[] timer;
    public Sprite[] card;

    //각 게임별 종류 저장배열
    //2D RUN
    public RuntimeAnimatorController[] run2Danim;
    public Player player;

    //Card(뒷면 1,2,3,4,5)
    public Sprite[] joCard;
    public Sprite[] kimCard;
    public Sprite[] nomalCard;

    //Timer 미정

    //구매 종류 카테고리 ,숫자를 담을 string , int값
    string gameCategory=null;
    int gameNumber = 0;

    //구매 확인 UI
    public GameObject purchaseUI;

    //상품가격을 담을 int값
    int productPrice=0;

    //구매 성공 , 실패 안내문
    public Text purchaseRespond;

    //아이템 셋팅갯수(0부터 시작)
    int maxItemNumber = 2;


    private void Awake()
    {
        if (storeManager == null)
        {
            storeManager = this;
        }
        else
        {
            Destroy(storeManager);
        }
        
    }
    void Start()
    {
        //스크립트 할당
        storeCheckBox = GetComponent<StoreCheckBox>();

        // 이미지 배열 넣기
        productImageCase = productCaseParent.GetComponentsInChildren<Image>();
        productText= productCaseParent.GetComponentsInChildren<Text>();

        //테스트
        if(PlayerPrefs.HasKey("firstGameState")){
            SettingMount();
            
        }
        

    }

    // Update is called once per frame
    public void SettingButtonImage(string _name)
    {
        if (!(productCaseParent.activeSelf))
        {
            productCaseParent.SetActive(true);
        }

        if(mainStoreImage.activeSelf)
        {
            mainStoreImage.SetActive(false);
        }
        gameCategory = _name;
        switch (_name)
        {
            
            case "Timer":
                for(int i = 0; i <= maxItemNumber; i++)
                {
                    
                    productImageCase[i].sprite = timer[i];
                    switch (storeCheckBox.timerState[i])
                    {
                        case StoreCheckBox.purchaseState.n_Buy:
                             if (i == 1)
                            {
                                productText[i].text = "5000원";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000원";
                            }
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "장착중";
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                    }
                }
                break;
            case "Card":
                for (int i = 0; i <= maxItemNumber; i++)
                {

                    productImageCase[i].sprite = card[i];
                    switch (storeCheckBox.cardState[i])
                    {
                        case StoreCheckBox.purchaseState.n_Buy:
                             if (i == 1)
                            {
                                productText[i].text = "5000원";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000원";
                            }
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "장착중";
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                    }
                    
                }
                break;
            case "2DRun":
                for (int i = 0; i <= maxItemNumber; i++)
                {
                    
                    productImageCase[i].sprite = run2D[i];
                    switch (storeCheckBox.run2DState[i])
                    {
                        case StoreCheckBox.purchaseState.n_Buy:
                            if (i == 1)
                            {
                                productText[i].text = "5000원";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000원";
                            }
                            productButton[i].gameObject.SetActive(false);
                            productText[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "장착중";
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                    }
                }
                break;
        }
    }


    //상품클릭
    public void ProductChoice(int _number)
    {
        //구매완료상태면 그냥 리턴함
        switch (gameCategory)
        {
            case "Timer":
                if(!(storeCheckBox.timerState[_number] == StoreCheckBox.purchaseState.n_Buy))
                {
                    return;
                }
                break;
            case "Card":
                if (!(storeCheckBox.cardState[_number] == StoreCheckBox.purchaseState.n_Buy))
                {
                    return;
                }
                break;
            case "2DRun":
                if (!(storeCheckBox.run2DState[_number] == StoreCheckBox.purchaseState.n_Buy))
                {
                    return;
                }
                break;

        }
        //아닐시 상품가격 저장후 purchaseUI ON
        switch (_number)
        {
            case 1:
                productPrice = 5000;
                break;
            case 2:
                productPrice = 10000;
                break;
        
        }
        gameNumber = _number;
        purchaseUI.SetActive(true);
        

    }

    //상품구매
    public void PurchaseProduct()
    {
        //구매가 되는지 확인하는 문
        if (ControlManager.controlManager.storagePoint >= productPrice)
        {
            ControlManager.controlManager.storagePoint -= productPrice;
            //장착하기 신호표시
            switch (gameCategory)
            {
                case "Timer":
                    storeCheckBox.timerState[gameNumber] = StoreCheckBox.purchaseState.Buy;
                    productText[gameNumber].gameObject.SetActive(false);
                    productButton[gameNumber].gameObject.SetActive(true);
                    break;
                case "Card":
                    storeCheckBox.cardState[gameNumber] = StoreCheckBox.purchaseState.Buy;
                    productText[gameNumber].gameObject.SetActive(false);
                    productButton[gameNumber].gameObject.SetActive(true);
                    break;
                case "2DRun":
                    storeCheckBox.run2DState[gameNumber] = StoreCheckBox.purchaseState.Buy;
                    productText[gameNumber].gameObject.SetActive(false);
                    productButton[gameNumber].gameObject.SetActive(true);
                    break;
            }
            //화면꺼짐
            purchaseUI.SetActive(false);
            purchaseRespond.text = "구매성공";
            //보여지는 돈 변경
            ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint+"원";


        }
        else //구매안됨
        {
            //초기화 및 구매화면 꺼짐
            purchaseUI.SetActive(false);
            purchaseRespond.text = "구매실패";

        }
        purchaseRespond.color= new Color(purchaseRespond.color.r, purchaseRespond.color.g, purchaseRespond.color.b, 255f);
        Invoke("ClearText", 2f);

    }



    //구매확인 애니메이션
    void ClearText()
    {
        purchaseRespond.color = new Color(purchaseRespond.color.r, purchaseRespond.color.g, purchaseRespond.color.b, 0f);

    }
    


    //상품구매취소
    public void PurchaseCancle()
    {
        purchaseUI.SetActive(false);
    }

    //상품 착용
    public void ChooseButton(int _number)
    {
        switch (gameCategory)
        {
            case "Timer":
                //마운트 되있는 종류 찾아서 구매상태로 교체
                for (int i = 0; i <= maxItemNumber; i++)
                {
                    if(storeCheckBox.timerState[i]== StoreCheckBox.purchaseState.mount)
                    {
                        storeCheckBox.timerState[i] = StoreCheckBox.purchaseState.Buy;
                        productText[i].gameObject.SetActive(false);
                        productButton[i].gameObject.SetActive(true);
                        continue;
                    }
 
                }
                // 구매상태를 마운트 상태로 교체
                storeCheckBox.timerState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "장착중";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // 게임 장착 교체(timer아직 미정)
                break;

            case "Card":
                //마운트 되있는 종류 찾아서 구매상태로 교체
                for (int i = 0; i <= maxItemNumber; i++)
                {
                    if (storeCheckBox.cardState[i] == StoreCheckBox.purchaseState.mount)
                    {
                        storeCheckBox.cardState[i] = StoreCheckBox.purchaseState.Buy;
                        productText[i].gameObject.SetActive(false);
                        productButton[i].gameObject.SetActive(true);
                        continue;
                    }

                }
                // 구매상태를 마운트 상태로 교체
                storeCheckBox.cardState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "장착중";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // 게임 장착 교체

                switch (_number)
                {
                    case 0:
                        for (int i = 0; i <= 5; i++)
                        {
                            gameManager_1.Case[i] = nomalCard[i];
                        }
                        break;
                    case 1:
                        for (int i = 0; i <= 5; i++)
                        {
                            gameManager_1.Case[i] = joCard[i];
                        }
                        break;
                    case 2:
                        for (int i = 0; i <= 5; i++)
                        {
                            gameManager_1.Case[i] = kimCard[i];
                            
                        }
                        break;
                }
                
                
                break;

            case "2DRun":
                //마운트 되있는 종류 찾아서 구매상태로 교체
                for (int i = 0; i <= maxItemNumber; i++)
                {
                    if (storeCheckBox.run2DState[i] == StoreCheckBox.purchaseState.mount)
                    {
                        storeCheckBox.run2DState[i] = StoreCheckBox.purchaseState.Buy;
                        productText[i].gameObject.SetActive(false);
                        productButton[i].gameObject.SetActive(true);
                        continue;
                    }

                }
                // 구매상태를 마운트 상태로 교체
                storeCheckBox.run2DState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "장착중";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // 게임 장착 교체
                player.GetComponent<SpriteRenderer>().sprite = run2D[_number];
                player.GetComponent<Animator>().runtimeAnimatorController = run2Danim[_number];
                break;
        }
    }

    public void SettingMount()
    {
        for(int i=0;i<=maxItemNumber;i++){
            
            if(storeCheckBox.timerState[i] == StoreCheckBox.purchaseState.mount)
            {
                Debug.Log("미정");
            }
            
            if(storeCheckBox.cardState[i] == StoreCheckBox.purchaseState.mount)
            {
                switch (i)
                {
                    case 0:
                        for (int j = 0; j <= 5; j++)
                        {
                            gameManager_1.Case[j] = nomalCard[j];
                        }
                        break;
                    case 1:
                        for (int j = 0; j <= 5; j++)
                        {
                            gameManager_1.Case[j] = joCard[j];
                        }
                        break;
                    case 2:
                        for (int j = 0; j <= 5; j++)
                        {
                            gameManager_1.Case[j] = kimCard[j];
                            
                        }
                        break;
                }
            }

            if(storeCheckBox.run2DState[i] == StoreCheckBox.purchaseState.mount)
            {
                player.GetComponent<SpriteRenderer>().sprite = run2D[i];
                player.GetComponent<Animator>().runtimeAnimatorController = run2Danim[i];
            }

        }
        
        
    }

    //샵 종료
    public void StoreEnd()
    {
        productCaseParent.SetActive(false);
        mainStoreImage.SetActive(true);
        storeUI.SetActive(false);
    }
}
