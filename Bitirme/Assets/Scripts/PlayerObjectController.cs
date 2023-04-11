using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerObjectController : NetworkBehaviour
{
    //Player Data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIDNumber;
    [SyncVar] public ulong PlayerSteamID;


    [SyncVar(hook = nameof(PlayerNameUpdate))]
    public string PlayerName;


    [SyncVar(hook = nameof(PlayerReadyUpdate))]
    public bool Ready;

    private CustomNetworkManager manager;

    private CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }
    public override void OnStartClient()
    {
        Debug.Log("Starting Client");
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }
    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string PlayerName) {
        this.PlayerNameUpdate(this.PlayerName, PlayerName);
    }


    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if (isServer) {
            this.PlayerName = NewValue;
        }
        if (isClient) {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    private void PlayerReadyUpdate(bool oldValue, bool newValue)
    {
        if (isServer)
        {
            this.Ready = newValue;
        }
        if (isClient)
        {
            LobbyController.Instance.UpdatePlayerList();

        }

    }

    [Command]
    private void CMdSetPlayerReady()
    {

        this.PlayerReadyUpdate(this.Ready,!this.Ready);
    }

    public void ChangeReady()
    {
        if (hasAuthority)
        {
            CMdSetPlayerReady();
        }
    }
}
