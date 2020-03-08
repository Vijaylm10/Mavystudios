using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Touchlocation 
{
    public int touchid;
    public Vector2 pos;
    public bool istouchalive;
  
    public Touchlocation(int newtouchid,Vector2 newpos,bool newistouchalive)
    {
        touchid = newtouchid;
        pos = newpos;
        istouchalive = newistouchalive;
        
    }
}
