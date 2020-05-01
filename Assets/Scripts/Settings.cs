using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Rendering.PostProcessing;
public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 sOriginPos;
    Quaternion sOriginRot;

    Vector3 mcPos;
    Quaternion mcRot;

    void Start()
    {
        sOriginPos = GameObject.Find("AR Session Origin").transform.position;
        sOriginRot = GameObject.Find("AR Session Origin").transform.rotation;

        mcPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        mcRot = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation;
    }

    public void Recalibrate()
    {
        GameObject.Find("AR Session Origin").transform.position = sOriginPos;
        GameObject.Find("AR Session Origin").transform.rotation = sOriginRot;

        GameObject.FindGameObjectWithTag("MainCamera").transform.position = mcPos;
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = mcRot;
    }

    public void SetMaxTS(float val)
    {
        GameObject.Find("Manager").GetComponent<Manager>().maxTimescale = val;
        print("Setting max timescale: " + val);
    }

    public void SetSSScale(float val)
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Planet"))
        {
            GameObject.Find("AR Session Origin").transform.localScale = new Vector3(val, val, val);
        }
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

    // Update is called once per frame
    void Update()
    {
        GetComponent<CanvasGroup>().interactable = (GetComponent<CanvasGroup>().alpha == 1);
    }
}
