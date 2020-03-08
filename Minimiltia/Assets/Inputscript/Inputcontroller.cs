using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Inputcontroller : MonoBehaviourPunCallbacks
{
    public float Xval;
    public float Yval;
    public float Xrot;
    public float Yrot;
    
    public Joystick joystick;
    public Joystick rotationjoystick;
    public bool canshoot;
    public bool canmove;

    void Awake()
    {
        //if (photonView.IsMine)
        //{
            for (int i = 0; i < GameObject.Find("Main Camera").GetComponent<Camerafollow>().inputpanel.GetComponent<RectTransform>().childCount; i++)
            {
                if (GameObject.Find("Main Camera").GetComponent<Camerafollow>().inputpanel.GetComponent<RectTransform>().GetChild(i).tag == "Moveinput")
                {
                    joystick = GameObject.Find("Main Camera").GetComponent<Camerafollow>().inputpanel.GetComponent<RectTransform>().GetChild(i).GetComponent<Joystick>();
                }
                if (GameObject.Find("Main Camera").GetComponent<Camerafollow>().inputpanel.GetComponent<RectTransform>().GetChild(i).tag == "Rotationinput")
                {
                    rotationjoystick = GameObject.Find("Main Camera").GetComponent<Camerafollow>().inputpanel.GetComponent<RectTransform>().GetChild(i).GetComponent<Joystick>();
                }

            }
       // }
    }

   
    void Inputval()
    {
       /* Xval = Input.GetAxis("Horizontal");
        Yval = Input.GetAxis("Vertical");*/
        Xval = joystick.Horizontalpos;
        Yval = joystick.Verticalpos;
        Xrot = rotationjoystick.Horizontalpos;
        Yrot = rotationjoystick.Verticalpos;
    }

    void Update()
    {




        if (photonView.IsMine)
        {


            Inputval();
            if (rotationjoystick.joystickdata.magnitude > 0.8 && rotationjoystick.IsTouching)
            {
                canshoot = true;
            }
            else
            {
                canshoot = false;
            }

            if (joystick.IsTouching)
            {
                canmove = true;
            }
            else
            {
                canmove = false;
            }
        }
    }
}
