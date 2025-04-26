using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{

    //���� ù �̹��� On/Off ���� gameobject ����
    public GameObject mainStoreImage;
    
    //����üũ�ڽ�
    StoreCheckBox storeCheckBox;

    //��������� �̱���
    public static StoreManager storeManager;

    //����� UI
    public GameObject storeUI;

    public GameManager gameManager_1;

    //��ǰ �θ� , ���� �ڽ�
    public GameObject productCaseParent;
    Image[] productImageCase;
    Text[] productText;
    public Button[] productButton;
    
    //�� ���Ӻ� ��ǰ �̹���
    public Sprite[] run2D;
    public Sprite[] timer;
    public Sprite[] card;

    //�� ���Ӻ� ���� ����迭
    //2D RUN
    public RuntimeAnimatorController[] run2Danim;
    public Player player;

    //Card(�޸� 1,2,3,4,5)
    public Sprite[] joCard;
    public Sprite[] kimCard;
    public Sprite[] nomalCard;

    //Timer ����

    //���� ���� ī�װ� ,���ڸ� ���� string , int��
    string gameCategory=null;
    int gameNumber = 0;

    //���� Ȯ�� UI
    public GameObject purchaseUI;

    //��ǰ������ ���� int��
    int productPrice=0;

    //���� ���� , ���� �ȳ���
    public Text purchaseRespond;

    //������ ���ð���(0���� ����)
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
        //��ũ��Ʈ �Ҵ�
        storeCheckBox = GetComponent<StoreCheckBox>();

        // �̹��� �迭 �ֱ�
        productImageCase = productCaseParent.GetComponentsInChildren<Image>();
        productText= productCaseParent.GetComponentsInChildren<Text>();

        //�׽�Ʈ
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
                                productText[i].text = "5000��";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000��";
                            }
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "������";
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
                                productText[i].text = "5000��";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000��";
                            }
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "������";
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
                                productText[i].text = "5000��";
                            }
                            else if (i == 2)
                            {
                                productText[i].text = "10000��";
                            }
                            productButton[i].gameObject.SetActive(false);
                            productText[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.Buy:
                            productText[i].gameObject.SetActive(false);
                            productButton[i].gameObject.SetActive(true);
                            break;
                        case StoreCheckBox.purchaseState.mount:
                            productText[i].text = "������";
                            productText[i].gameObject.SetActive(true);
                            productButton[i].gameObject.SetActive(false);
                            break;
                    }
                }
                break;
        }
    }


    //��ǰŬ��
    public void ProductChoice(int _number)
    {
        //���ſϷ���¸� �׳� ������
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
        //�ƴҽ� ��ǰ���� ������ purchaseUI ON
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

    //��ǰ����
    public void PurchaseProduct()
    {
        //���Ű� �Ǵ��� Ȯ���ϴ� ��
        if (ControlManager.controlManager.storagePoint >= productPrice)
        {
            ControlManager.controlManager.storagePoint -= productPrice;
            //�����ϱ� ��ȣǥ��
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
            //ȭ�鲨��
            purchaseUI.SetActive(false);
            purchaseRespond.text = "���ż���";
            //�������� �� ����
            ControlManager.controlManager.viewPoint.text = ControlManager.controlManager.storagePoint+"��";


        }
        else //���žȵ�
        {
            //�ʱ�ȭ �� ����ȭ�� ����
            purchaseUI.SetActive(false);
            purchaseRespond.text = "���Ž���";

        }
        purchaseRespond.color= new Color(purchaseRespond.color.r, purchaseRespond.color.g, purchaseRespond.color.b, 255f);
        Invoke("ClearText", 2f);

    }



    //����Ȯ�� �ִϸ��̼�
    void ClearText()
    {
        purchaseRespond.color = new Color(purchaseRespond.color.r, purchaseRespond.color.g, purchaseRespond.color.b, 0f);

    }
    


    //��ǰ�������
    public void PurchaseCancle()
    {
        purchaseUI.SetActive(false);
    }

    //��ǰ ����
    public void ChooseButton(int _number)
    {
        switch (gameCategory)
        {
            case "Timer":
                //����Ʈ ���ִ� ���� ã�Ƽ� ���Ż��·� ��ü
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
                // ���Ż��¸� ����Ʈ ���·� ��ü
                storeCheckBox.timerState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "������";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // ���� ���� ��ü(timer���� ����)
                break;

            case "Card":
                //����Ʈ ���ִ� ���� ã�Ƽ� ���Ż��·� ��ü
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
                // ���Ż��¸� ����Ʈ ���·� ��ü
                storeCheckBox.cardState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "������";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // ���� ���� ��ü

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
                //����Ʈ ���ִ� ���� ã�Ƽ� ���Ż��·� ��ü
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
                // ���Ż��¸� ����Ʈ ���·� ��ü
                storeCheckBox.run2DState[_number] = StoreCheckBox.purchaseState.mount;
                productText[_number].text = "������";
                productText[_number].gameObject.SetActive(true);
                productButton[_number].gameObject.SetActive(false);
                // ���� ���� ��ü
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
                Debug.Log("����");
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

    //�� ����
    public void StoreEnd()
    {
        productCaseParent.SetActive(false);
        mainStoreImage.SetActive(true);
        storeUI.SetActive(false);
    }
}
