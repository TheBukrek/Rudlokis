using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnerType { Uniform, Wave, Stabilizied   }
public class Spawner : MonoBehaviour {




    [Header("Spawner Configuration")]
    public SpawnerType spawnerType;
    
    public Transform spawnPoint;
    [Range(0,100)]
    public float spawnRadius;

    public float spawnMultiplier;
    public float UniformSpawnAmountPerSec;
    private float timeToSpawn = 0.1f;

    private float totalDeltaTime;
    
   
    
    
   
    [Header("Spawnee")]
    public GameObject prefab;
    private List<GameObject> pool;
    [Range(0, 1000)]
    public int poolVolume = 100;



    private void Awake()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolVolume; i++)
        {
            GameObject o = Instantiate(prefab, transform);
            o.transform.position = RandomPosition(spawnRadius);
            o.SetActive(false);
            pool.Add(o);

        }
    }

    public GameObject SpawnObject()
    {
        for (int i = 0; i < poolVolume; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
               
                return pool[i];
            }
        }

        return null;
    }

    private Vector3 RandomPosition(float radius) {
        
        float x = Random.Range(-radius, radius);
        float z = Random.Range(-radius, radius);
        float y = Random.Range(1f, 10f);

        Vector3 result = new Vector3(x, y, z);

        Debug.Log("RandomPosition: " + result);



        return result;
    }

    private void Start()
    {
        timeToSpawn = 1f / UniformSpawnAmountPerSec;
    }


    // Update is called once per frame
    void Update()
    {
        timeToSpawn = 1f / UniformSpawnAmountPerSec;
        totalDeltaTime += Time.deltaTime;

        if (true || spawnerType == SpawnerType.Uniform) {
            float spawnCountThisFrameFloat = totalDeltaTime / timeToSpawn;
            float remainderFloat = Mathf.Repeat(spawnCountThisFrameFloat, 1.0f);
            int spawnCountThisFrame = Mathf.FloorToInt(spawnCountThisFrameFloat);

            for (int i = 0; i < spawnCountThisFrame; i++)
            {
                GameObject newObject =  SpawnObject();
                if (newObject == null)
                {
                    Debug.Log("Pool is empty");
                }
                else { 
                    newObject.SetActive(true);
                
                }

                totalDeltaTime  -= timeToSpawn ;
            }

            
        }
        
        
    }
}
