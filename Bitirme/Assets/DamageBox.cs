using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform location;
    public float damage;
    public HealthIndicator healthIndicator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(location.position, location.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("damage yedin");
            healthIndicator.TakeDamage(damage);
        }
    }
}
