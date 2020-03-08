using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun data",menuName ="Gun")]
public class Gundata : ScriptableObject
{
    public string Gunname;
    public float range;
    public float nextbulletfirerate;
    public int currentammo;
    public int maxammo;
    public int damagetoobject;
    public float bulletspeed;
    public Sprite gunsprite;
    public Sprite bullets;
   
  void OnEnable()
    {
        bulletspeed = range / nextbulletfirerate;
    }
}
