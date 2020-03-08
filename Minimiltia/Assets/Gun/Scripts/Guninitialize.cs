using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guninitialize : MonoBehaviour
{
    #region Variables
    [Header("About guns")]
    public SpriteRenderer gunsprite;
    public List<Gundata> gundatas = new List<Gundata>();
    public Transform bulletpoint;
    public float nexttofire=0;
    private GameObject bullet;
    public GameObject bulletprefab;
    public int numberofbullets;
    public List<GameObject> bulletins = new List<GameObject>();
    public int currentindex;
    public int maxindex;
    public int previousindex;
    public int whichgun;
    public Vector2 bulletvelocity;

    [SerializeField]
    [Header("About Otherscripts")]
    private Inputcontroller inputcontroller;
    #endregion


    #region Builinmethods
    private void Start()
    {
        Bulletandgunspawn();
       
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Whichgun();
        }
        Shootdown();
        Bullethit();


    }
    #endregion

    #region Custommethods



    void Bulletandgunspawn()
    {
        gunsprite.sprite = gundatas[whichgun].gunsprite;
        for (int i = 0; i < numberofbullets; i++)
        {
            bullet = Instantiate(bulletprefab, bulletpoint.position, Quaternion.identity, bulletpoint);
            bulletins.Add(bullet);
            bullet.SetActive(false);
        }
    }

    void Shoot()
    {
        previousindex = currentindex;
        currentindex++;
       
        if (currentindex>maxindex)
        {
            currentindex = 0;
        }
        Bulletvisible();
        bulletins[currentindex].GetComponent<Bulletscript>().startpos = bulletpoint.position;
        bulletins[currentindex].GetComponent<SpriteRenderer>().sprite = gundatas[whichgun].bullets;
        bulletins[currentindex].transform.position = bulletpoint.position;
        bulletins[currentindex].GetComponent<Rigidbody2D>().velocity=transform.right*gundatas[whichgun].bulletspeed;
        bulletvelocity = bulletins[currentindex].GetComponent<Rigidbody2D>().velocity;
    }
    void Bulletvisible()
    {
        foreach (GameObject b in bulletins)
        {
            if (b.GetComponent<Rigidbody2D>().velocity.magnitude <= 0 && b.activeInHierarchy)
            {
                b.SetActive(false);

            }
            else
            {
                b.SetActive(true);
            }
        }
      
    }

    void Bullethit()
    {
        foreach (GameObject bullet in bulletins)
        {
            if(bullet.GetComponent<Bulletscript>().Ishitsomething)
            {
                bullet.SetActive(false);
            }
        }
    }

    public void Shootdown()
    {
        if(inputcontroller.canshoot&&Time.time>=nexttofire)
        {
            nexttofire = Time.time + 1 / gundatas[whichgun].nextbulletfirerate;
            print(nexttofire);
            Shoot();
        }
    }
    public void Whichgun()
    {
        
            whichgun++;
            if (whichgun >= gundatas.Count )
            {
                whichgun = 0;
            }
            gunsprite.sprite = gundatas[whichgun].gunsprite;
    }


    #endregion
}
