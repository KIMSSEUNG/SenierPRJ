using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefab : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -6.7)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector2.left * speed;
    }
}
