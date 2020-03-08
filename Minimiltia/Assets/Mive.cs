using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Mive : MonoBehaviourPunCallbacks
{
    public Rigidbody rigidbody;
    public float movespeed;
   
   
    // Update is called once per frame
    void Update()
    {
      
            float X = Input.GetAxis("Horizontal");
            float Y = Input.GetAxis("Vertical");
            Vector3 force = new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical"));
            rigidbody.velocity = force;
        
    }
}
