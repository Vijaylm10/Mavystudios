using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Manage : MonoBehaviourPunCallbacks
{
    public GameObject ins;



  
    // Start is called before the first frame update

    private void Start()
        {
       
            PhotonNetwork.Instantiate(ins.name, new Vector3(0, 0, 0), Quaternion.identity);
       
          
        }
    

  
}
