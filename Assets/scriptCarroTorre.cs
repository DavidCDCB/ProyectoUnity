using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCarroTorre : MonoBehaviour
{


    //Atributos de carro
    int direccion = 1;
    public float speed;
    private GameObject torre;
    private Transform transformPlayer;
    private Vector3 targetPosition;
    private bool sigue = false;

    //Estado(Cambiar a automata de estado finito luego)
    //Primer estado, movimiento fuera de la torre
    //Segundo estado, Movimiento de orden
    int estado = 0;
    int limiteContador = 0;
    int contador = 0;

    int limiteEstado0=0; //Limita el movimiento a la hora de salirr


    // Start is called before the first frame update
    void Start()
    {
this.speed = 40;

//---------------------------------Cambiar luego a un metodo de ordenamiento mejor--------------------
this.limiteEstado0= Random.Range(1, 50);

Debug.Log(limiteEstado0);


    }

    // Update is called once per frame
    void Update()
    
    {

        //----------------------Cambiar a movimiento en posicion especifica-------------

    
        //Estado 0
        if (estado == 0)
        {
            transform.position += new Vector3(1*direccion, 0, 0) * Time.deltaTime * speed;
            if (contador >= limiteEstado0)
            {
                this.estado = 1;
                contador = 0;
            }

        }

        //Estado 1
        else if (estado == 1)
        {
            transform.position += new Vector3(0, 0, 1)* Time.deltaTime  *speed;
            if (contador >= 50)
            {
                this.estado = 2;
                contador = 0;
            }
 
        }

        else if(estado==2){
            mueveCarro();
        }
contador++;
 }



        //Metodo que hace que un carro se mueva a una posicion si se selecciono
        void mueveCarro()
        {
            if (this.sigue)
            {
                followObject(targetPosition, transform);
            }

            if (Vector3.Distance(transform.position, this.targetPosition) < 1)
            {
                this.sigue = false;
            }


        }


        void followObject(Vector3 objectToFollow, Transform transformObject)
        {
            transformObject.position = Vector3.MoveTowards(
                transformObject.position,
                objectToFollow,
                Time.deltaTime * this.speed
            );
            transformObject.LookAt(objectToFollow);
        }

        void followStart(Vector3 position)
        {
            this.sigue = true;
            this.speed = 10;
            torre = GameObject.Find("t1Red");
            this.targetPosition = position;
        }
    
    void setDireccion(int direccion){
        this.direccion=0;
    }
}
