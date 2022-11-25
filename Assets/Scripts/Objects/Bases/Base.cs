using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        estado();
    }

    void OnTrigger(Collider other)
    {
        Debug.Log("-----------------");
        Debug.Log("Base colisiono con" + other.gameObject.name);
        Debug.Log("tag" + other.gameObject.tag);

    }


    public void reduceVida(int danio, string tipo)
    {
        if (tipo.Equals("Jugador"))
        {
            this.puntos_aliado = this.puntos_aliado + danio;
            this.puntos_enemigo = this.puntos_enemigo - danio;
        }
        else if (tipo.Equals("Oponente"))
        {
            this.puntos_aliado = this.puntos_aliado - danio;
            this.puntos_enemigo = this.puntos_enemigo + danio;
        }

    }

    public void estado()
    {
        //Para el aliado
        if (this.puntos_aliado > 500)
        {
            this.puntos_aliado = 500;
            this.puntos_enemigo = 0;
            this.tipo_jugador = Jugador_enum.Jugador;
        }
        else if (puntos_aliado <= 0)
        {
            this.puntos_aliado = 0;
        }

        //Para el enemigo
        if (puntos_enemigo > 500)
        {
            this.puntos_enemigo = 500;
            this.puntos_aliado = 0;
            this.tipo_jugador = Jugador_enum.Oponente;
        }
        else if (puntos_enemigo <= 0)
        {
            this.puntos_enemigo = 0;
        }

        //otros
    }


    public string tipo()
    {
        if (this.tipo_jugador == Jugador_enum.Jugador)
        {
            return "Jugador";
        }
        else if (this.tipo_jugador == Jugador_enum.Oponente)
        {
            return "Oponente";
        }
        else
        {
            return "Ninguno";
        }
    }








}
