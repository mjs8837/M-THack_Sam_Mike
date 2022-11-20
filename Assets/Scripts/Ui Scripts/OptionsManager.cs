using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public GameObject mainOptions;
    public GameObject secondOptions;

    public void OptionMenu()
    {
        if (mainOptions != null)
        {
            bool isActive = mainOptions.activeSelf;

            mainOptions.SetActive(!isActive);

            isActive = secondOptions.activeSelf;

            secondOptions.SetActive(!isActive);
        }
    }
}
