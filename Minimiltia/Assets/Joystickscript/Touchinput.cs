using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchinput : MonoBehaviour
{
    public List<Touchlocation> touches = new List<Touchlocation>();
    public Joystick joystick;
    public Rotationjoystick rotationjoystick;

    void Start()
    {
        joystick = GetComponent<Joystick>();
        rotationjoystick = GetComponent<Rotationjoystick>();
    }


    // Update is called once per frame
    void Update()
    {
       

        int i = 0;
        while(i<Input.touchCount)
        {
            print(i + "id");
            Touch t = Input.GetTouch(i);
            if(t.phase==TouchPhase.Began)
            {
                print("ISbegn");
                
                touches.Add(new Touchlocation(t.fingerId,t.position,true));
            }else if(t.phase==TouchPhase.Ended)
            {
                Touchlocation thistouch = touches.Find(x => x.touchid ==t.fingerId);
                touches.RemoveAt(touches.IndexOf(thistouch));
                thistouch.istouchalive = false;
                print("Ended");
            }else if(t.phase==TouchPhase.Moved)
            {
                Touchlocation thistouch = touches.Find(x => x.touchid == t.fingerId);
                thistouch.pos = t.position;
                thistouch.istouchalive = true;
                print("Moved");
            }
            ++i;
        }
    }
}
