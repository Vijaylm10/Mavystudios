using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ishitted;
    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D;

        hit2D = Physics2D.BoxCast(transform.position, boxCollider.size, 0f, new Vector2(1, -1), 0.25f);

        if (hit2D)
        {
            ishitted = true;
        }
        else
        {
            ishitted = false;
        }




    }
    void Verticalcollison(ref Vector3 velocity)
    {
        float DirectionY = Mathf.Sign(velocity.y);
        float Raylength = Mathf.Abs(velocity.y) + 0.015f;
        RaycastHit2D hit2D;

        hit2D = Physics2D.BoxCast(transform.position, new Vector2(boxCollider.size.x,2f), 0f, new Vector2(1, DirectionY),Raylength,LayerMask.GetMask("Ground"));
        if(hit2D)
        {
            velocity.y = (hit2D.distance)*DirectionY;
            Raylength = hit2D.distance;
        }

    }


    void Horizontalcollision(ref Vector3 velocity)
    {
        float DirectionX = Mathf.Sign(velocity.x);
        float Raylength = Mathf.Abs(velocity.x) + 0.015f;
        RaycastHit2D hit2D;

        hit2D = Physics2D.BoxCast(transform.position, new Vector2(boxCollider.size.x+1, boxCollider.size.y), 0f, new Vector2(DirectionX,1), Raylength, LayerMask.GetMask("Ground"));
        if (hit2D)
        {
            print(hit2D.distance);
            velocity.x = (hit2D.distance) * DirectionX;
            Raylength = hit2D.distance;
        }

    }
    public void Move( Vector3 velocity)
    {
        Verticalcollison(ref velocity);


       Horizontalcollision(ref velocity);
        transform.Translate( velocity);
    }
}
