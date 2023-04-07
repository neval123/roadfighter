using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float score = 0;
    public float fuel = 20;
    public float moveSpeed = 4.0f;
    public float currentSpeed = 0f;
    public TextMeshProUGUI finalScore;
    private float timeToEnd;
    private float fuelDecreaseTime = 1.0f;
    private Animator animator;
    private float direction;
    private bool zPressed = false;
    private bool gameEnds = false;
    private bool playerStarted = false;
    private bool infoPlayed = false;
    private bool isLocked = false;
    private bool isCollision = false;
    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool turboClicked = false;
    public Button leftButton, rightButton, turboButton;

    public AudioSource[] audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponents<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnds && Time.timeSinceLevelLoad > timeToEnd)
        {
            SceneManager.LoadScene("Menu");
        }
        if (Time.timeSinceLevelLoad > 3 && !gameEnds && !isLocked)
        {
            if (fuel <= 0)
            {
                audio[1].Stop();
                finalScore.SetText("Final score: " + score);
                timeToEnd = Time.timeSinceLevelLoad + 5;
                rb.velocity = new Vector2(0, 0);
                gameEnds = true;
            }
            if (isCollision)
            {
                Vector2 playerPos = transform.position;
                playerPos.x = 0;
                isCollision = false;
                rb.MovePosition(playerPos);
            }
            if (!playerStarted)
            {
                audio[1].Play();
                playerStarted = true;
                rb.velocity = new Vector2(direction * moveSpeed, moveSpeed);
            }
            else
            {
                direction = Input.GetAxisRaw("Horizontal");
            }
            if (Input.GetKey(KeyCode.Z) || turboClicked)
            {
                zPressed = true;
            }
            else
            {
                zPressed = false;
            }
            if (fuel < 10 && !infoPlayed)
            {
                audio[2].Play();
                infoPlayed = true;
            }
            if (audio[2].isPlaying && fuel > 10)
            {
                audio[2].Stop();
                infoPlayed = false;
            }
            if (Time.timeSinceLevelLoad - 3 > fuelDecreaseTime && fuel > 0)
            {
                fuelDecreaseTime++;
                fuel--;
            }
            
        }
    }
    private void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > 3 && !gameEnds && !isLocked && fuel > 0) 
        {
            if (zPressed && fuel > 0)
            {
                if (rightPressed)
                {
                    rb.velocity = new Vector2(moveSpeed, moveSpeed * 2);
                }
                else if (leftPressed)
                {
                    rb.velocity = new Vector2(-moveSpeed, moveSpeed * 2);
                }
                else
                {
                    rightPressed = false;
                    leftPressed = false;
                    rb.velocity = new Vector2(direction * moveSpeed, moveSpeed * 2);
                }
                currentSpeed = moveSpeed * 2;
            }
            if (!zPressed)
            {
                if (rightPressed)
                {
                    rb.velocity = new Vector2(moveSpeed, moveSpeed);
                }
                else if (leftPressed)
                {
                    rb.velocity = new Vector2(-moveSpeed, moveSpeed);
                }
                else
                {
                    rightPressed = false;
                    leftPressed = false;
                    rb.velocity = new Vector2(direction * moveSpeed, moveSpeed);
                }
                currentSpeed = moveSpeed;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.Play("Player_Slide");
            audio[0].Play();
        }
        else if (collision.gameObject.tag == "Finish")
        {
            audio[1].Stop();
            finalScore.SetText("Final score: " + score);
            gameEnds = true;
            rb.velocity = Vector2.zero;
            timeToEnd = Time.timeSinceLevelLoad + 5;
        }
        else if (collision.gameObject.tag == "Bonus")
        {
            score += 500;
            audio[4].Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "MegaBonus")
        {
            score += 2000;
            audio[4].Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "ExtraFuel")
        {
            fuel += 20;
            audio[4].Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            animator.Play("Player_Blow");
            audio[3].Play();
            Destroy(collision.gameObject);
            rb.velocity = new Vector2(0, 0);
            isCollision = true;
            isLocked = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play("Player_Blow");
        audio[3].Play();
        rb.velocity = new Vector2(0, 0);
        isCollision = true;
        isLocked = true;
    }
    private void waitForAnimationToFinish()
    {
        isLocked = false;
    }
    public void PointerDownRight()
    {
        rightPressed = true;
    }
    public void PointerUpRight()
    {
        rightPressed = false;
    }
    public void PointerDownLeft()
    {
        leftPressed = true;
    }
    public void PointerUpLeft()
    {
        leftPressed = false;
    }

    public void PointerDownTurbo()
    {
        zPressed = true;
        turboClicked = true;
    }
    public void PointerUpTurbo()
    {
        zPressed = false;
        turboClicked = false;
    }
}
