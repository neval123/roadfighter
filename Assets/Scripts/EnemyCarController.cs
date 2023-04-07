using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float interval = 1f;
    public float moveSpeed = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float beingFastTime = 1f;
    private PlayerMovement playerMovement;
    public bool isOnLeftSide = false;
    private bool isMovingHorizontal = false;
    private bool scoreGiven = false;
    private bool normalSpeedSet = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb.velocity = new Vector2(0, moveSpeed * 2);
    }

    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > beingFastTime)
        {
            if (!normalSpeedSet)
            {
                rb.velocity = new Vector2(0, moveSpeed);
                normalSpeedSet= true;
            }
            if (isMovingHorizontal && ((transform.position.x > 0.8 && !isOnLeftSide) || (isOnLeftSide && transform.position.x < -0.7)))
            {
                rb.velocity = new Vector2(0, moveSpeed);
                isMovingHorizontal = false;
            }
            if (interval <= Time.timeSinceLevelLoad - beingFastTime)
            {
                interval += interval;
                float horizontal = UnityEngine.Random.Range(-1f, 1f);
                if (horizontal > 0)
                {
                    if (!isOnLeftSide)
                    {
                        if (transform.position.x > -0.6f)
                        {
                            isMovingHorizontal = true;
                            rb.velocity = new Vector2(-horizontalSpeed, moveSpeed);
                            isOnLeftSide = true;
                        }
                        else
                        {
                            isMovingHorizontal = false;
                            rb.velocity = new Vector2(0, moveSpeed);
                        }
                    }
                    else
                    {
                        if (transform.position.x < 0.6f)
                        {
                            isMovingHorizontal = true;
                            rb.velocity = new Vector2(horizontalSpeed, moveSpeed);
                            isOnLeftSide = false;
                        }
                        else
                        {
                            isMovingHorizontal = false;
                            rb.velocity = new Vector2(0, moveSpeed);
                        }
                    }
                }
                else
                {
                    isMovingHorizontal = false;
                    rb.velocity = new Vector2(0, moveSpeed);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerMovement.transform.position.y > transform.position.y && !scoreGiven)
        {
            scoreGiven = true;
            playerMovement.score += 100;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
