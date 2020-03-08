using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Playerlisting : MonoBehaviourPunCallbacks
{
    #region Variables
    public Transform content;
    public Playerdetail playerdetail;
    public bool isready = false;
    public Text readytext;
    public Button readybutton;
    public List<Playerdetail> playerlist = new List<Playerdetail>();
    #endregion

    #region Builtinmethods
    public override void OnEnable()
    {
        readybutton.GetComponent<Image>().color = Color.red;
        Getcurrentroomplayers();
        base.OnEnable();
    }
    public override void OnDisable()
    {
        for (int i = 0; i < playerlist.Count; i++)
        {
            Destroy(playerlist[i].gameObject);
        }
        playerlist.Clear();
        base.OnDisable();
    }
    public override void OnLeftRoom()
    {
        content.Destroychildren();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Entered");
        Addplayertolist(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("Left");
        int index = playerlist.FindIndex(x => x.players.NickName == otherPlayer.NickName);
        if(index!=-1)
        {
            Destroy(playerlist[index].gameObject);
            playerlist.RemoveAt(index);

        }
    }
    #endregion

    #region Custommethods
   public void Getcurrentroomplayers()
    {
        foreach(KeyValuePair<int,Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            Addplayertolist(playerinfo.Value);
        }
    }
    public void Addplayertolist(Player player)
    {
        int index = playerlist.FindIndex(x => x.players == player);
        if (index == -1)
        {
            Playerdetail playerobj = Instantiate(playerdetail, content);
            if (playerobj != null)
            {
                playerobj.SetPlayerinfo(player);
                playerlist.Add(playerobj);
              
            }
        }
        else
        {
            playerlist[index].SetPlayerinfo(player);
            print(playerlist[index].players.NickName);
        }
       
    }

    public void OnclickLeaveroom()
    {
        PhotonNetwork.LeaveRoom(true);
        Connection.connection.roomCanvases.SetActive(true);
        Connection.connection.playercanvases.SetActive(false);
    }

    public void Setreadytext(bool ready)
    {
        isready = ready;
        if(ready)
        {
            readytext.text = "R";
            readybutton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            readytext.text = "N";
            readybutton.GetComponent<Image>().color = Color.red;
        }
    }
    public void Changescene()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < playerlist.Count; i++)
            {
                if(playerlist[i].players!=PhotonNetwork.LocalPlayer)
                {
                    if (!playerlist[i].isplayerready)
                        return;
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }



    public void Onclickreadybutton()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Setreadytext(!isready);
            print(isready);
            photonView.RPC("Changereadystate", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, isready);
        }
       
    }
    private void Update()
    {
        Namesync();
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < playerlist.Count; i++)
            {
                if (playerlist[i].players != PhotonNetwork.LocalPlayer)
                {

                    
                     
                            isready = playerlist[i].isplayerready;
                            if(isready)
                            {
                                readytext.text = "R";
                        readybutton.GetComponent<Image>().color = Color.green;

                    }
                            else
                            {
                                readytext.text = "N";
                        readybutton.GetComponent<Image>().color = Color.red;
                    }
                      
                        }
                    
                }
            
        }

       
    }

    [PunRPC]
    public void Changereadystate(Player player,bool ready)
    {
        int index = playerlist.FindIndex(x => x.players == player);
        if (index != -1)
            playerlist[index].isplayerready = ready;
    }

   
  
    public void Namesync()
    {
        for (int i = 0; i < playerlist.Count; i++)
        {
            if(playerlist[i].photonView.IsMine)
            {
                playerlist[i].username = Mastermanger._gamesettings._Nickname;
            }
           
        }
    }
    
    #endregion
}
