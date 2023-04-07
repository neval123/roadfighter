using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InterfaceController : MonoBehaviour
{
    public PlayerMovement player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI fuelText;
    public Image countdown3;
    public Image countdown2;
    public Image countdown1;
    public AudioSource[] audio;
    public Slider distanceLeft;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        distanceLeft.maxValue = GameObject.FindGameObjectWithTag("Finish").transform.position.y;
        distanceLeft.minValue = player.transform.position.y;
        distanceLeft.enabled = false;
        audio = GetComponents<AudioSource>();
        audio[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 1 && !countdown3.IsDestroyed())
        {
            audio[0].Stop();
            audio[0].Play();
            Destroy(countdown3);
        }
        if (Time.timeSinceLevelLoad >= 2 && !countdown2.IsDestroyed())
        {
            audio[0].Stop();
            audio[1].Play();
            Destroy(countdown2);
        }
        if (Time.timeSinceLevelLoad >= 3 && !countdown1.IsDestroyed())
        {
            audio[1].Stop();
            Destroy(countdown1);
        }
        scoreText.text = "1P\n" + player.score;
        fuelText.text = "FUEL\n" + player.fuel;
        speedText.text = player.currentSpeed * 40 + " km\\h";
        distanceLeft.value = player.transform.position.y;
    }
}
