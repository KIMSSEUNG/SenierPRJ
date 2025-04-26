using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{


    ////배경화면 반복을 위한 start , end position 선언
    //[SerializeField]
    //float startPosition;
    //[SerializeField]
    //float endPosition;
    ////배경이 움직이는 스피드
    //[SerializeField]
    //float speed;



    //void Update()
    //{
    //    BGScroll();
    //}

    //void BGScroll(){


    //    transform.Translate(speed * -1*Time.deltaTime, 0, 0);
    //    if (transform.position.x <= -endPosition)
    //    {
    //        transform.position = new Vector2(startPosition, 0);
    //    }
    //}
    [SerializeField]
    float offset;
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    float speed;

    void Update()
    {
        BGScroll();
    }

    void BGScroll()
    {
        offset += speed*Time.deltaTime;
        if (offset > 1)
        {
            offset = offset % 1;
        }
        Vector2 offVec = new Vector2(offset,0);
        mesh.material.SetTextureOffset("_MainTex",offVec);
    }

}
