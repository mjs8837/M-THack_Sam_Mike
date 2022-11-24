using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskCreator : MonoBehaviour
{
    // Prefabs to clone
    [SerializeField] GameObject createTaskButton;
    [SerializeField] GameObject taskInput;
    [SerializeField] GameObject startHourDropDown;
    [SerializeField] GameObject endHourDropDown;
    [SerializeField] GameObject startMinuteDropDown;
    [SerializeField] GameObject endMinuteDropDown;
    [SerializeField] GameObject fillInCalendarPrefab;

    [SerializeField] GameObject calendarLayout;

    // Relavent lists
    private List<GameObject> hourBreaks;
    public List<GameObject> taskList;

    // Split strings for calculating start and end hour
    string[] startHour;
    string[] endHour;

    // Time calculating variables
    int startHourIndex;
    int endHourIndex;
    int startMinuteIndex;
    int endMinuteIndex;

    float startHourFloat;
    float startMinuteFloat;
    float endHourFloat;
    float endMinuteFloat;
    string taskName;

    public static TaskCreator taskCreator;

    // Start is called before the first frame update
    void Start()
    {
        hourBreaks = new List<GameObject>();
        hourBreaks = calendarLayout.GetComponent<CalendarLayout>().hourBreakList;
        taskList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        taskName = taskInput.GetComponent<TMP_InputField>().text;
    }

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

        // Breaking out of the function if the user tries to create a task with negative or zero time
        if (totalMinutes <= 0.0f)
        {
            return;
        }

        // Checking if the minute value should be added or subracted from the total minutes
        if (startMinuteFloat > endMinuteFloat)
        {
            totalMinutes -= startMinuteFloat - endMinuteFloat;
        }
        else
        {
            totalMinutes += endMinuteFloat - startMinuteFloat;
        }

        // Setting up scale and position values
        float multiplier = totalMinutes / 60.0f;
        float minuteOffset = startMinuteFloat / 60.0f;
        yPos = hourBreaks[startHourDropDown.GetComponent<TMP_Dropdown>().value].transform.position.y + 1.0f - minuteOffset;

        // Filling the calendar where a task is createds
        GameObject tempCalendarFill = Instantiate(fillInCalendarPrefab);
        tempCalendarFill.transform.position = new Vector3(-2.6f, yPos, -1.0f);
        tempCalendarFill.transform.localScale = new Vector3(7.0f, multiplier, 1.0f);

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
        
        AddTaskToList(taskInput.GetComponent<TMP_InputField>().text, 
            new Vector2(startHourFloat, startMinuteFloat), 
            new Vector2(endHourFloat, endMinuteFloat));

     /* PlayerPrefs.SetFloat("Task-" + taskList.Count + "-StartHour", startHourFloat);
        PlayerPrefs.SetFloat("Task-" + taskList.Count + "-EndHour", endHourFloat);
        PlayerPrefs.SetFloat("Task-" + taskList.Count + "StartMinute", startMinuteFloat);
        PlayerPrefs.SetFloat("Task-" + taskList.Count + "EndMinute", endMinuteFloat);
        PlayerPrefs.SetString("Task-" + taskList.Count + "-Name", taskName); 
     */
    }

    private void AddTaskToList(string taskName, Vector2 startTime, Vector2 endTime)
    {
        GameObject newTask = new GameObject();
        newTask.AddComponent<Task>();
        newTask.GetComponent<Task>().taskType = taskName;
        newTask.GetComponent<Task>().beginningTime = startTime;
        newTask.GetComponent<Task>().endingTime = endTime;
        taskList.Add(newTask);
    }

    public void SwtichScene()
    {
        /*if (!startMinuteDropDown.GetComponent<TMP_Dropdown>().IsExpanded && !endMinuteDropDown.GetComponent<TMP_Dropdown>().IsExpanded)
        {
            // Set up some checks in here to only write a task if conditions are met
            taskWriter = new StreamWriter(taskPath, true);
            taskWriter.WriteLine(taskInput.GetComponent<TMP_InputField>().text);
            startHour = startHourDropDown.GetComponent<TMP_Dropdown>().options[startHourIndex].text.Split(" ");
            endHour = endHourDropDown.GetComponent<TMP_Dropdown>().options[endHourIndex].text.Split(" ");

            taskWriter.WriteLine(startHour[0] + ":" + startMinuteDropDown.GetComponent<TMP_Dropdown>().options[startMinuteIndex].text + startHour[1]);
            taskWriter.WriteLine(endHour[0] + ":" + endMinuteDropDown.GetComponent<TMP_Dropdown>().options[endMinuteIndex].text + endHour[1]);

            taskWriter.Close();
        }*/

        SceneManager.LoadScene("MainScene");
    }
}
