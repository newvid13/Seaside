using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Group : MonoBehaviour
{
    private bool isOn = false;
    private CanvasGroup myGroup;

    void Start()
    {
        myGroup = GetComponent<CanvasGroup>();
    }

    public void Stim()
    {
        isOn = !isOn;

        if(isOn)
        {
            myGroup.alpha = 1;
            myGroup.interactable = true;
        }
        else
        {
            myGroup.alpha = 0;
            myGroup.interactable = false;
        }
    }
}
