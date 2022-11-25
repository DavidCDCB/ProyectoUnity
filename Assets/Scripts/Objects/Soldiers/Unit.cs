using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

/*     Enum

    public enum Soldado_enum
    {
        Soldado_A,
        Soldado_B,
        Soldado_C  //TODO:Ver si se agregan otras
    } */


/*     public enum Color_enum
    {
        Rojo,
        Azul,
        Verde, //Ordenar luego si se implementan aliados
        Amarillo  //Ordenar luego si se implementan aliados 
    } */

    public enum Jugador_enum
    {
        Jugador,
        Oponente,
        Aliado,
        Ninguno
    }

    public enum Estado_enum
    {
        EnEspera, //El jugador espera a una accion
        EnCamino, //El jugador va a una direccion especifica
        Atacando, //El jugador ataca
        Muerto, //El jugador murio :c
    }

        [Header("Estados")]
        //Valores
/*         public Soldado_enum tipoSoldado;
        public Color_enum colorSoldado; */

        public Jugador_enum tipoJugador;
        public Estado_enum estadoJugador;

        [Header("Variables")]
        public float vida_base;
        public float vida;
        public int danio; //Cambiar luego si hay multiples ataques

        [Header("Datos AI")]
        protected List<Transform> puntos_viaje;

}
