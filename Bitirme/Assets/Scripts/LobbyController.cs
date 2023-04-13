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

    public void UpdateLobbyName()
    {
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");

    }

    public void UpdatePlayerList()
    {

        if (!PlayerItemCreated)
        {
            CreateHostPlayerItem();
        }
        if (PlayerListItems.Count < Manager.GamePlayers.Count) { CreateClientPlayerItem(); }

        if(PlayerListItems.Count > Manager.GamePlayers.Count) { RemovePlayerItem(); }

        if(PlayerListItems.Count > Manager.GamePlayers.Count) { UpdatePlayerItem(); }

    }
    public void FindLocalPlayer() {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }
    public void CreateHostPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers) {

            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
<<<<<<< HEAD
            Debug.Log("createHostPlayerItem() called");
=======
            Debug.Log(NewPlayerItem.transform.position);
            Debug.Log("ANASKM");
>>>>>>> parent of e106c42 (Debug Deleted)
            
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
            if (!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                //GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab, Vector3.zero, Quaternion.identity, LobbyInfoCanvas.transform) as GameObject;
                NewPlayerItem.transform.localPosition = Vector3.zero;
                NewPlayerItem.transform.localRotation = Quaternion.identity;
                Debug.Log(NewPlayerItem.transform.localPosition);
                Debug.Log("ANASKM");
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


}
