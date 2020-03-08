using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Controller : MonoBehaviourPunCallbacks
{
    public Collision2D collisionpublic;

    public Rigidbody2D rb2d;
    public float slopeangle;
    public bool iscollided;
    public float handangle;
    [SerializeField]
    private Inputcontroller inputcontroller;
    public List<Animatingsurface> animatingsurfaces = new List<Animatingsurface>();
    public Vector3 direction;
    public float DirectionY;
    public float DirectionX;
    public Vector3 vel;
    public bool iscollidedbelow, up, right, left;
    public Inputcontroller inputs
    {
        get
        {
            return inputcontroller;
        }
    }
    void Start()
    {
        inputcontroller = GetComponent<Inputcontroller>();
        rb2d = GetComponent<Rigidbody2D>();
        foreach (Animatingsurface animatingsurface in animatingsurfaces)
        {
            animatingsurface.Initialize(this);
        }
    }

    public void Move( Vector3 velocity)
    {
        
        
        transform.Translate(velocity,Space.World);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionpublic = collision;
        iscollided = true;
        Whichsidecollided();
      


    }
    void OnCollisionExit2D(Collision2D collison)
    {
        OnExitcollision();
        iscollided = false;
        Reset();
    }
   
   void Reset()
    {
        collisionpublic = null;
        DirectionX = 0;
        DirectionY = 0;

    }

    void Whichsidecollided()
    {
        if(Mathf.Abs( vel.x)>Mathf.Abs( vel.y))

        {
           
            DirectionX = Mathf.Sign(vel.x);
        }
        if (Mathf.Abs(vel.x) < Mathf.Abs(vel.y))
        {

            DirectionY = Mathf.Sign(vel.y);
        }
      
    }
   

    void Handrot()
    {
       
        foreach (Animatingsurface animatingsurface in animatingsurfaces)
        {
            animatingsurface.Whichpartofbody(inputcontroller);
            animatingsurface.Objecttorotate();
        }
    }


    void Playerrot()
    {
        if (!iscollidedbelow)
        {

            Vector3 euler = Vector3.forward * (Mathf.Sin(inputcontroller.Xval) * Mathf.Rad2Deg);
          //  transform.localRotation = Quaternion.Euler(euler);
        }
       

    }
    void Update()
    {
        vel = rb2d.velocity;
        
        Playerrot();
        
        Handrot();
        if (collisionpublic != null)
        {
            CollisionDetection();
        }
         if(!iscollided)
        {
            Reset();
        }
    }

    void CollisionDetection( )
    {

        float angle = 0;
         //   Vector2.Angle(transform.up, collisionpublic.contacts[0].normal);
        slopeangle = angle;
        slopeangle = Mathf.Round(slopeangle);


        iscollidedbelow = DirectionY == -1;
        up = DirectionY == 1;
        right = DirectionX == 1;
        left = DirectionX == -1;
       if(DirectionX==0)
        {
            right = left = false;
        }
       if(DirectionY==0)
        {
            up = iscollidedbelow = false;
        }
        
    }



    void OnExitcollision()
    {
        iscollidedbelow = up = right = left = false;
    }
}
