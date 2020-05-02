using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Planet : MonoBehaviour
{
    [Header("Properties")]
    public float rotationMPH;
    public bool orbitsTheSun = true;
    public float orbitVelocity;
    public GameObject atmosphere;
    public float moonSizeRatio;
    public int moons;
    public GameObject moonPrefab;

    float moonDistanceRadius = 1.2f;


    [HideInInspector]
    public string planetName;
    Manager manager;
    GameObject sun;
    public List<GameObject> moonGameObjects;

    // ** tooltip vars **
    GameObject tooltip;
    GameObject label3DText;
    float transitionSpeed = 1.2f;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        planetName = name;

        if(tag == "Planet" || tag == "Sun")
        {
            label3DText = Instantiate(manager.planetLabelPrefab);
            label3DText.transform.position = transform.position + new Vector3(0, transform.localScale.x / 5, 0);
            label3DText.transform.SetParent(transform);
            label3DText.GetComponentInChildren<TextMeshProUGUI>().text = planetName;
            label3DText.name = planetName + "-Label";

            tooltip = Instantiate(manager.tooltipPrefab);
            tooltip.transform.position = transform.position + new Vector3(transform.localScale.x/5, transform.localScale.x / 5, transform.localScale.x / 5);
            tooltip.transform.SetParent(transform);

            tooltip.GetComponent<Tooltip>().SetPlanetName(planetName);
            tooltip.GetComponent<CanvasGroup>().alpha = 0;

            AnimationCurve widthCurve = new AnimationCurve();
            widthCurve.AddKey(new Keyframe(0, transform.localScale.x/10));
            widthCurve.AddKey(new Keyframe(0.5f, 0));

            GradientColorKey[] gckArray = { new GradientColorKey(Color.white, 0.4f), new GradientColorKey(Color.black, 0) };

            gameObject.AddComponent<TrailRenderer>();
            gameObject.GetComponent<TrailRenderer>().widthCurve = widthCurve;
            gameObject.GetComponent<TrailRenderer>().colorGradient.colorKeys = gckArray;

            Material whiteDiffuseMat = new Material(Shader.Find("Standard"));
            gameObject.GetComponent<TrailRenderer>().material = whiteDiffuseMat;

        }

        moonGameObjects = new List<GameObject>();

        if (moons > 0)
        {
            // Debug moon space
            Debug.DrawLine(-transform.localScale * moonDistanceRadius, transform.localScale * moonDistanceRadius, Color.red);
        }

        for (int i = 0; i < moons; i ++)
        {
            GameObject moon = Instantiate(moonPrefab,transform);
            moon.transform.localScale = transform.localScale * moonSizeRatio;
            moon.transform.Rotate(transform.position, Random.Range(0, 360));
            moon.transform.Translate(new Vector3(
                Random.Range(-transform.localScale.x* moonDistanceRadius, transform.localScale.x* moonDistanceRadius), 
                Random.Range(-transform.localScale.y* moonDistanceRadius, transform.localScale.y * moonDistanceRadius), 
                Random.Range(-transform.localScale.z* moonDistanceRadius, transform.localScale.z * moonDistanceRadius)));
            moon.GetComponent<MeshRenderer>().enabled = false;
            moonGameObjects.Add(moon);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Tap: " + this);

        if (transform.parent && transform.parent.name != "SolarSystem")
        {
            manager.Focus(transform.parent.gameObject);
        }
        else
        {
            manager.Focus(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sun = GameObject.FindGameObjectWithTag("Sun");
        manager = GameObject.Find("Manager").GetComponent<Manager>();

        transform.Rotate(Vector3.down, rotationMPH / 120 * Time.deltaTime);

        if (label3DText)
            label3DText.transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position);

        if (!manager.disableOrbit)
            transform.RotateAround(sun.transform.position, Vector3.down, orbitVelocity * Time.deltaTime);

        if (atmosphere)
        {
            atmosphere.transform.Rotate(Vector3.down, rotationMPH / 60 * Time.deltaTime);
        }

        // handle focus
        if ((tag == "Planet" || tag == "Sun") && tooltip != null)
        {
            if (manager.focusedPlanet == gameObject)
            {
                if (tooltip.GetComponent<CanvasGroup>().alpha <= 1)
                {
                    tooltip.GetComponent<CanvasGroup>().alpha += (transitionSpeed / Time.timeScale) * Time.deltaTime;
                }
            }
            else
            {
                if (tooltip.GetComponent<CanvasGroup>().alpha >= 0)
                {
                    tooltip.GetComponent<CanvasGroup>().alpha -= (transitionSpeed / Time.timeScale) * Time.deltaTime;
                }
            }
        }
    }
}
