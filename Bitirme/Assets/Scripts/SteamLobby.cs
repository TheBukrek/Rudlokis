using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using Mirror;
using TMPro;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby Instance;
    
    //Callbacks
    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;
    
    //Variables
    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";
    private CustomNetworkManager manager;
    
    //Gameobject
    public GameObject HostButton;
    public TextMeshProUGUI LobbyNameText;

    private void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }

        if (Instance == null)
        {
            Instance = this;
        }

        manager = GetComponent<CustomNetworkManager>();

        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            HostLobby();
        }
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            return;
        }
        
        manager.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey,
            SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name",
            SteamFriends.GetPersonaName().ToString() + "'s lobby");
        
        Debug.Log("Lobby created successfully.");
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        Debug.Log("Request to join lobby");
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        //Everyone
        HostButton.SetActive(false);
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        LobbyNameText.gameObject.SetActive(true);
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name");
        
        //Clients
        if (NetworkServer.active)
        {
            return;
        }

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        manager.StartClient();
        Debug.Log("Lobby entered");
    }

    public void JoinLobby(CSteamID lobbyID)
    {
        SteamMatchmaking.JoinLobby(lobbyID);
    }
}
