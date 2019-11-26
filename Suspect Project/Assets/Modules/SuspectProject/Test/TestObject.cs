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
            displayName.text = GameBuilder.displayName;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GameBuilder.displayName);
        }

        if (stream.IsReading)
        {
            displayName.text = stream.ReceiveNext() as string;
        }
    }
}
