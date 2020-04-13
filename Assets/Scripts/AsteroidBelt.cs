using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int asteroidCount;

    // Start is called before the first frame update
    void Start()
    {
        // torus ( R - √ x2 + y2 ) 2 + z2 = r2
        for (int i = 0; i < asteroidCount; i ++)
        {
            //GameObject asteroid = Instantiate(asteroidPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
