using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public enum Jugador_enum
    {
        Jugador,
        Oponente,
        Aliado,
        Ninguno
    }

        public enum Tipo_Cons_enum
    {
        Torre,
        Base
    }

        public enum Color_enum
    {
        Rojo,
        Azul,
        Verde, //Ordenar luego si se implementan aliados
        Amarillo  //Ordenar luego si se implementan aliados 
    }


    public enum Estado_Cons_enum
    {   Normal,
        EnAtaque,
        Espera_regeneracion,
        SinEscudo
    }


    public Jugador_enum tipo_jugador;
    public Tipo_Cons_enum tipo_construccion;
    public Color_enum color;
    public Estado_Cons_enum Normal;

    public float id;
    
    public float vida_base;

    public float vida;


}
