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

    private float timerHour;
    private float timerMinute;
   
    private string timer;

    TaskCreator taskCreator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        Task tempTask1 = new Task("Homework", new Vector2(12,00), new Vector2(02,30));
        Task tempTask2 = new Task("Nap", new Vector2(16, 30), new Vector2(18, 00));
        Task tempTask3 = new Task("Exam", new Vector2(10, 00), new Vector2(11, 50));

        //tasks.Add(tempTask1);
        tasks.Add(tempTask2);
        tasks.Add(tempTask3);

        for (int i = 0; i < PlayerPrefs.GetInt("NumTasks"); i++)
        {
            tasks.Add(new Task
                (PlayerPrefs.GetString("Task-" + i + "-Name"),
                new Vector2(PlayerPrefs.GetFloat("Task-" + i + "-StartHour"), PlayerPrefs.GetFloat("Task-" + i + "-StartMinute")),
                new Vector2(PlayerPrefs.GetFloat("Task-" + i + "-EndHour"), PlayerPrefs.GetFloat("Task-" + i + "-EndMinute"))));
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            Debug.Log(tasks[i].taskType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentTask);
        if (currentTask <= tasks.Count - 1)
        {
            SetTimeAndTask();
        }
    }

    void SetTimeAndTask()
    {
        currentTimeHour = DateTime.Now.Hour;
        currentTimeMinute = DateTime.Now.Minute;

        string tempString = currentTimeHour.ToString() + currentTimeMinute.ToString();
        if (currentTimeMinute == 0)
            tempString = tempString + "0";
        else if (currentTimeMinute < 10)
        {
            //Debug.Log(currentTimeMinute);
            tempString = currentTimeHour.ToString() + "0" + currentTimeMinute.ToString();
        }
            
        int.TryParse(tempString, out int currentTime);

        tempString = tasks[currentTask].beginningTime.x.ToString() + tasks[currentTask].beginningTime.y.ToString();
        if (tasks[currentTask].beginningTime.y == 0)
            tempString = tempString + "0";
        //else if (currentTask < 10)
            //tempString = currentTimeHour.ToString() + "0" + currentTimeMinute.ToString();
        int.TryParse(tempString, out int beginningTime);

        tempString = tasks[currentTask].endingTime.x.ToString() + tasks[currentTask].endingTime.y.ToString();
        if (tasks[currentTask].endingTime.y == 0)
            tempString = tempString + "0";
        //else if (currentTask < 10)
            //tempString = currentTimeHour.ToString() + "0" + currentTimeMinute.ToString();
        int.TryParse(tempString, out int endingTime);

        //Debug.Log(currentTime > beginningTime);
        //Debug.Log("Current time " + currentTime);
        //Debug.Log("Beginning time " + beginningTime);
        //Debug.Log("Ending time " + endingTime);


        //After beginning timer and before ending timer
        if (currentTime > beginningTime
            && currentTime < endingTime)
        {
            //Debug.Log("Running during");
            timerHour = tasks[currentTask].endingTime.x - currentTimeHour;
            timerMinute = tasks[currentTask].endingTime.y - currentTimeMinute;

            if(timerMinute < 0)
            {
                timerHour--;
                timerMinute = 60 + tasks[currentTask].endingTime.y - currentTimeMinute;
            }

            SetCurrentTask(false);
            FindTimerText();
        }
        //Before beginning timer
        else if (currentTime < beginningTime)
        {
            //Debug.Log("Running before");
            timerHour = tasks[currentTask].beginningTime.x - currentTimeHour;
            timerMinute = tasks[currentTask].beginningTime.y - currentTimeMinute;

            Debug.Log(timerMinute);

            if (timerMinute < 0)
            {
                timerHour--;
                timerMinute = 60 + tasks[currentTask].beginningTime.y - currentTimeMinute;
            }

            SetCurrentTask(true);
            FindTimerText();
        }
        //Move to next task
        else
        {
            //Debug.Log("Running after");
            currentTask++;
            SetCurrentTask(false);          
        }
    }

    void SetCurrentTask(bool isBefore)
    {
        if(currentTask > tasks.Count - 1)
        {
            currentTaskText.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
            currentTaskText.GetComponent<TextMeshPro>().text = "Tasks Complete";
            timerText.GetComponent<TextMeshPro>().fontSize = 72;
            timerText.GetComponent<TextMeshPro>().text = "Time to relax!";
        }
        else if (isBefore)
        {
            currentTaskText.GetComponent<TextMeshPro>().text = "Time Remaining Till: " + tasks[currentTask].taskType;
        }
        else
        {
            currentTaskText.GetComponent<TextMeshPro>().text = "Current Task: " + tasks[currentTask].taskType;
        }
    }

    void FindTimerText()
    {
        if (timerHour > 10)
        {
            if (timerMinute > 10)
            {
                timer = timerHour.ToString() + ":" + timerMinute.ToString();
            }
            else
            {
                timer = timerHour.ToString() + ":0" + timerMinute.ToString();
            }
        }
        else
        {
            if (timerMinute > 10)
            {
                timer = "0" + timerHour.ToString() + ":" + timerMinute.ToString();
            }
            else
            {
                timer = "0" + timerHour.ToString() + ":0" + timerMinute.ToString();
            }
        }

        timerText.GetComponent<TextMeshPro>().text = timer;
    }
}
