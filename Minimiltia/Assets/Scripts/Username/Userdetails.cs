using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class Userdetails : MonoBehaviourPunCallbacks
{
    #region Variables
    public Button userbutton;
    public InputField usernamefield;
    public Text usertext;
    public string username;
    public static Userdetails userdetails;
    #endregion

    #region BuiltinMethods
    void Start()
    {
        if (Connection.connection == null)
        {
            Connection.connection = FindObjectOfType<Connection>();
        }
        userbutton.onClick.AddListener(Oncilckconfirm);
    }

    void Update()
    {
       

        Userbuttonvisible();
    }
    #endregion

    #region CustomMethods

    void Oncilckconfirm()
    {


        username = usernamefield.text;
        Mastermanger._gamesettings._Nickname = username;
        PhotonNetwork.LocalPlayer.NickName = username;
        Connection.connection.roomCanvases.SetActive(true);
       



        Connection.connection.usercanvas.SetActive(false);
    }

    void Userbuttonvisible()
    {
        if(usertext.text.Length!=0)
        {
            userbutton.interactable = true;
        }
        else
        {
            userbutton.interactable = false;
        }

       
    }

 
    #endregion
}
