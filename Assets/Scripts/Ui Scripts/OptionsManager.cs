using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    public GameObject mainOptions;
    public GameObject secondOptions;

    public GameObject tasksButton;
    public GameObject menuButton;

    public void OptionMenu()
    {
        if (mainOptions != null)
        {
            bool isActive = mainOptions.activeSelf;

            mainOptions.SetActive(!isActive);

            isActive = secondOptions.activeSelf;

            secondOptions.SetActive(!isActive);
        }
    }

    public void TaskScene()
    {
        SceneManager.LoadScene("Scheduling");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
