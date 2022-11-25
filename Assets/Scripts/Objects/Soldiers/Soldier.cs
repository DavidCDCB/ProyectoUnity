using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Unit
{

    public NavMeshAgent navMesh;

    public Base baseAtaque;
    public Tower torreAtaque;
    public Soldier Jugador_Ataque;

    public int contador;

    private void Awake()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.navMesh = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.estadoJugador = Estado_enum.EnEspera;
    }

    private void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    //Recibe informacion de como inicializar 
    public void Inicializa(int vida, float danio, string tipo)
    {
        this.vida_base = vida;
        this.vida = vida;
        this.danio = danio;

        if (tipo == "Jugador")
        {
            this.tipoJugador = Jugador_enum.Jugador;
        }
        else
        {
            this.tipoJugador = Jugador_enum.Oponente;
        }

        this.estadoJugador = Estado_enum.EnEspera;
    }


    public void Update()
    {
        //Comprueba si llego al lugar
        if (this.navMesh.remainingDistance <= this.navMesh.stoppingDistance)
        {
            this.estadoJugador = Estado_enum.EnEspera;
        }

        //Si esta atacando 
        if (this.estadoJugador == Estado_enum.Atacando)
        {
            ataque();
        }
        else if (this.estadoJugador == Estado_enum.EnCamino)
        {
            /*             Debug.Log("En camino"); */

        }

        if (this.vida <= 0)
        {
            Destroy(this);
        }



        this.contador++;
    }

    public void ataque()
    {
        //Prioridad a la base
        if (contador % 300 == 0 && this.baseAtaque != null)
        {
            if (!this.baseAtaque.tipo().Equals(this.tipo()))
            {
                this.baseAtaque.reduceVida(30, this.tipo());
            }
            else
            {
                this.estadoJugador = Estado_enum.EnEspera;
            }
        }
        //Segunda prioridad al soldado

    }

    public void envia_a_pos(Vector3 position)
    {
        navMesh.SetDestination(position);
        this.estadoJugador = Estado_enum.EnCamino;
    }


    void OnTriggerEnter(Collider other)
    {
        //Si colisiona con una base
        if (other.gameObject.tag == "Base")
        {
            Base b = other.GetComponent<Base>();



            if (!this.tipo().Equals(b.tipo()))
            {
                this.baseAtaque = b;
                this.estadoJugador = Estado_enum.Atacando;
            }
        }

        if (other.gameObject.tag == "Solider")
        {
            Soldier s = other.GetComponent<Soldier>();

            if (!this.tipo().Equals(s.tipo()))
            {
                Debug.Log("---info---");
                Debug.Log("esta atacando a"+other.gameObject.tag );

            }
        }


        //Si colisiona con un enemigo
    }


    public bool isEnEspera()
    {
        if (this.estadoJugador == Estado_enum.EnEspera)
        {
            return true;
        }
        return false;
    }

    public bool isEnCamino()
    {
        if (this.estadoJugador == Estado_enum.EnCamino)
        {
            return true;
        }
        return false;


    }

    public bool isAtaque()
    {
        if (this.estadoJugador == Estado_enum.Atacando)
        {
            return true;
        }
        return false;
    }

    public string tipo()
    {
        if (this.tipoJugador == Jugador_enum.Jugador)
        {
            return "Jugador";
        }
        else
        {
            return "Oponente";
        }
    }

}
