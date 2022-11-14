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
    {
        Normal,
        EnAtaque,
        Espera_regeneracion,
        SinEscudo
    }


    public Jugador_enum tipo_jugador;
    public Tipo_Cons_enum tipo_construccion;
    public Color_enum color;
    public Estado_Cons_enum Normal;

    [Header("Datos")]

    public float id;
    public float vida_base;
    public float vida;

    [Header("Conexiones")]
    public List<Building> caminos;

    public List<Building> get_caminos()
    {
        return this.caminos;
    }

    public float porc_vida()
    {
        if (this.vida_base > 0 && this.vida > 0)
        {
            return this.vida / this.vida_base;
        }
        else
        {
            return 0;
        }


    }

}
