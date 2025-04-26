using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{


    ////���ȭ�� �ݺ��� ���� start , end position ����
    //[SerializeField]
    //float startPosition;
    //[SerializeField]
    //float endPosition;
    ////����� �����̴� ���ǵ�
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
