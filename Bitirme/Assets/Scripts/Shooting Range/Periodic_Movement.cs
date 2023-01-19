using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Periodic_Movement : MonoBehaviour
{
    public float speed = 1.0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = Mathf.PingPong(Time.time * speed, 6.0f) - 3f;
        transform.position = new Vector3(transform.position.x, transform.position.y, movement);
    }
}
