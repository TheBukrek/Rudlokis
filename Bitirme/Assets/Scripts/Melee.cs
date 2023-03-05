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


    public Transform spawnPoint;

    private Vector3 midPoint;
    private Vector3 startPoint;
    private Vector3 swingStartVector;

    private Vector3 startRotation;
    
    private Vector3 endRotation;

    private Quaternion midRotation;
    private bool midRotationAssigned;



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
                swingStartVector = spawnPoint.up;
                startRotation = spawnPoint.eulerAngles;
                quaternionList.Add(spawnPoint.rotation);

                startPoint = spawnPoint.position;

            }

            swingAmountCurrent += Time.deltaTime * velocity;

            if( swingAmountCurrent >= swingAmountRequired / 2 ){
                if( midRotationAssigned == false){
                    midRotation = spawnPoint.rotation;
                    midPoint = spawnPoint.position;
                    midRotationAssigned = true;
                    Debug.Log("midRotationAssigned    swing progress: %"+swingAmountCurrent/swingAmountRequired*100);
                }
               
                
            }else{
                midRotationAssigned = false;
            }


            if (swingAmountCurrent >= swingAmountRequired) {

                swingAmountCurrent = 0f;
                GameObject newProjectile = Instantiate<GameObject>(projectilePrefab, midPoint, midRotation) ;
                newProjectile.GetComponent<SwordProjectile>().speed = Math.Min(20, velocity * 20);

            }

        }
        else {
            //Debug.Log("Swing cancelled");
            swingAmountCurrent = 0f;
        }
        
    }


    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    

}
