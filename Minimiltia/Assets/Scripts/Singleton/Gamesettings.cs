using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Game",menuName ="Settings")]
public class Gamesettings : ScriptableObject
{
    [SerializeField]
    private string Gameversion;
    public string _GameVersion
    {
        get
        {
            return Gameversion;
        }
    }
    [SerializeField]
    private string Nickname;
    public string _Nickname
    {
        get
        {
           
            return Nickname;
        }
        set
        {
            Nickname = value;
        }
    }

    [SerializeField]
    private string Opponentname;
    public string _Opponentname
    {
        get
        {

            return Opponentname;
        }
        set
        {
            Opponentname = value;
        }
    }

    public bool Canjoinrandomroom;

}
