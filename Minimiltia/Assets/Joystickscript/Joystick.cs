using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Whichplatform
{
    PC, MOBILE
}
public enum Canreset
{
    RESET, NOTRESET
}

public class Joystick : MonoBehaviour
{
    #region Variables

    [Header("Other objects and scripts")]
    public RectTransform knob;
    private RectTransform outer;
    [SerializeField]
    private List<Touchlocation> touches = new List<Touchlocation>();

    [SerializeField]
    private int id;
    [Header("Joystick Data")]
    public Vector3 joystickdata;
    [SerializeField]
    private bool Istouching;
    private float maxknob;
    private bool canresetvalue;
    [SerializeField]
    public int index;


    [Header("Enums")]
    [SerializeField]
    private Whichplatform whichplatform;
    [SerializeField]
    private Canreset canreset;
    #endregion
    #region Hidingdatavariables
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
    public bool IsTouching
    {
        get
        {
            return Istouching;
        }
    }
    #endregion
    #region Builtinmethods
    void Update()
    {
        Platform();
        Switchplatform();





    }
    #endregion
    #region Custom methods
    #region PC
    void Istouchingknob()
    {
        if (Input.GetMouseButton(0))
        {
            outer = GetComponent<RectTransform>();
            Istouching = RectTransformUtility.RectangleContainsScreenPoint(knob, Input.mousePosition);
            maxknob = knob.rect.width;
        }
    }
    void Handlejoystick()
    {
        if (Istouching)
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
    #endregion
    #region Mobile
    void Mobiletouch()
    {


        outer = GetComponent<RectTransform>();
        maxknob = knob.rect.width;


    }

    void Platform()
    {
        switch (SystemInfo.deviceType)
        {

            case DeviceType.Handheld:
                whichplatform = Whichplatform.MOBILE;

                break;
            case DeviceType.Desktop:
               whichplatform = Whichplatform.PC;

                break;
            default:
                print("none");
                break;
        }
    }

    public void Switchplatform()
    {
        switch (whichplatform)


        {
            case Whichplatform.PC:
                if (!Istouching)
                {
                    Istouchingknob();
                }




                if (Input.GetMouseButton(0))
                {
                    Handlejoystick();
                }
                else
                {
                    Istouching = false;
                    Reset();
                }
                break;

            case Whichplatform.MOBILE:
                Mobiletouch();
                if (Istouching)
                {
                    Getwhichtouch();
                }
                else
                {
                    Reset();



                }
                Handletouches();
                break;


        }

        switch (canreset)
        {
            case Canreset.RESET:
                canresetvalue = true;
                break;
            case Canreset.NOTRESET:
                canresetvalue = false;
                break;
        }

    }



    void Getwhichtouch()
    {


        if (index > -1)
        {
            Vector2 wantedposition = Vector2.zero;
            if (index != -1)
            {
                index = touches.FindIndex(x => x.touchid == id);
                
                RectTransformUtility.ScreenPointToLocalPointInRectangle(outer, (touches[index].pos), null, out wantedposition);
            }
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
                    index = touches.FindIndex(x => x.touchid == id);
                    // Reset();
                    if (thistouch != null)
                    {
                        thistouch.istouchalive = false;
                        Istouching = touches[index].istouchalive;
                    }
               
                }

                

                if (!Istouching)
                {
                  

                    if (id == 0)
                    {
                        id = 0;
                    }
                    else
                    {
                        id -= 1;
                    }


                }
                if (thistouch != null)
                {
                    touches.RemoveAt(touches.IndexOf(thistouch));
                    print("Ended" + thistouch.touchid);
                }

            }
            else if (t.phase == TouchPhase.Moved)
            {
                Touchlocation thistouch = touches.Find(x => x.touchid == t.fingerId);
                if (thistouch != null)
                {
                    thistouch.pos = t.position;
                }


            }
            ++i;


        }


    }
    #endregion

    public void Reset()
    {
        if (canresetvalue)
        {
            joystickdata = Vector2.zero;
            knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition, Vector2.zero, 20 * Time.deltaTime);
            //  knob.anchoredPosition = Vector2.zero;
        }
    }
    #endregion
}
