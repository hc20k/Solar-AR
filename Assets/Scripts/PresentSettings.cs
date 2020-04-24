using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentSettings : MonoBehaviour
{
    public Canvas settingsCanvas;
    bool settingsIsPresented = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas.enabled = true;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<Button>().onClick.AddListener(OpenSettings_);
    }

    void OpenSettings_()
    {
        if (!settingsIsPresented)
        {
            StartCoroutine("OpenSettings");
            settingsIsPresented = true;
        }
    }

    public void CloseSettings_()
    {
        if (settingsIsPresented)
        {
            StartCoroutine("CloseSettings");
            settingsIsPresented = false;
        }
    }

    IEnumerator OpenSettings()
    {
        print("Open settings");
        //GameObject.Find("Manager").GetComponent<Manager>().Unfocus();
        settingsCanvas.enabled = true;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 0;

        Time.timeScale = GameObject.Find("Manager").GetComponent<Manager>().maxTimescale;

        while (Time.timeScale > 0.01f)
        {
            transform.parent.GetComponent<CanvasGroup>().alpha -= 10 * Time.deltaTime;
            settingsCanvas.GetComponent<CanvasGroup>().alpha += 10 * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CloseSettings()
    {
        print("Closing settings");
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 1;

        Time.timeScale = GameObject.Find("Manager").GetComponent<Manager>().maxTimescale;
        transform.parent.GetComponent<CanvasGroup>().alpha = 1;
        settingsCanvas.GetComponent<CanvasGroup>().alpha = 0;
        settingsCanvas.enabled = false;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
