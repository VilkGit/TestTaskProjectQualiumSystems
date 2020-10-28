using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    private bool buttonPause = false;
    GameObject panelControl = null;
    private void Start()
    {
        panelControl = GetComponentInChildren<PanelControl>()
            .gameObject;
    }

    public void Pause()
    {
        if(buttonPause)
        {
            Time.timeScale = 1;
            panelControl.SetActive(buttonPause);

        }
        else
        {
            panelControl.SetActive(buttonPause);
            Time.timeScale = 0;
        }

        buttonPause = !buttonPause;
    }

}
