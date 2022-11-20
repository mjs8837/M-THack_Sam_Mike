using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskCreator : MonoBehaviour
{
    [SerializeField] GameObject createTaskButton;
    [SerializeField] GameObject taskInput;
    [SerializeField] GameObject startHourDropDown;
    [SerializeField] GameObject endHourDropDown;
    [SerializeField] GameObject startMinuteDropDown;
    [SerializeField] GameObject endMinuteDropDown;
    [SerializeField] GameObject fillInCalendarPrefab;

    private StreamWriter taskWriter;

    [SerializeField] GameObject calendarLayout;

    private List<GameObject> hourBreaks;
    public List<Task> taskList;

    private string taskPath = "taskList.txt";

    string[] startHour;
    string[] endHour;

    int startHourIndex;
    int endHourIndex;
    int startMinuteIndex;
    int endMinuteIndex;

    float startHourFloat;
    float startMinuteFloat;
    float endHourFloat;
    float endMinuteFloat;

    public static TaskCreator taskCreator;

    // Start is called before the first frame update
    void Start()
    {
        hourBreaks = new List<GameObject>();
        hourBreaks = calendarLayout.GetComponent<CalendarLayout>().hourBreakList;
        taskList = new List<Task>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddTaskToCalendar()
    {
        float yPos = new float();

        startHourIndex = startHourDropDown.GetComponent<TMP_Dropdown>().value;
        endHourIndex = endHourDropDown.GetComponent<TMP_Dropdown>().value;
        startMinuteIndex = startMinuteDropDown.GetComponent<TMP_Dropdown>().value;
        endMinuteIndex = endMinuteDropDown.GetComponent<TMP_Dropdown>().value;

        startHour = startHourDropDown.GetComponent<TMP_Dropdown>().options[startHourIndex].text.Split(" ");
        endHour = endHourDropDown.GetComponent<TMP_Dropdown>().options[endHourIndex].text.Split(" ");

        float.TryParse(startHour[0], out startHourFloat);
        float.TryParse(endHour[0], out endHourFloat);

        if (startHourIndex > 6)
        {
            startHourFloat += 12.0f;
        }
        if (endHourIndex > 6)
        {
            endHourFloat += 12.0f;
        }
        
        
        float.TryParse(startMinuteDropDown.GetComponent<TMP_Dropdown>().options[startMinuteIndex].text, out startMinuteFloat);
        float.TryParse(endMinuteDropDown.GetComponent<TMP_Dropdown>().options[endMinuteIndex].text, out endMinuteFloat);

        float totalHours = endHourFloat - startHourFloat;

        float totalMinutes = totalHours * 60.0f;

        if (startMinuteFloat > endMinuteFloat)
        {
            totalMinutes -= startMinuteFloat - endMinuteFloat;
        }
        else
        {
            totalMinutes += endMinuteFloat - startMinuteFloat;
        }

        yPos = hourBreaks[startHourDropDown.GetComponent<TMP_Dropdown>().value].transform.position.y + 1.0f;
        float multiplier = totalMinutes / 60.0f;

        if (multiplier < 1.0f)
        {
            yPos = hourBreaks[startHourDropDown.GetComponent<TMP_Dropdown>().value].transform.position.y + 0.5f;
        }

        GameObject tempCalendarFill = Instantiate(fillInCalendarPrefab);
        tempCalendarFill.transform.position = new Vector3(-2.6f, yPos, -1.0f);
        tempCalendarFill.transform.localScale = new Vector3(7.0f, multiplier, 1.0f);

        Task newTask = new Task(taskInput.GetComponent<TMP_InputField>().text, new Vector2(startHourFloat, startMinuteFloat), new Vector2(endHourFloat, endMinuteFloat));
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
        for (int i = 0; i < taskList.Count; i++)
        {
            PlayerPrefs.SetFloat("Task-" + i + "-StartHour", startHourFloat);
            PlayerPrefs.SetFloat("Task-" + i + "-EndHour", endHourFloat);
        }

        SceneManager.LoadScene("MainScene");
    }
}
