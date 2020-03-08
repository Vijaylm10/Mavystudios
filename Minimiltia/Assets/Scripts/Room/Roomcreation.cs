using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Roomcreation : MonoBehaviourPunCallbacks
{

    public Text roomname;
    public Button roombutton;
    public Button joinrandom;
   
    void Start()
    {
        if(Connection.connection==null)
        {
            Connection.connection = FindObjectOfType<Connection>();
        }
        roombutton.onClick.AddListener(Onclickcreateroom);
        joinrandom.onClick.AddListener(Onclickjoinrandomroom);
      
    }

    private void Update()
    {
       
        Createroomvbuttonvisible();
    }
    public void Onclickcreateroom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        
            PhotonNetwork.JoinOrCreateRoom(roomname.text, options, TypedLobby.Default);
        

    }
    public void Onclickjoinrandomroom()
    {

        if (PhotonNetwork.CountOfRooms > 0)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
           
            PhotonNetwork.JoinRandomRoom();
           
        }
    }

    
    void Createroomvbuttonvisible()
    {
        if(roomname.text.Length>1)
        {
            roombutton.interactable = true;
        }
        else
        {
            roombutton.interactable = false;
        }
    }
    public override void OnJoinedRoom()
    {
        Mastermanger._gamesettings.Canjoinrandomroom = false;
        Connection.connection.playercanvases.SetActive(true);
        Connection.connection.roomCanvases.SetActive(false);

    }
    public override void OnCreatedRoom()
    {
        print("Room created successfully");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print(message + "is the reason");
    }
}
