using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Watch : Unit
{
    public NavMeshAgent navMesh;

    private void Awake()
    {
        this.navMesh = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.estadoJugador = Estado_enum.EnEspera;
        //TODO:Agregar un animador
        //TODO:Agregar efectos sonidos
    }

    //Recibe informacion de como inicializar 
    public void Inicializa(List<Transform> puntos)
    {
        this.puntos_viaje = puntos;
        this.vida_base = 0;
        this.vida = 0;
        this.danio = 10;

        this.puntos_viaje.RemoveAt(0);
    }

    void Update()
    {


        if (this.puntos_viaje.Count > 0)
        {
            navMesh.SetDestination(this.puntos_viaje[0].position);
            if (!this.navMesh.pathPending)
            {
                if (this.navMesh.remainingDistance <= this.navMesh.stoppingDistance)
                {
                    this.puntos_viaje.RemoveAt(0);
                }

            }



        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("vigia colision con" + other.gameObject.name);
        Debug.Log("tag" + other.gameObject.tag);

    }
}
