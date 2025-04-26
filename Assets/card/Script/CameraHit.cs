using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHit : MonoBehaviour
{
    // Start is called before the first frame update

    // ???²J ?????
    Vector2 mousePos;
    Camera camera;
    AudioSource audio;


    void Start()
    {
        camera = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.transform.position, new Vector3(0,0,10), Color.green);
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
            RayHit();
        }
    }

    void RayHit()
    {
        if (!GameManager.gameManager.isWait)
        {
            mousePos = Input.mousePosition;
            mousePos = camera.ScreenToWorldPoint(mousePos);

            RaycastHit2D rayHit = Physics2D.Raycast(mousePos, new Vector3(0, 0, 1), 10, LayerMask.GetMask("Card"));

            if (rayHit.collider != null)
            {
                Debug.Log("??");

                GameManager.gameManager.StartCoroutine(GameManager.gameManager.Getray(rayHit.collider));


            }
        }


    }


    

}
