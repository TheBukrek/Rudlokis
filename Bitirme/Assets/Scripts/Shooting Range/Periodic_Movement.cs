using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Periodic_Movement : MonoBehaviour
{
    public float speed = 0.0f;
    float movement;
    bool active = false;
    float direction = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 3.0f)
        {
            direction = -1.0f;
        }
        else if (transform.position.z < -3.0f)
        {
            direction = 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (speed == 1.0f)
            {
                speed = 0.0f;
                active = false;
            }
            else
            {
                speed = 1.0f;
                active = true;
            }
        }
        if (active)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, speed*direction*Time.deltaTime + transform.position.z);
        }
    }
}
