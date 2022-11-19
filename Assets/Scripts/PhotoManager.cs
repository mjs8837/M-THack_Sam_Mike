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

    private GameObject currentImage;

    [SerializeField] enum PictureType { encouraging, neutral, calming, puppy };
    private PictureType currentPictureType = PictureType.encouraging;

    float switchTimer = 10.0f;
    private float timeReset = 10.0f;

    private int currentPictureIndex = 1;

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

        currentImage = displayedPictures[0];
        currentImage.SetActive(true);
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
            tempPicture.SetActive(false);
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
            currentImage.SetActive(false);
            currentImage = displayedPictures[currentPictureIndex];
            currentImage.SetActive(true);
            if (currentPictureIndex < displayedPictures.Count - 1) { currentPictureIndex++; }
            else { currentPictureIndex = 0; }
            
            switchTimer = timeReset;
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
        currentTime = currentTimeHour.ToString() + ":" + currentTimeMinute.ToString();
        timeText.GetComponent<TextMeshPro>().text = currentTime;
    }
}
