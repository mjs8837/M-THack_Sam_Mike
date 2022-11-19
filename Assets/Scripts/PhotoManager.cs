using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] List<Sprite> productivePictures;
    [SerializeField] List<Sprite> calmingPictures;
    [SerializeField] List<Sprite> puppyPictures;

    // Start is called before the first frame update
    void Start()
    {
        productivePictures = new List<Sprite>();
        calmingPictures = new List<Sprite>();
        puppyPictures = new List<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
