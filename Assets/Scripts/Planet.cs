using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    float transitionSpeed = 1.2f;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        planetName = name;

        if(tag == "Planet" || tag == "Sun")
        {
            tooltip = Instantiate(manager.tooltipPrefab);
            tooltip.transform.position = transform.position + new Vector3(transform.localScale.x, 0, 0);
            tooltip.transform.SetParent(transform);

            tooltip.GetComponent<Tooltip>().SetPlanetName(planetName);
            tooltip.GetComponent<CanvasGroup>().alpha = 0;

            AnimationCurve widthCurve = new AnimationCurve();
            widthCurve.AddKey(new Keyframe(0, 0.2f));
            widthCurve.AddKey(new Keyframe(0.5f, 0));

            GradientColorKey[] gckArray = { new GradientColorKey(Color.white, 0.4f), new GradientColorKey(Color.white, 0) };

            gameObject.AddComponent<TrailRenderer>();
            gameObject.GetComponent<TrailRenderer>().widthCurve = widthCurve;
            gameObject.GetComponent<TrailRenderer>().colorGradient.colorKeys = gckArray;

            Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
            gameObject.GetComponent<TrailRenderer>().material = whiteDiffuseMat;
        }

        moonGameObjects = new List<GameObject>();
        sun = GameObject.FindGameObjectWithTag("Sun");

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

            moonGameObjects.Add(moon);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Tap: " + this);

        if (transform.parent)
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
        transform.Rotate(Vector3.down, rotationMPH / 120 * Time.deltaTime);

        if (!manager.disableOrbit)
            transform.RotateAround(sun.transform.position, Vector3.down, orbitVelocity * Time.deltaTime);

        if (atmosphere)
        {
            atmosphere.transform.Rotate(Vector3.down, rotationMPH / 60 * Time.deltaTime);
        }

        // handle focus
        if (tag == "Planet" || tag == "Sun")
        {
            if (manager.focusedPlanet == gameObject)
            {
                if (tooltip.GetComponent<CanvasGroup>().alpha <= 1)
                {
                    tooltip.GetComponent<CanvasGroup>().alpha += (transitionSpeed/Time.timeScale) * Time.deltaTime;
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
