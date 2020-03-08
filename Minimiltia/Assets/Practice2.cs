using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice2 : MonoBehaviour
{
    public Vector3 velocity;
    public float gravity;
    Practice controller;


    private void Start()
    {
        controller = GetComponent<Practice>();
    }
    private void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * 10f;
        controller.Move(velocity*Time.deltaTime);
    }
}
