using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{   

    [Range(0,10)]
    public Transform target;
    public float speed = 0.3f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        


        //transform.LookAt(target);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}
