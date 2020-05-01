using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentSettings : MonoBehaviour
{
    public Canvas settingsCanvas;
    bool settingsIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas.enabled = true;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ToggleSettings()
    {
        if (settingsIsOpen == false)
        {
            OpenSettings();
            settingsIsOpen = true;
        } else
        {
            CloseSettings();
            settingsIsOpen = false;
        }
    }

    void OpenSettings()
    {
        print("Open settings");
        settingsCanvas.GetComponent<CanvasGroup>().interactable = true;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 1;

        transform.parent.GetComponent<CanvasGroup>().interactable = false;
        transform.parent.GetComponent<CanvasGroup>().alpha = 0;

        Time.timeScale = GameObject.Find("Manager").GetComponent<Manager>().maxTimescale;
    }

    void CloseSettings()
    {
        print("Closing settings");
        Time.timeScale = GameObject.Find("Manager").GetComponent<Manager>().maxTimescale;

        transform.parent.GetComponent<CanvasGroup>().interactable = true;
        transform.parent.GetComponent<CanvasGroup>().alpha = 1;

        settingsCanvas.GetComponent<CanvasGroup>().interactable = false;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
