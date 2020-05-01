using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tooltipPrefab;
    public bool disableOrbit = true;
    public TextAsset planetInfoJSON;

    [HideInInspector]
    public GameObject focusedPlanet;
    bool isFocused;

    public float maxTimescale = 1;
    public float minTimescale = 0.3f;

    float timescaleTransitionModifier = 2f;

    void Start()
    {

    }

    public void Focus(GameObject planet)
    {
        print("Focused");
        isFocused = true;
        focusedPlanet = planet;
    }

    public void Unfocus()
    {
        print("Unfocused");
        isFocused = false;
        focusedPlanet = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isFocused && Time.timeScale <= maxTimescale)
        {
            Time.timeScale += timescaleTransitionModifier * Time.deltaTime;
        } else if(isFocused && Time.timeScale >= minTimescale)
        {
            Time.timeScale -= timescaleTransitionModifier * Time.deltaTime;
        }

        if (focusedPlanet)
        {
            foreach (GameObject inactivePlanet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                if (inactivePlanet != focusedPlanet)
                {
                    Color color = inactivePlanet.GetComponent<Renderer>().material.color;
                    color.a = 0.1f;
                    inactivePlanet.GetComponent<Renderer>().material.color = color;
                }
            }
        } else
        {
            foreach (GameObject inactivePlanet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                Color color = inactivePlanet.GetComponent<Renderer>().material.color;
                color.a = 1;
                inactivePlanet.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
