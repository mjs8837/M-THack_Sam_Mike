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

    // Enum for type of pictures to display
    [SerializeField] enum PictureType { encouraging, neutral, calming, puppy };
    private PictureType currentPictureType = PictureType.encouraging;

    // Timer fields
    float switchTimer = 10.0f;
    private float timeReset = 10.0f;

    // Picture related fields
    private int currentPictureIndex = 0;
    private float currentOpacity = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        displayedPictures = new List<GameObject>();

        // Initializing a list of prefabs based on the enum type
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

        // Setting the first image to display
        displayedPictures[currentPictureIndex].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePhoto(timeReset);
    }

    // Helper function to initialize a list of set prefabs
    private void InstantiateList(List<GameObject> prefabList)
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            GameObject tempPicture = Instantiate(prefabList[i]);
            displayedPictures.Add(tempPicture);
            tempPicture.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.0f);
        }
    }

    // Helper function to update photo timer and change background image
    private void UpdatePhoto(float timeReset)
    {
        // Decreasing the timer before setting up to switch photos
        if (switchTimer > 0.0f)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            // Decreasing opacity of the current photo and increasing the opacity of the next one to cyle through
            if (currentOpacity > 0.0f)
            {
                currentOpacity -= Time.deltaTime / 4.0f;

                if (currentPictureIndex < displayedPictures.Count - 1)
                {
                    displayedPictures[currentPictureIndex].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, currentOpacity);
                    displayedPictures[currentPictureIndex + 1].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1.0f - currentOpacity);           
                }
                else
                {
                    displayedPictures[currentPictureIndex].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, currentOpacity);
                    displayedPictures[0].GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1.0f - currentOpacity);
                }
            }

            // Resetting opacity and timer to reset the cycle as well as increasing the current index
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
}
