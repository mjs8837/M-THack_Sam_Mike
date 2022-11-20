using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject importNotifPanel;
    public GameObject unimportNotifPanel;
    public GameObject notifPanel;
    public GameObject notifArrow;


    public void OpenImportPanel()
    {
        if(importNotifPanel != null)
        {
            bool isActive = importNotifPanel.activeSelf;

            importNotifPanel.SetActive(!isActive);

            //Debug.Log("Running import Notif Panel Method");
            OpenNotifPanel();
            
        }
    }

    public void OpenUnImportPanel()
    {
        if (unimportNotifPanel != null)
        {
            bool isActive = unimportNotifPanel.activeSelf;

            unimportNotifPanel.SetActive(!isActive);

            OpenNotifPanel();
        }
    }

    public void OpenNotifPanel()
    {
        Debug.Log("Running Notif Panel Method");
        if (notifPanel != null)
        {
            bool isActive = notifPanel.activeSelf;

            notifPanel.SetActive(!isActive);
        }
    }

    public void NotifMenu()
    {
        if (notifArrow != null)
        {
            bool isActive = notifArrow.activeSelf;

            notifArrow.SetActive(!isActive);

            isActive = notifPanel.activeSelf;

            notifPanel.SetActive(!isActive);
        }
    }

    public void NotifMenuAnimation()
    {
        if (notifArrow != null)
        {
            bool isActive = notifArrow.activeSelf;

            notifArrow.SetActive(!isActive);

            Animator animator = notifPanel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Show");
                animator.SetBool("Show", !isOpen);
            }
        }
    }
    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

           if (craftButtonAnimator != null)
        {
            craftButtonAnimator.SetBool("show", false);
        }*/
}
