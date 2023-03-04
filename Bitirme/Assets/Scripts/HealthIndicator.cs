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
            
            health = MAX_HEALTH;
        }
        
    }

    public void Regen(float deltaTime){
        health += regen*deltaTime;
        if(health > MAX_HEALTH)
        {
            
            health = MAX_HEALTH;
        }
        
    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    private void UpdateHealth()
    {
        
        int segmentCount = Segments.Length;
        
        float segmentHealth = MAX_HEALTH / segmentCount;
        

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
