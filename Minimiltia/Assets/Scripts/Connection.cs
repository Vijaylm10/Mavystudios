using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Connection : MonoBehaviourPunCallbacks
{
    
    public GameObject roomCanvases;
    public bool isinroom;
    public bool isinplayerlobby;
    public GameObject playercanvases;
    public GameObject usercanvas;
    public static Connection connection;
    public Image infoimage;
    public List<Sprite> infoimages = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
      
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = Mastermanger._gamesettings._GameVersion;
        PhotonNetwork.NickName = Mastermanger._gamesettings._Nickname;
        print("Is connecting");
       
    }
    void Update()
    {
        if(playercanvases.activeInHierarchy)
        {
            isinplayerlobby = true;
            isinroom = false;
        }
        if(roomCanvases.activeInHierarchy)
        {
            isinroom = true;
            isinplayerlobby = false;
            
        }
        if (isinplayerlobby)
        {
            infoimage.sprite = infoimages[1];
        }
    }
    public override void OnConnectedToMaster()
    {
        print("Connected");
        if (!isinplayerlobby)
        {
            infoimage.sprite = infoimages[0];
        }
        if (!PhotonNetwork.InLobby)
        PhotonNetwork.JoinLobby();
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print(cause + "Is reason");
    }
}
