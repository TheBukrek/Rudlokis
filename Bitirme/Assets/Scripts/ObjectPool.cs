using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance;

    // Prefabs of the objects to be pooled
    [Header("Zombie Prefab")]
    [Tooltip("Change pool settings here")]
    public GameObject prefab;
    private List<GameObject> pool;
    [Range(0,1000)]
    public int poolVolume = 24;    
    
    [Header("Another Prefab")]
    [Tooltip("Change pool settings here")]
    public GameObject prefab2;
    private List<GameObject> pool2;
    [Range(0,1000)]
    public int poolVolume2 = 0; 

    




    void Awake(){
        if (instance == null){
            instance = this;
        }

        pool = new List<GameObject>();
        for(int i = 0; i < poolVolume; i++){
            GameObject o = Instantiate(prefab, transform);
            o.SetActive(false);
            pool.Add(o);
            
        }
    }

    public GameObject GetPooledZombie(){ 
        for(int i = 0; i < poolVolume; i++){
            if(! pool[i].activeInHierarchy){
                return pool[i];
            }
        }
        
        return null;
    }
}