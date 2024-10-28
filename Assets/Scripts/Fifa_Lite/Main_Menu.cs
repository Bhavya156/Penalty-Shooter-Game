using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    
    public void Play() {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit() {
        Application.Quit();
    }
}
