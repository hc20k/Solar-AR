using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlanet : MonoBehaviour
{
    Vector3 origPos;
    public bool goLeft;
    public float speedMultiplier = 1;
    public float rotationMultiplier = 1;
    public float xBoundary;

    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(((goLeft) ? 1 : -1) * speedMultiplier, 0, 0);
        transform.Rotate(Vector3.down, 90 * Time.deltaTime);

        if ((transform.position.x > xBoundary && goLeft) || (transform.position.x < xBoundary && !goLeft))
        {
            transform.position = origPos;
        }
    }
}
