using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnEat : MonoBehaviour
{
    public float healAmount = 10f;

    //public HealthIndicator healthIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mouth"))
        {
            Debug.Log("yedin");
            //healthIndicator.Heal(healAmount);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Mouth"))
        {
            Debug.Log("yedin");
            //healthIndicator.Heal(healAmount);
        }
    }
}
