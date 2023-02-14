using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Rigidbody rd;
    Vector3 previousPosition;
    float velocity;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        velocity = ((transform.position - previousPosition).magnitude) / Time.deltaTime;
        previousPosition = transform.position;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (velocity >= 2f)
            {
                //Debug.Log("velocity:   " + map(velocity, 2, 10, 1, 2));
                EnemyScript enemy = collision.transform.GetComponent<EnemyScript>();

                enemy.Damage(map(velocity,2,10,1,2) * 30);
            }
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
