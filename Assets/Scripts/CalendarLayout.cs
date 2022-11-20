using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarLayout : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    [SerializeField] GameObject hourBreak;

    private int numRows = 18;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numRows; i++)
        {
            GameObject tempRow = Instantiate(rowPrefab);
            tempRow.transform.position = new Vector3(-3.8f, 4.3f - i, 0.0f);

            GameObject tempHourBreak = Instantiate(hourBreak);
            tempHourBreak.transform.position = new Vector3(-3.8f, 4.8f - i, -1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
