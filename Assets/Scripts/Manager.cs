using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sun;
    public GameObject[] planets;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject planetGO in planets)
        {
            Planet planet = planetGO.GetComponent<Planet>();

            if (planet.orbitsTheSun == true)
                planetGO.transform.RotateAround(sun.transform.position, Vector3.down, planet.orbitVelocity * Time.deltaTime);
        }
    }
}
