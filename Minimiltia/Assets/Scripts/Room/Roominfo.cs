using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Roominfo :MonoBehaviourPunCallbacks
{

    public Text rooomname;
    public RoomInfo RoomInfos;
    public void SetRoomInfo(RoomInfo roominfo)
    {
        RoomInfos = roominfo;
        rooomname.text = roominfo.Name;

    }
    public void Onclickroombutton()
    {
        PhotonNetwork.JoinRoom(RoomInfos.Name);
    }

}
