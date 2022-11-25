using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskCreator : MonoBehaviour
{
    // Prefabs to clone
    [SerializeField] GameObject taskInput;
    [SerializeField] GameObject startHourDropDown;
    [SerializeField] GameObject endHourDropDown;
    [SerializeField] GameObject startMinuteDropDown;
    [SerializeField] GameObject endMinuteDropDown;
    [SerializeField] GameObject fillInCalendarPrefab;
    [SerializeField] GameObject errorTextPrefab;

    [SerializeField] GameObject calendarLayout;

    // Relavent lists
    private List<GameObject> hourBreaks;
    public static List<GameObject> taskList;
    [SerializeField] List<GameObject> calendarFills;

    // Split strings for calculating start and end hour
    string[] startHour;
    string[] endHour;

    // Time calculating variables
    int startHourIndex;
    int endHourIndex;
    int startMinuteIndex;
    int endMinuteIndex;

    float timeErrorTextTimer = 5.0f;
    float taskErrorTextTimer = 5.0f;

    float startHourFloat;
    float startMinuteFloat;
    float endHourFloat;
    float endMinuteFloat;
    string taskName;

    GameObject timeErrorText;
    GameObject taskErrorText;

    public static TaskCreator taskCreator;

    // Start is called before the first frame update
    void Start()
    {
        taskList = new List<GameObject>();
        hourBreaks = new List<GameObject>();
        hourBreaks = calendarLayout.GetComponent<CalendarLayout>().hourBreakList;
        
    }

    // Update is called once per frame
    void Update()
    {
        taskName = taskInput.GetComponent<TMP_InputField>().text;
        CountErrorTimer();
    }

    // Function to add tasks to the calendar in the scene
    public void AddTaskToCalendar()
    {
        float yPos = new float();

        // Setting all starting values
        startHourIndex = startHourDropDown.GetComponent<TMP_Dropdown>().value;
        endHourIndex = endHourDropDown.GetComponent<TMP_Dropdown>().value;
        startMinuteIndex = startMinuteDropDown.GetComponent<TMP_Dropdown>().value;
        endMinuteIndex = endMinuteDropDown.GetComponent<TMP_Dropdown>().value;

        startHour = startHourDropDown.GetComponent<TMP_Dropdown>().options[startHourIndex].text.Split(" ");
        endHour = endHourDropDown.GetComponent<TMP_Dropdown>().options[endHourIndex].text.Split(" ");

        // Getting the hour value from each hour dropdown box
        float.TryParse(startHour[0], out startHourFloat);
        float.TryParse(endHour[0], out endHourFloat);

        // Converting the hour value to military time to do calculations
        if (startHourIndex > 6)
        {
            startHourFloat += 12.0f;
        }
        if (endHourIndex > 6)
        {
            endHourFloat += 12.0f;
        }
        
        // Getting the minute value for each minute dropdown box
        float.TryParse(startMinuteDropDown.GetComponent<TMP_Dropdown>().options[startMinuteIndex].text, out startMinuteFloat);
        float.TryParse(endMinuteDropDown.GetComponent<TMP_Dropdown>().options[endMinuteIndex].text, out endMinuteFloat);

        float totalHours = endHourFloat - startHourFloat;

        float totalMinutes = totalHours * 60.0f;

        // Checking if the minute value should be added or subracted from the total minutes
        if (startMinuteFloat > endMinuteFloat)
        {
            totalMinutes -= startMinuteFloat - endMinuteFloat;
        }
        else
        {
            totalMinutes += endMinuteFloat - startMinuteFloat;
        }

        // Breaking out of the function if the user tries to create a task with negative or zero time
        if (totalMinutes < 30.0f)
        {
            if (timeErrorText == null && taskErrorText == null)
            {
                timeErrorText = Instantiate(errorTextPrefab);
                timeErrorText.name = "TimeErrorText";
                timeErrorText.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
            }
            else
            {
                timeErrorTextTimer = 5.0f;
            }

            return;
        }

        // Setting up scale and position values
        float multiplier = totalMinutes / 60.0f;
        float minuteOffset = startMinuteFloat / 60.0f;
        yPos = hourBreaks[startHourDropDown.GetComponent<TMP_Dropdown>().value].transform.position.y + 1.0f - minuteOffset;

        for (int i = 0; i < taskList.Count; i++)
        {
            if (startHourFloat == taskList[i].GetComponent<Task>().beginningTime[0])
            {
                if (startMinuteFloat <= taskList[i].GetComponent<Task>().endingTime[1])
                {
                    Debug.Log("TEST");
                    return;
                }
            }
        }

        // Filling the calendar where a task is createds
        GameObject tempCalendarFill = Instantiate(fillInCalendarPrefab);
        tempCalendarFill.transform.position = new Vector3(-2.6f, yPos, -1.0f);
        tempCalendarFill.transform.localScale = new Vector3(7.0f, multiplier, 1.0f);

        // Looping through all the calendar fill children to populate them correctly
        for (int i = 0; i < tempCalendarFill.GetComponentsInChildren<TMP_Text>().Length; i++)
        {
            // Setting the task name text and its scale appropriately
            if (tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].name == "TaskName")
            {
                tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].text = taskInput.GetComponent<TMP_InputField>().text;
                tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].gameObject.transform.localScale = new Vector3(
                    tempCalendarFill.GetComponentInChildren<TMP_Text>().gameObject.transform.localScale.x,
                    1 / tempCalendarFill.transform.localScale.y,
                    -1.0f);
            }

            // Setting the time text and its scale appropriately
            if (tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].name == "TimeText")
            {
                string startTimeOfDay = "am";
                string endTimeOfDay = "am";

                if (startHourFloat >= 12.0f)
                {
                    startTimeOfDay = "pm";
                }
                if (endHourFloat >= 12.0f)
                {
                    endTimeOfDay = "pm";
                }

                tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].text = startHour[0] + ":" + startMinuteFloat.ToString("00") + startTimeOfDay + 
                    " - " + endHour[0] + ":" + endMinuteFloat.ToString("00") + endTimeOfDay;

                tempCalendarFill.GetComponentsInChildren<TMP_Text>()[i].gameObject.transform.localScale = new Vector3(
                    tempCalendarFill.GetComponentInChildren<TMP_Text>().gameObject.transform.localScale.x,
                    1 / tempCalendarFill.transform.localScale.y,
                    -1.0f);
            }
        }

        AddTaskToList(taskName, 
            new Vector2(startHourFloat, startMinuteFloat), 
            new Vector2(endHourFloat, endMinuteFloat));
    }
    
    // Helper function to count timers in relavent situations
    private void CountErrorTimer()
    {
        if (timeErrorText != null)
        {
            if (timeErrorTextTimer > 0.0f)
            {
                timeErrorTextTimer -= Time.deltaTime;
            }
            else
            {
                timeErrorTextTimer = 5.0f;
                Destroy(timeErrorText);
            }
        }
        if (taskErrorText != null)
        {
            if (taskErrorTextTimer > 0.0f)
            {
                taskErrorTextTimer -= Time.deltaTime;
            }
            else
            {
                taskErrorTextTimer = 5.0f;
                Destroy(taskErrorText);
            }
        }
    }

    // Adds tasks to the task list with proper information
    private void AddTaskToList(string taskName, Vector2 startTime, Vector2 endTime)
    {
        GameObject newTask = new GameObject();
        newTask.AddComponent<Task>();
        newTask.GetComponent<Task>().taskType = taskName;
        newTask.GetComponent<Task>().beginningTime = startTime;
        newTask.GetComponent<Task>().endingTime = endTime;
        taskList.Add(newTask);
    }

    // Function to attempt to switch scenes 
    public void SwtichScene()
    {
        if (taskList.Count > 0)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                DontDestroyOnLoad(taskList[i]);
            }

            SceneManager.LoadScene("MainScene");
        }
        else
        {
            if (taskErrorText == null && timeErrorText == null)
            {
                taskErrorText = Instantiate(errorTextPrefab);
                taskErrorText.name = "TaskErrorText";
                taskErrorText.GetComponent<TMP_Text>().text = "Please add a task before starting your schedule.";
                taskErrorText.transform.position = new Vector3(0.0f, 0.0f, -1.0f);
            }

            else
            {
                taskErrorTextTimer = 5.0f;
            }
        }
        
    }
}
