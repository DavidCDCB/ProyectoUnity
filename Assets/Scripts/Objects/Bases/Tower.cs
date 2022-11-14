using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    float escudo_base = 0;
    float escudo = 0;




    void Awake()
    {
        //Agregar informacion de mesh
        //Agregar informacion de animacion
    }

    //Recibe informacion de como inicializar 
    public void Inicializa(int a)
    {
        this.vida_base = this.vida = 500;
        this.escudo_base = this.escudo = 500;
    }

    public Vector3[] devuelve_posiciones()
    {

        GameObject posiciones = this.transform.GetChild(1).gameObject;

        Vector3[] vector = new Vector3[posiciones.transform.childCount];
        for (int i = 0; i < posiciones.transform.childCount; i++)
        {
            vector[i] = posiciones.transform.GetChild(i).transform.position;
        }


        return vector;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("torre colisiono con" + other.gameObject.name);
        Debug.Log("tag" + other.gameObject.tag);

        if (other.gameObject.tag == "Watch")
        {

            this.vida = this.vida - 5;
        }
    }




}
