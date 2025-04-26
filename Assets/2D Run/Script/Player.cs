using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player이동키 구현
    bool hDown;
    bool vDown;
    bool hUp;
    bool vUp;
    bool IsPlayerMove;
    public float curPosition_Y;
    public float curPosition_X;
    float Vertical;
    float Horizontal;
    //무적시간
    public int noDamageTime;
    public int heart;
    //런매니저
    public RunManager runManager;
    Vector2 playerMove;
    //데미지구현을 위한 렌더러 정의
    SpriteRenderer playerRenderer;
    //하트 감소 이미지구현
    public Image[] heartImage; 


    private void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //움직임 및 범위를 벗어나는걸 막는 키워드
        
        curPosition_Y = transform.position.y;
        curPosition_X = transform.position.x;
        
        if(Input.GetButtonDown("Horizontal")|| Input.GetButtonDown("Vertical"))
        {
            PlayerMove();
        }
        


    }

    void PlayerMove()
    {
        
        Horizontal = runManager.isStart ? Input.GetAxisRaw("Horizontal") : 0;
        Vertical = runManager.isStart ? Input.GetAxisRaw("Vertical") : 0;
        hDown = Input.GetButtonDown("Horizontal");
        vDown = Input.GetButtonDown("Vertical");
        hUp = Input.GetButtonUp("Horizontal");
        vUp = Input.GetButtonUp("Vertical");
        
        if (curPosition_Y == 2 && Vertical == 1)
        {
            return;
        }
        else if (curPosition_Y == -2 && Vertical == -1)
        {
            return;
        }
        else if (curPosition_X == 0.5 && Horizontal == 1)
        {
            return;
        }
        else if (curPosition_X == -4.5 && Horizontal == -1)
        {
            return;
        }

        if (hDown || vUp)
        {
            IsPlayerMove = true;
        }
        else if (vDown || hUp)
        {
            IsPlayerMove = false;
        }

        playerMove = IsPlayerMove ? new Vector2(Horizontal, 0) : new Vector2(0, Vertical );
        transform.Translate(playerMove); 
    }

    //충돌시 데미지 구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        heart--;
        heartImage[heart].color= Color.gray;

        if (heart == 0)
        {
            foreach (Transform airPlane in runManager.AirPlaneParent.GetComponentsInChildren<Transform>())
            {
                if (!(airPlane.name == "AirPlaneParent"))
                {
                    Destroy(airPlane.gameObject);
                }

            }
            runManager.GameEnd(false);
            return;
        }
        //데미지시 무적
        Debug.Log("데미지");
        playerRenderer.color = Color.gray;
        gameObject.layer = 8;
        Debug.Log(gameObject.layer);

        Debug.Log(heart);
        Invoke("OnDamage", noDamageTime);
        
        
    }

    void OnDamage()
    {
        playerRenderer.color = Color.white;
        gameObject.layer = 7;

    }



}
