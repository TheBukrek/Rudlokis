using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    public TextMeshProUGUI LobbyNameText;

    public GameObject LobbyInfoCanvas;

    //playerData
    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;
    //OtherData
    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;
    //ReadyData
    public Button StartGameButton;
    public TextMeshProUGUI ReadyButtonText;



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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        
    }
    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");

    }

    public void UpdatePlayerList()
    {
        if (!PlayerItemCreated)
        {
            Debug.Log("Create List");
            CreateHostPlayerItem();
        }
        if (PlayerListItems.Count < Manager.GamePlayers.Count) {
            Debug.Log("Create Client");
            CreateClientPlayerItem(); 
        }

        if(PlayerListItems.Count > Manager.GamePlayers.Count) {
            Debug.Log("Remove Client");
            RemovePlayerItem(); 
        }

        if(PlayerListItems.Count == Manager.GamePlayers.Count) {
            Debug.Log("Update player");
            UpdatePlayerItem(); 
        }

    }
    public void FindLocalPlayer() {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }
    public void CreateHostPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers) {

            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            
            
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            
            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);

        }
        PlayerItemCreated = true;

    }

    public void ReadyPlayer()
    {
        LocalPlayerController.ChangeReady();
    }

    public void UpdateButton()
    {
        if (LocalPlayerController.Ready)
        {
            ReadyButtonText.text = "Unready";
        }
        else
        {
            ReadyButtonText.text = "Ready";
        }
    }

    public void CheckIfAllReady()
    {
        bool AllReady = false;

        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            if (player.Ready)
            {
                AllReady = true;
            }
            else
            {
                AllReady = false;
                break;
            }
        }
        if (AllReady)
        {
            if(LocalPlayerController.PlayerIDNumber == 1)
            {
                StartGameButton.interactable = true;
            }
            else
            {
                StartGameButton.interactable = false;
            }
        }
        else
        {
            StartGameButton.interactable = false;
        }

    }

    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            Debug.Log("Player name: " + player.PlayerName);
            if (!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                //GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab, Vector3.zero, Quaternion.identity, LobbyInfoCanvas.transform) as GameObject;
                NewPlayerItem.transform.localPosition = Vector3.zero;
                NewPlayerItem.transform.localRotation = Quaternion.identity;
                Debug.Log(NewPlayerItem.transform.localPosition);
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.Ready = player.Ready;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem() {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            foreach (PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if (PlayerListItemScript.ConnectionID == player.ConnectionID) {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.Ready = player.Ready;
                    PlayerListItemScript.SetPlayerValues();
                    if(player == LocalPlayerController)
                    {
                        UpdateButton();

                    }
                }
            }
        }
        CheckIfAllReady();
    }

    public void RemovePlayerItem() {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();
        foreach (PlayerListItem playerListItem in PlayerListItems) {
            if (!Manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID)) {
                playerListItemToRemove.Add(playerListItem);
            }
        }
        if (playerListItemToRemove.Count > 0) {
            foreach (PlayerListItem playerlistItemToRemove in playerListItemToRemove) {
                GameObject ObjectToRemove = playerlistItemToRemove.gameObject;
                PlayerListItems.Remove(playerlistItemToRemove);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }

    public void StartGame(string SceneName) 
    {
        LocalPlayerController.CanStartGame(SceneName);
    }
}
