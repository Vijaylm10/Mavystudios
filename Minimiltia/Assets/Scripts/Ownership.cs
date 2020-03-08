using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Ownership : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
        print(PhotonNetwork.MasterClient.NickName+"Masterclient");
    }
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != base.photonView)
            return;
        base.photonView.TransferOwnership(requestingPlayer);
      
    }
  

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView != base.photonView)
            return;
        base.photonView.TransferOwnership(previousOwner);
    }

    private void OnMouseDown()
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
        base.photonView.RequestOwnership();
    }
}
