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
    AudioSource audioData;

    public Animator mAnimator;

    public int contador;

    private void Awake()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.navMesh = this.GetComponent<NavMeshAgent>();
        this.estadoJugador = Estado_enum.EnEspera;
    }

    private void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.audioData = GetComponent<AudioSource>();
    }

    //Recibe informacion de como inicializar 
    public void Inicializa(int vida, int danio, string tipo)
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
        if (!this.isMuerto())
        {
            metodos();
        }

    }

    void metodos()
    {
        //Comprueba si llego al lugar
        if (this.navMesh.remainingDistance <= this.navMesh.stoppingDistance)
        {
            if (!this.isAtaque())
            {
                this.setEnEspera();
            }
            this.navMesh.isStopped = true;

        }

        //Si esta atacando 
        if (this.estadoJugador == Estado_enum.Atacando)
        {
            this.navMesh.isStopped = true;
            ataque();
        }
        else if (this.estadoJugador == Estado_enum.EnCamino)
        {
            this.navMesh.isStopped = false;
        }
        else if (this.estadoJugador == Estado_enum.EnEspera)
        {
            this.navMesh.isStopped = true;
        }

        //Animador
        OrganizaAnimaciones();

        this.contador++;

    }


    void LateUpdate()
    {
        //Si se muere
        if (this.vida <= 0)
        {
            Destroy(gameObject, 0f);
            this.estadoJugador = Estado_enum.Muerto;
        }

    }

    public void ataque()
    {
        if (contador % 300 == 0)
        {
            if (this.Jugador_Ataque != null)
            {
                if (!this.Jugador_Ataque.tipo().Equals(this.tipo()))
                {
                    this.Jugador_Ataque.reduceVida(this.danio);
                    return;
                }
            }

            if (this.torreAtaque != null)
            {
                if (!this.torreAtaque.tipo().Equals(this.tipo()))
                {
                    this.torreAtaque.reduceVida(this.danio);
                    return;
                }
            }


            if (this.baseAtaque != null)
            {
                if (!this.baseAtaque.tipo().Equals(this.tipo()))
                {
                    this.baseAtaque.reduceVida(this.danio, this.tipo());
                    return;
                }
            }



            this.estadoJugador = Estado_enum.EnEspera;

        }

    }

    public void envia_a_pos(Vector3 position)
    {
        this.navMesh.isStopped = false;
        this.setEnCamino();
        navMesh.SetDestination(position);

    }


    void OnTriggerEnter(Collider other)
    {

        if (this.tipo().Equals("Jugador"))
        {
            Debug.Log(other.name);
        }


        //-------Si colisiona con una base--------
        if (other.gameObject.tag == "Base")
        {
            Base b = other.GetComponent<Base>();
            if (!this.tipo().Equals(b.tipo()))
            {
                this.baseAtaque = b;
                this.setAtaque();
            }
        }

        //-------Si colisiona con un soldado
        if (other.gameObject.tag == "Solider")
        {
            Soldier s = other.GetComponent<Soldier>();

            if (!this.tipo().Equals(s.tipo()))
            {
                Debug.Log("esta atacando soldado");
                this.Jugador_Ataque = s;
                this.setAtaque();

            }
        }

        //-------Si colisiona con un soldado
        if (other.gameObject.tag == "Tower")
        {
            Tower t = other.GetComponent<Tower>();

            if (!this.tipo().Equals(t.tipo()))
            {
                Debug.Log("esta atacando torre");
                this.torreAtaque = t;
                this.setAtaque();

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

    public bool isMuerto()
    {
        if (this.estadoJugador == Estado_enum.Muerto)
        {
            return true;
        }
        return false;
    }

    public void setEnEspera()
    {
        this.estadoJugador = Estado_enum.EnEspera;
        this.navMesh.isStopped = true;
    }

    public void setEnCamino()
    {
        this.navMesh.isStopped = false;

        this.estadoJugador = Estado_enum.EnCamino;
    }

    public void setAtaque()
    {
        if(this.gameObject.name.Contains("P1")){
            this.audioData.Play();
        }
        
        this.navMesh.velocity = Vector3.zero;
        this.estadoJugador = Estado_enum.Atacando;
        this.navMesh.isStopped = true;
    }

    public void setMuerte()
    {
        this.estadoJugador = Estado_enum.Muerto;
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


    public void OrganizaAnimaciones()
    {

        if (this.estadoJugador == Estado_enum.EnCamino)
        {
            this.mAnimator.SetTrigger("EnCamino");
            this.mAnimator.ResetTrigger("Atacando");
            this.mAnimator.ResetTrigger("EnEspera");
        }
        else if (this.estadoJugador == Estado_enum.Atacando)
        {
            this.mAnimator.ResetTrigger("EnCamino");
            this.mAnimator.ResetTrigger("EnEspera");
            this.mAnimator.SetTrigger("Atacando");
        }
        else if (this.estadoJugador == Estado_enum.Muerto)
        {
            this.mAnimator.SetTrigger("Muerto");
            this.mAnimator.ResetTrigger("Atacando");
            this.mAnimator.ResetTrigger("EnCamino");
            this.mAnimator.ResetTrigger("EnEspera");
        }
        else
        {
            this.mAnimator.SetTrigger("EnEspera");
            this.mAnimator.ResetTrigger("Atacando");
            this.mAnimator.ResetTrigger("EnCamino");
        }

    }

    public void reduceVida(int danio)
    {
        this.vida = this.vida - danio;
    }

}
