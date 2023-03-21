using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public string taskType;

    //Vector2 stores beginning and ending time. x is hours, y is minutes
    public Vector2 beginningTime;
    public Vector2 endingTime;

    public string mood;

    public bool isCompleted;

    public Task(string _taskType, Vector2 _beginningTime, Vector2 _endingTime)
    {
        taskType = _taskType;
        beginningTime = _beginningTime;
        endingTime = _endingTime;
    }

    public Task()
    {
        taskType = "Default Task";
        beginningTime = new Vector2(0,0);
        endingTime = new Vector2(0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        TaskCreator.RemoveTaskFromList(gameObject);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
