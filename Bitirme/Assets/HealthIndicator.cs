using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [Range(0, 100)]
    public float MAX_HEALTH = 100;
    public float health =0;
    [Range(0, 3f)]
    public float regen = 0.1f;

    public GameObject [] Segments = new GameObject[10];

    public void Heal(float amount)
    {
        health += amount;
        if(health > MAX_HEALTH)
        {
            Debug.Log("Health is full (overheal)");
            health = MAX_HEALTH;
        }
        
    }

    public void Regen(float deltaTime){
        health += regen*deltaTime;
        if(health > MAX_HEALTH)
        {
            Debug.Log("Health is full (overheal)");
            health = MAX_HEALTH;
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
    }

    private void UpdateHealth()
    {
        Debug.Log("Update Health: " + health);
        int segmentCount = Segments.Length;
        Debug.Log("Segment Count: " + segmentCount);
        float segmentHealth = MAX_HEALTH / segmentCount;
        Debug.Log("Segment Health: " + segmentHealth);

        for (int i = 0; i < transform.childCount; i++)
        {

            GameObject child = transform.GetChild(i).gameObject;
            if((i+1)*segmentHealth < health)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        health = MAX_HEALTH ;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        Regen(Time.deltaTime);
        UpdateHealth();
    }
}
