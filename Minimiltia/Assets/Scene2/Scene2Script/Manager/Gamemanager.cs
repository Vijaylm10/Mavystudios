using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gamemanager : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Player information")]
    public GameObject playerprefab;
    public List<GameObject> playerlist = new List<GameObject>();

    [Header("Spawnposition")]
    public Vector2 spawnposition;
    public float Maxspawnrange;
    public float Minspawnrange;


    [Header("Camera values assign")]
    public Camerafollow camerafollow;
  
    #endregion
    #region Builtinmethods
    void Awake()
    {
        Getothercomponents();
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawnpositioncalculate();
        Spawnplayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion



    #region Custommethods
    void Getothercomponents()
    {
        camerafollow = GameObject.Find("Main Camera").GetComponent<Camerafollow>();
    }

    void Spawnpositioncalculate()
    {
        spawnposition = new Vector2(Random.Range(Minspawnrange, Maxspawnrange), 1f);
    }

    void Spawnplayers()
    {
        GameObject p = PhotonNetwork.Instantiate(playerprefab.name, spawnposition, Quaternion.identity);
        if (camerafollow != null)
        {
            camerafollow.target = p;
        }
    }


    
    #endregion
}
