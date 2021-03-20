using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float scroll;
    float moveHorizontal;
    float moveVertical;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.position = new Vector3(transform.position.x, transform.position.y - scroll, transform.position.z);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            moveVertical = Input.GetAxis("Vertical");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveVertical);
        }
    }
}
