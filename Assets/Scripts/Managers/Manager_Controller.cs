using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
    public GameObject menu;


    public bool menuActive=false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Borrar luego
        if (Input.GetKeyDown("x"))
        {
            Manager_Game.Instance.creaSoldado2();
        }

        if (Input.GetKeyDown("c"))
        {
            print("c key was pressed");
        }


        //Varianbles de teclado
        this.activaMenu();


    }

    //Teclado
    void activaMenu(){
        if (Input.GetKeyDown("q"))
        {
            menuActive=!menuActive;
        }

    }

    //¿¿¿???

    public bool isMenuActive(){
        return menuActive;
    }





}
