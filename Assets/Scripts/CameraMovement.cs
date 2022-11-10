using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    private Vector3 direction; 
    private bool mover = false;
    // Start is called before the first frame update

    private float axisMouse;

    void Start()
    {
        this.speed = 5;
        this.axisMouse = Input.GetAxis("Mouse ScrollWheel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            direction = Vector3.left;
            mover = true;
        }else
        if (Input.GetKey(KeyCode.D)) {
            direction = Vector3.right;
            mover = true;
        }else
        if (Input.GetKey(KeyCode.W)) {
            direction = Vector3.forward;
            mover = true;
        }else
        if (Input.GetKey(KeyCode.S)) {
            direction = Vector3.back;
            mover = true;
        }else{
            mover = false;
        }

        if(mover){
            transform.position += direction * Time.deltaTime * speed;
        }
        
        this.axisMouse = Input.GetAxis("Mouse ScrollWheel");

        if (this.axisMouse > 0f) // axis positivo
        {
            transform.Translate(0, -5, 0, Space.World);
        }
        else if (this.axisMouse < 0f) // axis negativo
        {
            transform.Translate(0, 5, 0, Space.World);
        }
    }
}
