using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PhotoManager : MonoBehaviour
{
    // All picture lists
    [SerializeField] List<GameObject> encouragingPictures;
    [SerializeField] List<GameObject> neutralPictures;
    [SerializeField] List<GameObject> calmingPictures;
    [SerializeField] List<GameObject> puppyPictures;

    private List<GameObject> displayedPictures;

    [SerializeField] enum PictureType { encouraging, neutral, calming, puppy };
    private PictureType currentPictureType = PictureType.encouraging;

    [SerializeField] float switchTimer = 1.0f;
    private float timeReset = 1.0f;

    [SerializeField] int currentPictureIndex = 0;

    private float currentOpacity = 1.0f;

    private float currentTimeHour;
    private float currentTimeMinute;
    private string currentTime;

    [SerializeField] GameObject timeText;

    // Start is called before the first frame update
    void Start()
    {
        displayedPictures = new List<GameObject>();

        switch (currentPictureType)
        {
            case PictureType.encouraging:
                InstantiateList(encouragingPictures);
                break;
            case PictureType.neutral:
                InstantiateList(neutralPictures);
                break;
            case PictureType.calming:
                InstantiateList(calmingPictures);
                break;
            case PictureType.puppy:
                InstantiateList(puppyPictures);
                break;
            default:
                Debug.Log("");
                break;
        }

        displayedPictures[currentPictureIndex].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePhoto(timeReset);
        SetCurrentTime();
    }

    private void InstantiateList(List<GameObject> prefabList)
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            GameObject tempPicture = Instantiate(prefabList[i]);
            displayedPictures.Add(tempPicture);
            tempPicture.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    // Helper function to update photo timer and change background image
    private void UpdatePhoto(float timeReset)
    {
        if (switchTimer > 0.0f)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (currentOpacity > 0.0f)
            {
                currentOpacity -= Time.deltaTime / 4.0f;

                if (currentPictureIndex < displayedPictures.Count - 1)
                {
                    displayedPictures[currentPictureIndex].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, currentOpacity);
                    displayedPictures[currentPictureIndex + 1].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f - currentOpacity);           
                }
                else
                {
                    displayedPictures[0].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f - currentOpacity);
                }
            }

            else
            {
                currentOpacity = 1.0f;
                switchTimer = timeReset;

                // Setting the index if it reaches the end of the current list
                if (currentPictureIndex < displayedPictures.Count - 1)
                {
                    currentPictureIndex++;
                }
                else { currentPictureIndex = 0; }
            }
        }
    }

    // Helper function to set the current time and display it on screen
    private void SetCurrentTime()
    {
        if (DateTime.Now.Hour > 12)
        {
            currentTimeHour = DateTime.Now.Hour - 12;
        }
        else
        {
            currentTimeHour = DateTime.Now.Hour;
        }

        currentTimeMinute = DateTime.Now.Minute;

        if (currentTimeMinute < 10)
        {
            currentTime = currentTimeHour.ToString() + ":0" + currentTimeMinute.ToString();
        }
        else
        {
            currentTime = currentTimeHour.ToString() + ":" + currentTimeMinute.ToString();
        }      
        timeText.GetComponent<TextMeshPro>().text = currentTime;
    }
}
