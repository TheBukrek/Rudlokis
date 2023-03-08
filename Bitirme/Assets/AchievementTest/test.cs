using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class test : MonoBehaviour
{
    public string id;

    private int ShotsFired;
    // Start is called before the first frame update
    void Start() {
        if(SteamManager.Initialized) {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            bool ach;
            Debug.Log(Steamworks.SteamUserStats.GetAchievement("FIRST_SHOT", out ach));
            Debug.Log("User has achievement " + id + ": " + ach);
        }
        if (Input.GetKeyDown("x"))
        {
            Debug.Log(Steamworks.SteamUserStats.SetAchievement(id));
            Debug.Log("Achievement " + id + " unlocked!");
            Debug.Log(SteamUserStats.StoreStats());
        }
        if (Input.GetKeyDown("c"))
        {
            bool ach;
            Debug.Log(Steamworks.SteamUserStats.ClearAchievement(id));
            Debug.Log("Achievement " + id + " cleared!");
        }
        if (Input.GetKeyDown("v"))
        {
            SteamUserStats.GetStat("Shots_Fired", out ShotsFired);
            ShotsFired++;
            SteamUserStats.SetStat("Shots_Fired", ShotsFired);
            SteamUserStats.StoreStats();
            Debug.Log(ShotsFired);
        }

        SteamAPI.RunCallbacks();
    }
}
