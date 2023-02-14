using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Rigidbody rd;
    Vector3 previousPosition;
    float velocity;

    public float swingAmountRequired = 1;
    public float swingAmountCurrent  = 0;
    public float swingAmountMinimumVelocity = 1;

    public Transform spawnPoint;
    public Vector3 midVector;
    public Vector3 midPoint;
    public Vector3 startPoint;
    public Vector3 swingStartVector;

    public GameObject projectilePrefab;



    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        velocity = ((transform.position - previousPosition).magnitude) / Time.deltaTime;
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {   
        
        velocity = ((transform.position - previousPosition).magnitude) / Time.deltaTime;
        previousPosition = transform.position;


        if (velocity >= swingAmountMinimumVelocity)
        {

            if (swingAmountCurrent == 0) {
                //Debug.Log("Swing Started");
                swingStartVector = spawnPoint.forward;

                startPoint = spawnPoint.position;

            }

            swingAmountCurrent += Time.deltaTime * velocity;

            if (swingAmountCurrent >= swingAmountRequired) {
                Debug.Log("Projectile throw");

                midVector = (spawnPoint.forward.normalized + swingStartVector.normalized).normalized;

                midPoint = (spawnPoint.position + startPoint)/2;
                swingAmountCurrent = 0f;

                GameObject newProjectile = Instantiate<GameObject>(projectilePrefab, midPoint, Quaternion.identity) ;
                newProjectile.GetComponent<SwordProjectile>().direction = midVector;  
                
            }

        }
        else {
            //Debug.Log("Swing cancelled");
            swingAmountCurrent = 0f;
        }
        
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
