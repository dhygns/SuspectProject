using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkLobby : MonoBehaviourPunCallbacks
{
    public static NetworkLobby instance { get; private set; } = null;

    [Header("SERVER STATUS")]
    public TMP_Text networkStatus;

    [Header("PLAY BUTTON")]
    public Button joinButton;

    [Header("EXIT BUTTON")]
    public Button exitButton;

    private bool _isPlayerNameReady = false;
    private bool isPlayerNameReady
    {
        get => _isPlayerNameReady;
        set
        {
            _isPlayerNameReady = value;
            joinButton.interactable = _isServerReady && _isPlayerNameReady;
        }
    }

    private bool _isServerReady = false;
    private bool isServerReady
    {
        get => _isServerReady;
        set
        {
            _isServerReady = value;
            joinButton.interactable = _isServerReady && _isPlayerNameReady;
        }
    }

    private int _triedCount = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Connect
        SetNetworkStatus("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        SetNetworkStatus($"Connected!\n{PhotonNetwork.ServerAddress}");
        isServerReady = true;
        _triedCount = 0;
    }

    public void TryConnectToRandomRoom()
    {
        SetNetworkStatus("Joinning...");
        _triedCount = 0;
        joinButton.interactable = false;
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 10
        };

        PhotonNetwork.CreateRoom($"Room {randomRoomName}", roomOps);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        SetNetworkStatus($"Join Room Failed ({_triedCount}) - {message}");
        _triedCount++;
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        SetNetworkStatus($"Create Room Failed ({_triedCount}) - {message}");
        _triedCount++;
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        _triedCount = 0;
        SetNetworkStatus($"Room Joined\n{PhotonNetwork.CurrentRoom.Name}");
        SceneManager.LoadScene("GameScene");
    }

    public void OnPlayerNameChanged(string playerName)
    {
        UserInfoManager.SetPlayerName(playerName);
        isPlayerNameReady = !string.IsNullOrWhiteSpace(playerName);
    }

    public void SetNetworkStatus(string status)
    {
        networkStatus.text = status;
    }
}
