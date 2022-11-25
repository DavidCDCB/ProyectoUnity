using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    public Camera myCam;
    public LayerMask Clickable;
    public LayerMask Ground;

    public List<GameObject> unitsSelected = new List<GameObject>();

    public GameObject controlador_juego;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        seleccion();
        movimiento();
    }


    void seleccion()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable))
            {

                if (Input.GetKey(KeyCode.Q))
                {
                    Debug.Log(hit.collider.gameObject);
                    GameObject g = hit.collider.gameObject;
                    Unit u = g.GetComponent<Unit>();

                    this.controlador_juego.GetComponent<Manager_Game>().agregaSoldadoSeleccion(u);

                }

                else
                {
                    Debug.Log(hit.collider.gameObject);
                    GameObject g = hit.collider.gameObject;
                    Unit u = g.GetComponent<Unit>();

                    this.controlador_juego.GetComponent<Manager_Game>().deseleccionaSoldados();
                    this.controlador_juego.GetComponent<Manager_Game>().agregaSoldadoSeleccion(u);

                }


            }

            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Debug.Log("No selecciono nada");
                    this.controlador_juego.GetComponent<Manager_Game>().deseleccionaSoldados();
                }
            }



        }

    }

    void movimiento()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
            {
                this.controlador_juego.GetComponent<Manager_Game>().enviaSoldado(hit.point);
                Debug.Log(hit.point);
            }

        }


    }

}
