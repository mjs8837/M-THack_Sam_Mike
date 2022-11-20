using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private GameObject taskCreator = null;

    // Start is called before the first frame update
    void Start()
    {
        taskCreator = GameObject.Find("TaskCreation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "sceneButton")
        {
            string[] objectName = gameObject.name.Split(" ");
            SceneManager.LoadScene(objectName[0]);
        }
        else
        {
            try
            {
                taskCreator.GetComponent<TaskCreator>().CreateTask();
            }
            catch
            {
                Debug.Log("Task Creator is null");
            }
        }
    }
}
