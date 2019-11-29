using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SuspectProject.Data;

/// <summary>
/// The class's role is creating GameDataManager
/// </summary>
public class GameBuilder : MonoBehaviour
{
    #region KEPT DATAS
    public static string displayName { get; set; }
    #endregion

    public GameObject prefabGameDataManager;

    private void Awake()
    {
        if (PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.Joined && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(prefabGameDataManager.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Instantiate(prefabGameDataManager);
        }

        Destroy(gameObject);
    }
}
