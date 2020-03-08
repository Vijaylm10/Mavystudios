using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletscript : MonoBehaviour
{
    public bool Ishitsomething;
    public Vector2 startpos;
    public Vector3 velocity;
    public float magnitude;

    void Update()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
        magnitude = velocity.magnitude;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            Ishitsomething = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            Ishitsomething = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Ishitsomething = false;
        }
    }
}
