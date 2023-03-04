using System;
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

    private GameObject[] multipleEnemy;

    public Transform spawnPoint;
    public Vector3 midVector;
    public Vector3 midPoint;
    public Vector3 startPoint;
    public Vector3 swingStartVector;

    public Vector3 startRotation;
    public Vector3 endRotation;


    public Vector3 closeEnemy;

    public GameObject projectilePrefab;


    public List<Quaternion> quaternionList = new List<Quaternion>();


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
                startRotation = spawnPoint.eulerAngles;
                quaternionList.Add(spawnPoint.rotation);

                startPoint = spawnPoint.position;

            }

            swingAmountCurrent += Time.deltaTime * velocity;

            if (swingAmountCurrent >= swingAmountRequired) {

                midVector = (spawnPoint.forward.normalized + swingStartVector.normalized).normalized;
                endRotation = spawnPoint.eulerAngles;
                float angle = Vector3.Angle(swingStartVector, spawnPoint.forward);
                //endRotation = new Vector3(endRotation.x - angle/2, endRotation.y, endRotation.z);
                midPoint = (spawnPoint.position + startPoint)/2;
                swingAmountCurrent = 0f;

                
                //Vector3 midRotation = new Vector3( (startRotation.x + endRotation.x)/2f, (startRotation.y + endRotation.y) / 2f, (startRotation.z + endRotation.z) /2f    );
                Quaternion midRotation = calcAvg(quaternionList);

                Vector3 proj = Vector3.Project(spawnPoint.forward, swingStartVector);
                
                
                



                GameObject newProjectile = Instantiate<GameObject>(projectilePrefab, midPoint, Quaternion.Euler(endRotation)) ;
                newProjectile.GetComponent<SwordProjectile>().speed = Math.Min(20, velocity * 20);



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
    public Transform getClosestEnemy()
    {
        multipleEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        float closestdis = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in multipleEnemy)
        {
            float currentDis;
            currentDis = Vector3.Distance(transform.position, go.transform.position);
            if (currentDis < closestdis)
            {
                closestdis = currentDis;
                trans = go.transform;
            }
        }
        return trans;

    }

    private Quaternion calcAvg(List<Quaternion> rotationlist)
    {
        if (rotationlist.Count == 0)
            throw new ArgumentException();

        float x = 0, y = 0, z = 0, w = 0;
        foreach (Quaternion q in rotationlist)
        {
            x += q.x; y += q.y; z += q.z; w += q.w;
        }
        float k = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
        return new Quaternion(x * k, y * k, z * k, w * k);
    }

}
