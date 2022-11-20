using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class TaskCreator : MonoBehaviour
{
    [SerializeField] GameObject createTaskButton;
    [SerializeField] GameObject taskInput;

    private StreamWriter taskWriter;

    private string taskPath = "taskList.txt";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTask()
    {
        // Set up some checks in here to only write a task if conditions are met
        taskWriter = new StreamWriter(taskPath, true);
        taskWriter.WriteLine(taskInput.GetComponent<TMP_InputField>().text);
        taskWriter.Close();
    }
}
