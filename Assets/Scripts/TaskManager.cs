using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TaskManager : MonoBehaviour
{
    //List of tasks
    private List<Task> tasks = new List<Task>();
    private int currentTask;

    //Canvas Files
    [SerializeField] GameObject timerText;
    [SerializeField] GameObject currentTaskText;

    private float currentTimeHour;
    private float currentTimeMinute;
   
    private string currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTask <= tasks.Count - 1)
        {
            SetTimeAndTask();
        }
    }

    void SetTimeAndTask()
    {
        currentTimeHour = DateTime.Now.Hour;
        currentTimeMinute = DateTime.Now.Minute;

        //After beginning timer and before ending timer
        if (currentTimeHour > tasks[currentTask].beginningTime.x && currentTimeMinute > tasks[currentTask].beginningTime.y
            && currentTimeHour < tasks[currentTask].endingTime.x && currentTimeMinute < tasks[currentTask].endingTime.y)
        {
            currentTimeMinute = DateTime.Now.Minute;
            currentTime = currentTimeHour.ToString() + ":" + currentTimeMinute.ToString();
            
        }
        //Before beginning timer
        else if (currentTimeHour < tasks[currentTask].beginningTime.x)
        {

        }
        //Before beginning timer, but accounting for same hour, but less minutes
        else if (currentTimeHour == tasks[currentTask].beginningTime.x && currentTimeMinute < tasks[currentTask].beginningTime.y)
        {

        }
        //Move to next task
        else
        {
            currentTask++;
            SetCurrentTask();
        }

    }

    void SetCurrentTask()
    {
        if(currentTask > tasks.Count - 1)
        {
            currentTaskText.GetComponent<TextMeshPro>().text = "";
            timerText.GetComponent<TextMeshPro>().fontSize = 36;
            timerText.GetComponent<TextMeshPro>().text = "Tasks Complete!";
        }
        else
        {
            currentTaskText.GetComponent<TextMeshPro>().text = "Current Task: " + tasks[currentTask];
        }
    }
}
