using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    private void Awake()
    {
        this.estadoJugador = Estado_enum.EnEspera;
        //TODO:Agregar un animador
        //TODO:Agregar efectos sonidos
    }

    //Recibe informacion de como inicializar 
    public void Inicializa(int a)
    {
        this.vida_base=0;
        this.vida=0;
        this.danio=10;
    }




}
