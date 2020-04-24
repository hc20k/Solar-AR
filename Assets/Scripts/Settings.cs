using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
