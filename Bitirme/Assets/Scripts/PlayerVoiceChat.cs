using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerVoiceChat : NetworkBehaviour
{
    public AudioSource audioSource;

    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.T))
        {
            SteamUser.StartVoiceRecording();
            Debug.Log("Record Start");
        }
        else if (isLocalPlayer && Input.GetKeyUp(KeyCode.T))
        {
            SteamUser.StopVoiceRecording();
            Debug.Log("Record Stop");
        }

        if (isLocalPlayer)
        {
            uint compressed;
            EVoiceResult ret = SteamUser.GetAvailableVoice(out compressed);
            if (ret == EVoiceResult.k_EVoiceResultOK && compressed > 1024)
            {
                Debug.Log(compressed);
                byte[] destBuffer = new byte[1024];
                uint bytesWritten;
                ret = SteamUser.GetVoice(true, destBuffer, 1024, out bytesWritten);
                if (ret == EVoiceResult.k_EVoiceResultOK && bytesWritten > 0)
                {
                    Cmd_SendData(destBuffer, bytesWritten);
                }
            }
        }
    }

    [Command(channel = 2)]
    void Cmd_SendData(byte[] data, uint size)
    {
        Debug.Log("Command");
        PlayerVoiceChat[] players = FindObjectsOfType<PlayerVoiceChat>();

        for (int i = 0; i < players.Length; i++)
        {
            Target_PlaySound(players[i].GetComponent<NetworkIdentity>().connectionToClient, data, size);
        }
    }

    [TargetRpc(channel = 2)]
    void Target_PlaySound(NetworkConnection conn, byte[] destBuffer, uint bytesWritten)
    {
        Debug.Log("Target");
        byte[] destBuffer2 = new byte[22050 * 2];
        uint bytesWritten2;
        EVoiceResult ret = SteamUser.DecompressVoice(destBuffer, bytesWritten, destBuffer2, (uint)destBuffer2.Length, out bytesWritten2, 22050);
        if (ret == EVoiceResult.k_EVoiceResultOK && bytesWritten2 > 0)
        {
            audioSource.clip = AudioClip.Create(UnityEngine.Random.Range(100, 1000000).ToString(), 22050, 1, 22050, false);

            float[] test = new float[22050];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = (short)(destBuffer2[i * 2] | destBuffer2[i * 2 + 1] << 8) / 32768.0f;
            }
            audioSource.clip.SetData(test, 0);
            audioSource.Play();
        }
    }
}