using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatsUI : MonoBehaviour
{
    public GameObject menuButton;

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

