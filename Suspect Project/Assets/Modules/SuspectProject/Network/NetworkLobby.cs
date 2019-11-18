using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkLobby : MonoBehaviourPunCallbacks
{
    public static NetworkLobby instance { get; private set; } = null;

    public Button joinRoomButton;

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
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryConnectToRandomRoom()
    {
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

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        joinRoomButton.interactable = true;
        Debug.Log("Player has connected to the Photon master server");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError($"Tried to join a random game but failed. There must be no open games available {returnCode} / {message}");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Tried to create a new room but failed. There must be no open games available {returnCode} / {message}");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Player has connected to random room : {PhotonNetwork.CurrentRoom.Name}");
    }
}
