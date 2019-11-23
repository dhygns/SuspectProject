using Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestObject : MonoBehaviourPun, IPunObservable
{

    public TMP_Text displayName;

    public void Awake()
    {
        if (photonView.IsMine)
        {
            displayName.text = UserInfoManager.GetPlayerName();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(UserInfoManager.GetPlayerName());
        }

        if (stream.IsReading)
        {
            displayName.text = stream.ReceiveNext() as string;
        }
    }
}
