using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopoutAnimator : MonoBehaviour
{
    public GameObject PanelMenu;



    public void ShowHideMenu()
    {
        Debug.Log("Button Pressed");
        if (PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Show");
                animator.SetBool("Show", !isOpen);
            }
        }
    }
}