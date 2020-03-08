using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationjoystick : MonoBehaviour
{
    public RectTransform knob;
    private RectTransform outer;
    

    public int id;
    public List<Touchlocation> touches = new List<Touchlocation>();

    public Vector3 joystickdata;
    public Vector3 JoystickData
    {
        get
        {
            return joystickdata;
        }
    }
    public bool Istouching;
    public float maxknob;
    public Camera cam;
    public bool idalreadyassigned;
    public float Verticalpos
    {
        get
        {
            return joystickdata.y;
        }
    }

    public float Horizontalpos
    {
        get
        {
            return joystickdata.x;
        }
    }
    public bool usemouse;

    void Istouchingknob()
    {
        outer = GetComponent<RectTransform>();
        if (Input.GetMouseButton(0) )
        {
            Istouching = RectTransformUtility.RectangleContainsScreenPoint(knob, Input.mousePosition);
        }
        maxknob = knob.rect.width;
    }
    void Mobiletouch()
    {
        
            outer = GetComponent<RectTransform>();
        //Istouching = RectTransformUtility.RectangleContainsScreenPoint(knob, Input.mousePosition);
            maxknob = knob.rect.width;
       
    }

  
    void Update()
    {
        /*  if (usemouse)
          {
              if (!Istouching)
              {
                  Istouchingknob();
              }
          }


          if (usemouse)
          {
              if (Input.GetMouseButton(0))
              {
                  Handlejoystick();
              }
              else
              {
                  Istouching = false;
                  Reset();
              }

          }
          else
          {*/
        Mobiletouch();
        if (Istouching)
        {
            Getwhichtouch();
        }
       
        Handletouches();

        //  }


    }
    void Getwhichtouch()
    {


        if (id != -1)
        {
            Vector2 wantedposition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(outer, Input.GetTouch(id).position, null, out wantedposition);
            knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition, wantedposition, 20 * Time.deltaTime);
            knob.anchoredPosition = Vector2.ClampMagnitude(knob.anchoredPosition, maxknob);
            float Xval = knob.anchoredPosition.x / (outer.rect.width * 0.5f);
            float Yval = knob.anchoredPosition.y / (outer.rect.height * 0.5f);

            joystickdata = new Vector2(Xval, Yval);
            joystickdata = Vector2.ClampMagnitude(joystickdata, 1f);
            joystickdata = Vector2.ClampMagnitude(joystickdata, 1f);
            print("Moved");
        }
        else
        {
            touches.Clear();
        }

    }
    void Handlejoystick()
    {
        if (Istouching && Input.mousePosition.x < Screen.width * 0.5f && Input.mousePosition.y < Screen.height * 0.5f)
        {
            Vector2 wantedposition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(outer, Input.mousePosition, null, out wantedposition);
            knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition, wantedposition, 20 * Time.deltaTime);
            knob.anchoredPosition = Vector2.ClampMagnitude(knob.anchoredPosition, maxknob);
            float Xval = knob.anchoredPosition.x / (outer.rect.width * 0.5f);
            float Yval = knob.anchoredPosition.y / (outer.rect.height * 0.5f);

            joystickdata = new Vector2(Xval, Yval);
            joystickdata = Vector2.ClampMagnitude(joystickdata, 1f);
            joystickdata = Vector2.ClampMagnitude(joystickdata, 1f);
        }

    }
    void Handletouches()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            print(i + "id");
            Touch t = Input.GetTouch(i);
            if (t.phase == TouchPhase.Began)
            {
                print("ISbegn");


                touches.Add(new Touchlocation(t.fingerId, t.position, true));
                Touchlocation thistouch = touches.Find(x => x.touchid == t.fingerId);
                if (!Istouching)
                {
                    id = thistouch.touchid;
                }
                if (!Istouching)
                {
                    Istouching = RectTransformUtility.RectangleContainsScreenPoint(knob, thistouch.pos);
                }



            }
            else if (t.phase == TouchPhase.Ended)
            {
                Touchlocation thistouch = touches.Find(x => x.touchid == t.fingerId);

                if (Istouching)
                {
                    thistouch.istouchalive = false;
                    Istouching = touches[id].istouchalive;

                }


                touches.RemoveAt(touches.IndexOf(thistouch));


                if (id == 0)
                {
                    id = 0;
                }
                else
                {
                    id -= 1;
                }

                print("Ended" + thistouch.touchid);

            }
            else if (t.phase == TouchPhase.Moved)
            {
                Touchlocation thistouch = touches.Find(x => x.touchid == t.fingerId);

                thistouch.pos = t.position;

                id = Input.touchCount - thistouch.touchid;
                //  thistouch.istouchalive = true;
                //Istouching = RectTransformUtility.RectangleContainsScreenPoint(knob, thistouch.pos);
            }
            ++i;


        }


    }
    public void Reset()
    {
        joystickdata = Vector2.zero;
        knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition,Vector2.zero, 20 * Time.deltaTime);
       
    }
    public Vector2 Screenpoint(Vector2 pos)
    {
        return cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, transform.position.z));
    }
}
