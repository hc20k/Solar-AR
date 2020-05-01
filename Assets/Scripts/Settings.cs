using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Rendering.PostProcessing;
public class Settings : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        ToggleMoons(false);
    }

    public void Recalibrate()
    {
        GameObject.Find("SolarSystem").transform.position = GameObject.Find("AR Session Origin").transform.position;
    }

    public void SetMaxTS(float val)
    {
        GameObject.Find("Manager").GetComponent<Manager>().maxTimescale = val;
        print("Setting max timescale: " + val);
    }

    public void SetSSScale(float val)
    {
        GameObject.Find("SolarSystem").transform.localScale = new Vector3(val, val, val);
    }

    public void SetGraphics(bool val)
    {
        // don't know why this shows as an error in vs. code works
        Camera.main.GetComponent<PostProcessLayer>().enabled = val;
    }

    public void ToggleMoons(bool val)
    {
        foreach(GameObject planets in GameObject.FindGameObjectsWithTag("Planet"))
        {
            foreach(GameObject moon in planets.GetComponent<Planet>().moonGameObjects)
            {
                moon.GetComponent<MeshRenderer>().enabled = !val;
            }
        }
    }

    public void ToggleSkybox(bool val)
    {
        GameObject.Find("AR Camera").GetComponent<UnityEngine.XR.ARFoundation.ARCameraBackground>().useCustomMaterial = val;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<CanvasGroup>().interactable = (GetComponent<CanvasGroup>().alpha == 1);
    }
}
