using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public GameObject target;
    public Vector2 boundsarea;
    public Vector2 Focusareasize;
    public float verticaloffset;
    [SerializeField]
    private  Focusarea focusarea;
    public float smoothtime;
    public GameObject inputpanel;
    private void Start()
    {
        if (target != null)
        {
            focusarea = new Focusarea(target.transform.position, Focusareasize);
        }
    }


    private void LateUpdate()
    {
        focusarea.Update(target.transform.position);
        Vector3 camposition = focusarea.centre + Vector2.up * verticaloffset;
        transform.position = Vector3.Lerp(transform.position,(camposition + Vector3.forward * -10),smoothtime* Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(focusarea.centre, Focusareasize);
    }
    [System.Serializable]
    struct Focusarea
  {
        public Vector2 centre;
       public float right, left;
       public float top, down;
        public Focusarea(Vector2  targetbounds, Vector2 size)
        {
            left = targetbounds.x - size.x / 2;
            right = targetbounds.y + size.x / 2;
            top = targetbounds.y + size.y / 2;
            down = targetbounds.y - size.y / 2;
            centre = new Vector2((left + right) / 2, (top + down) / 2);
        }

        public void Update(Vector2 targetbounds)
        {
            float ShiftX = 0;
            if(targetbounds.x<left)
            {
                ShiftX = targetbounds.x - left;
            }
            else
            {
                ShiftX = targetbounds.x - right;
            }
            left += ShiftX;
            right += ShiftX;

            float ShiftY = 0;
            if (targetbounds.y < down)
            {
                ShiftY = targetbounds.y - down;
            }
            else
            {
                ShiftY = targetbounds.y - top;
            }
            down += ShiftY;
            top += ShiftY;


            centre = new Vector2((left+right)/2,(top+down)/2);
        }
  }
  
}
