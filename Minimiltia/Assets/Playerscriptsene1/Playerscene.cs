using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscene : MonoBehaviour
{
    public float gravity;
    public float jumpvelocity;
    public float heighttojump;
    public float timetojump;
    public float Xspeed;
    private Controller controller;
    private Inputcontroller inputcontroller;
  
    public Vector3 velocity;
    public bool isleft;
    public float ping;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<Controller>();
        inputcontroller = GetComponent<Inputcontroller>();
        gravity =2* heighttojump / Mathf.Pow(timetojump, 2);
        controller.rb2d.gravityScale = gravity;
        jumpvelocity =Mathf.Abs( gravity )* timetojump;
    }

    // Update is called once per frame
    void Update()
    {
        
      
        if (inputcontroller.Yval != 0)
        {
            controller.rb2d.velocity = new Vector2(velocity.x, velocity.y);
        }
        else if(controller.iscollided)
        {
            controller.rb2d.velocity= new Vector2(velocity.x, controller.rb2d.velocity.y);
        }
        else
        {
            controller.rb2d.velocity = new Vector2(velocity.x, controller.rb2d.velocity.y);
        }
      
            velocity.x = inputcontroller.Xval * Xspeed;

            velocity.y = inputcontroller.Yval * jumpvelocity;




        






    }
}
