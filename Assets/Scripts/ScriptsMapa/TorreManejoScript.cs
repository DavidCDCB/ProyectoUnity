using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreManejoScript : MonoBehaviour


{

    //Referencias a contadores, eliminar luego
    int num = 0;
    int contador = 0;


    //Lista de objetos
    public List<GameObject> lista;      //Lista de coches


    //Atributos para generacion de coches
    int direccion = 0;
    int posicion = 40;


    void Start()
    {
        this.num = Random.Range(100, 300);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.contador >= this.num)
        {
            Debug.Log("se creo una unidad");

            //Crea numero aleatorio para establecer una posicion en un radio
            creaCarro();



            this.contador = 0;

        }
        contador++;

    }


    void cambiaDireccion()
    {

        if (this.direccion == 1)
        {
            this.direccion = -1;
        }
        else
        {
            this.direccion = 1;
        }

    }

    void creaCarro()
    {
        if (lista.Count <= 5)
        {

            GameObject g = Instantiate(GameObject.Find("AutoTorre"), transform.position, Quaternion.identity);

            
            lista.Add(g);
        }


    }
}
