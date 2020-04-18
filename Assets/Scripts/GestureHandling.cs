using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleTap(Touch touch)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, touch.position, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Planet")
            {
                hit.transform.gameObject.GetComponent<Planet>().OnMouseDown();
            } else
            {
                GameObject.Find("Manager").GetComponent<Manager>().Unfocus();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject.Find("Manager").GetComponent<Manager>().Unfocus();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.tapCount == 1 && touch.phase == TouchPhase.Began)
            {
                // Singular tap
                HandleTap(touch);
            }
        }
    }
}
