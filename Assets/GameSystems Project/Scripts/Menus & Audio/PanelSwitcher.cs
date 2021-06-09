using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void SetPanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
            panel.SetActive(true);
    } 
        
}
