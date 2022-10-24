using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    private Camera myCam;

    private float axisMouse;
    private float axis_X;
    private float axis_Y;

    // Start is called before the first frame update
    void Start()
    {
        this.myCam = Camera.main; //Llama la camara principal
    }

    // Update is called once per frame
    void Update()
    {

        controlAxis();

    }


    void controlAxis()
    {
        this.axisMouse = Input.GetAxis("Mouse ScrollWheel");


        movimiento_espacial();
        movimiento_zoom();

        //Movimiento del escenario
        void movimiento_espacial()
        {

            if (Input.GetMouseButton(2))
            {
                this.axis_X=Input.GetAxis("Mouse X");
                this.axis_Y=Input.GetAxis("Mouse Y");

                Debug.Log("Pressed middle click.");
                Debug.Log(this.axis_X);
                Debug.Log(this.axis_Y);

                
                this.myCam.transform.Translate(this.axis_X*10, 0, this.axis_Y*10, Space.World);


            }
        }

        //-------------Cambiar a un movimiento que pueda ser mas intuitivo-----------------------
        //zoom del escenario
        void movimiento_zoom()
        {
            this.axisMouse = Input.GetAxis("Mouse ScrollWheel");

            if (this.axisMouse > 0f) // axis positivo
            {
                this.myCam.transform.Translate(0, -5, 0, Space.World);
            }
            else if (this.axisMouse < 0f) // axis negativo
            {
                this.myCam.transform.Translate(0, 5, 0, Space.World);
            }

        }

        //
    }


}
