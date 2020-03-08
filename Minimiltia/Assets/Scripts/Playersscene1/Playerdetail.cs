using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Playerdetail : MonoBehaviourPunCallbacks
{
    public Text playername;
    public Player players
    {
        get;private set;
    }
    public bool isplayerready;

    public string username;

    public void SetPlayerinfo(Player player)
    {
          if(Userdetails.userdetails==null)
          {
            Userdetails.userdetails = FindObjectOfType<Userdetails>();
          }
            players = player;
       
        playername.text = player.NickName;
        username = player.NickName;
        
       
            
        
        
       
           
        
    }

 
}
