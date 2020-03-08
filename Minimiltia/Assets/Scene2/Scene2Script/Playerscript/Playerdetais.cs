using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Playerdetais : MonoBehaviourPunCallbacks,IPunObservable
{
    #region Variables
    [Header("Aboutplayer")]
    public string playername;
    public GameObject canvasobject;
    public Vector3 smoothpos;
    public Quaternion smoothrot;
    public float smoothtime;
    private Controller controller;
    public bool isfirstupdated = false;
    private Vector3 canvasscale;
    
    [Header("About other objects")]
    public Gamemanager gamemanager;
    #endregion
    #region Builtinmethods
    void Start()
    {
        Photonsendrate();
        Getotherobjectsscripts();
        photonView.RPC("Playernamesync", RpcTarget.OthersBuffered, null);
     
        Addplayertolist();
       
    }

    void Update()
    {
       
        Playerpositionsync();
    }
    #endregion
    #region Custommethods
    void Getotherobjectsscripts()
    { 
        
        gamemanager = FindObjectOfType<Gamemanager>();
        controller = GetComponent<Controller>();
    }



    [PunRPC]
    void Playernamesync()

    {
        
            playername = photonView.Owner.NickName;
            this.name = playername;
        
    }
   
   void Photonsendrate()
    {
        PhotonNetwork.SerializationRate = 15;
        PhotonNetwork.SendRate = 20;
    }

   
    void Playerpositionsync()
    {
        Otherplayerpositionsync();
        Otherplayerrotationsync();
    }

    public void Addplayertolist()
    {
        if(gamemanager!=null)
        {
            gamemanager.playerlist.Add(this.gameObject);
        }
    }
    void Otherplayerpositionsync()
    {
       
        if (photonView.IsMine)
            return;



        var Lagdistance = smoothpos - transform.position;



         if (Lagdistance.magnitude>5f)
         {


              Lagdistance = smoothpos - transform.position;
                 Lagdistance = Vector3.zero;


         }
         if(Lagdistance.magnitude<0.11f)
         {
             print("Islagging");
             controller.inputs.Xval =0;
             controller.inputs.Yval = 0;

         }
         else
         {
             print("Islagging normalized");
             controller.inputs.Xval = Lagdistance.normalized.x;
            controller.inputs.Yval = Lagdistance.normalized.y;
         }
       

    }
    void Otherplayerrotationsync()
    {
        if (photonView.IsMine)
         return;
       // transform.rotation = smoothrot;
     
    }
  
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
           

             stream.SendNext(transform.position);
         





        }
        else if(stream.IsReading)
        {
           
            smoothpos = (Vector3)stream.ReceiveNext();
           
        }
    }
    #endregion



}
