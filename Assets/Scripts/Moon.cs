using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    // Start is called before the first frame update
    public float orbitVelocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.parent.transform.position, Vector3.down, orbitVelocity / 60 * Time.deltaTime);
    }
}
