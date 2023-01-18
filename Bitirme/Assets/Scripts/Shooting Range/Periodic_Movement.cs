using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Periodic_Movement : MonoBehaviour
{
    public float speed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, 7.0f) - 3.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, movement);
    }
}
