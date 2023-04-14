using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamVoiceChat : MonoBehaviour
{
    private UInt32 pcbCompressed;

    private UInt32 cbDestBufferSize = 32;

    private Byte[] pDestBuffer;
    // Start is called before the first frame update
    void Start()
    {
        SteamUser.StartVoiceRecording();
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamUser.GetAvailableVoice(out pcbCompressed) == EVoiceResult.k_EVoiceResultOK)
        {
            SteamUser.GetVoice(true, pDestBuffer,cbDestBufferSize, out pcbCompressed);
            //Fill this function.
            // SteamNetworkingMessages.SendMessageToUser();
        }
    }

    private void OnApplicationQuit()
    {
        SteamUser.StopVoiceRecording();
    }
}
