using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarLayout : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    [SerializeField] GameObject hourBreak;
    [SerializeField] GameObject timePrefab;

    [SerializeField] Camera mainCamera;
    [SerializeField] Canvas canvas;

    private int numRows = 18;

    // Start is called before the first frame update
    void Start()
    {
        int timeValue = 6;

        for (int i = 0; i < numRows; i++)
        {
            GameObject tempRow = Instantiate(rowPrefab);
            tempRow.transform.position = new Vector3(-3.8f, 3.3f - i, 0.0f);

            GameObject tempHourBreak = Instantiate(hourBreak);
            tempHourBreak.transform.position = new Vector3(-3.8f, 3.8f - i, -1.0f);

            GameObject tempHourMark = Instantiate(timePrefab);
            tempHourMark.transform.position = new Vector3(-11.0f, 2.8f - i, -1.0f);

            if (timeValue < 13)
            {
                if (timeValue == 12)
                {
                    tempHourMark.GetComponent<TextMeshPro>().text = timeValue.ToString() + " PM";
                }
                tempHourMark.GetComponent<TextMeshPro>().text = timeValue.ToString() + " AM";
            }
            else
            {
                tempHourMark.GetComponent<TextMeshPro>().text = (timeValue - 12).ToString() + " PM";
            }
            timeValue++;
        }

        GameObject downwardLine = Instantiate(hourBreak);
        downwardLine.transform.position = new Vector3(-8.0f, -5.2f, -1.0f);
        downwardLine.transform.localScale = new Vector3(18.0f, 0.01f, 1.0f);
        downwardLine.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.y > -10.0f && Input.mouseScrollDelta.y < 0.0f)
        {
            mainCamera.transform.position += new Vector3(0.0f, Input.mouseScrollDelta.y, 0.0f);
            canvas.transform.position += new Vector3(0.0f, Input.mouseScrollDelta.y, 0.0f);
        }
        else if (mainCamera.transform.position.y < 0.0f && Input.mouseScrollDelta.y > 0.0f)
        {
            mainCamera.transform.position += new Vector3(0.0f, Input.mouseScrollDelta.y, 0.0f);
            canvas.transform.position += new Vector3(0.0f, Input.mouseScrollDelta.y, 0.0f);
        }
    }
}
