using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public enum Whichpart
{
    Hand,Head,Player,Leg,Playerinairrotaate
}
public class Animatingsurface : MonoBehaviourPunCallbacks,IPunObservable
{

    public Whichpart whichpart;
    public float wantedangle;
    public Transform objecttorotate;
    public Vector3 axis;
    public float currentangle;
    private Quaternion smoothrot;
    private Vector3 smoothpos;
    private Controller controller;
  

    public void Initialize(Controller controllercurrent)
    {
        controller = controllercurrent;
    }
   public void Objecttorotate()
    {
        if (objecttorotate != null)
        {
            Vector3 currentangle = axis * wantedangle;
            Quaternion lerprot = Quaternion.Euler(currentangle);
            // transform.localRotation = Quaternion.Lerp(transform.localRotation, lerprot, 10 * Time.deltaTime);
            if (photonView.IsMine)
            {
                objecttorotate.transform.localRotation = Quaternion.Lerp(objecttorotate.transform.localRotation, lerprot, 0.2f);
            }
            else
            {
                objecttorotate.transform.localRotation = lerprot;
            }
        }
        
    }

   void Update()
    {
        Syncmovements();
    }

    public void Whichpartofbody(Inputcontroller inputcontroller)
    {
      
        float rotateangle = 0;
        switch(whichpart)
        {
            
                case Whichpart.Hand:
                rotateangle = inputcontroller.Yrot*currentangle;
                break;
            case Whichpart.Player:
            
                rotateangle= (Mathf.Sign(inputcontroller.Xrot))==-1 ? -180 : 0;
                break;

            case Whichpart.Playerinairrotaate:

               
                rotateangle =Mathf.Sin( inputcontroller.Xval) * Mathf.Rad2Deg;
                break;
            case Whichpart.Head:
                rotateangle = Mathf.Sin(inputcontroller.Yrot) * Mathf.Rad2Deg;
                
                break;
            case Whichpart.Leg:
                rotateangle = Mathf.PerlinNoise(inputcontroller.Xrot, inputcontroller.Yrot);
                break;

        }
        wantedangle = rotateangle;
    }
    void Syncmovements()
    {
         if (photonView.IsMine)
              return;
        if (objecttorotate != null)
        {
            objecttorotate.rotation = smoothrot;
             
            
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       if(stream.IsWriting)
        {
         
            stream.SendNext(objecttorotate.rotation);
        }else if(stream.IsReading)
        {
          
            smoothrot =(Quaternion)stream.ReceiveNext();
           
        }
    }


}
