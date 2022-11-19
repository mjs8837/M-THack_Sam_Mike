using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    private float currentTimeHour;
    private float currentTimeMinute;
    private string currentTime;

    [SerializeField] GameObject timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentTime();
    }

    // Helper function to set the current time and display it on screen
    private void SetCurrentTime()
    {
        if (DateTime.Now.Hour > 12)
        {
            currentTimeHour = DateTime.Now.Hour - 12;
        }
        else
        {
            currentTimeHour = DateTime.Now.Hour;
        }

        currentTimeMinute = DateTime.Now.Minute;

        if (currentTimeMinute < 10)
        {
            currentTime = currentTimeHour.ToString() + ":0" + currentTimeMinute.ToString();
        }
        else
        {
            currentTime = currentTimeHour.ToString() + ":" + currentTimeMinute.ToString();
        }
        timeText.GetComponent<TextMeshPro>().text = currentTime;
    }
}
