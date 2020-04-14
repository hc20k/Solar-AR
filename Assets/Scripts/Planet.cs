using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public string planetName;

    GameObject sun;
    List<GameObject> moonGameObjects;
    GameObject tooltip; //TODO: Implement

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        tooltip = new GameObject();
        tooltip.name = name+"-Canvas";
        tooltip.AddComponent<Canvas>();
        tooltip.transform.SetParent(transform);
        tooltip.GetComponent<Canvas>().

        moonGameObjects = new List<GameObject>();
        sun = GameObject.FindGameObjectWithTag("Sun");
            
        planetName = name;

        for (int i = 0; i < moons; i ++)
        {
            GameObject moon = Instantiate(moonPrefab,transform);
            moon.transform.localScale = transform.localScale * moonSizeRatio;
            moon.transform.Rotate(transform.position, Random.Range(0, 360));
            moon.transform.Translate(new Vector3(Random.Range(-transform.localScale.x*2, transform.localScale.x), Random.Range(-transform.localScale.y*2, transform.localScale.y), Random.Range(-transform.localScale.z*2, transform.localScale.z)));
            moonGameObjects.Add(moon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down, rotationMPH / 120 * Time.deltaTime);
        transform.RotateAround(sun.transform.position, Vector3.down, orbitVelocity * Time.deltaTime);

        if (atmosphere)
        {
            atmosphere.transform.Rotate(Vector3.down, rotationMPH / 60 * Time.deltaTime);
        }
    }
}
