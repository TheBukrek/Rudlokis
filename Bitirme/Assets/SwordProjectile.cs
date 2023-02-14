using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{   

    [Range(0,10)]
    public float speed = 0.3f;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(transform.position + direction);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}
