using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    float scrollSpeed;
    [SerializeField]
    float offset;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        BGScroll();
    }

    void BGScroll()
    {
        offset -= scrollSpeed * Time.deltaTime;
        if (offset < -1)
        {
            offset = offset % 1;
        }
        Vector2 offVec = new Vector2(0, offset);
        mesh.material.SetTextureOffset("_MainTex", offVec);
    }
}
