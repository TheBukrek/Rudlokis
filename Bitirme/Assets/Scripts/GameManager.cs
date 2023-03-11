using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;


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
        if (SteamManager.Initialized)
        {
            SteamUserStats.RequestCurrentStats();
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
        SteamUserStats.GetStat("XP", out xp);
        SteamUserStats.GetStat("Level", out level);

        DontDestroyOnLoad(this.gameObject);
        CalculateRequiredXp();
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

    void OnApplicationQuit()
    {
        SteamUserStats.SetStat("Level", level);
        SteamUserStats.SetStat("XP", xp);
    }
}

