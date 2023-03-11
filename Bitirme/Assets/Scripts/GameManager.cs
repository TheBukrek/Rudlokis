using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance{

        get{
            if (_instance == null) {
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }


    public int level;
    public float xp;

    public float requiredXpMultiplier;
    public float requiredXp;


    public float enemyXpReward1 = 180;

    public void AddXp(float xpAmount) {
        float newXp = xpAmount + xp;

        level = Mathf.FloorToInt (Mathf.Sqrt(newXp) * requiredXpMultiplier);
        xp = newXp;
        CalculateRequiredXp();

    }

    public float CalculateRequiredXp()
    {
        float result = Mathf.Pow((level + 1) / requiredXpMultiplier, 2);
        requiredXp = result;
        return requiredXp;
        
    }



    private void Update()
    {
    }
}
