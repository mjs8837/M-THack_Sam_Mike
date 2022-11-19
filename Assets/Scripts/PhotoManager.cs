using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] List<GameObject> encouragingPictures;
    [SerializeField] List<GameObject> neutralPictures;
    [SerializeField] List<GameObject> calmingPictures;
    [SerializeField] List<GameObject> puppyPictures;

    private GameObject currentImage;

    [SerializeField] enum PictureType { encouraging, neutral, calming, puppy };
    private PictureType currentPicture = PictureType.encouraging;

    [SerializeField] float switchTimer = 10.0f;
    private float timeReset = 10.0f;

    private int pictureCount = 0;
    private int currentPictureIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        encouragingPictures = new List<GameObject>();
        neutralPictures = new List<GameObject>();
        calmingPictures = new List<GameObject>();
        puppyPictures = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePhoto(timeReset);
        currentImage = GameObject.Find("CurrentImage");
    }

    // Helper function to reset 
    private void UpdatePhoto(float timeReset)
    {
        if (switchTimer > 0.0f)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            // Code to reset photo here based on photo type
            switch(currentPicture)
            {
                case PictureType.encouraging:

                    // Code for displaying encouraging pictures
                    currentImage = encouragingPictures[currentPictureIndex];
                    pictureCount = encouragingPictures.Count;

                    if (currentPictureIndex < pictureCount)
                    {
                        currentPictureIndex++;
                    }
                    else
                    {
                        currentPictureIndex = 0;
                    }

                    break;

                case PictureType.neutral:
                    // Code for displaying neutral pictures
                    break;
                case PictureType.calming:
                    // Code for displaying calming pictures
                    break;
                case PictureType.puppy:
                    // Code for displaying calming pictures
                    break;
            }

            switchTimer = timeReset;
        }
    }
}
