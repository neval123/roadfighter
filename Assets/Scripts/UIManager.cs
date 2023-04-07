using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public AudioSource sound;
   
    public void StartGame(){
        Debug.Log("Starting the game...");
        SceneManager.LoadScene("Game");
    }

        

}
