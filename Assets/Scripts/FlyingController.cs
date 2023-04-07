using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float flySpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -flySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
