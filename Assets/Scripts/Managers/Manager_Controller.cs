using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
    public GameObject menu;
    public bool menuActive = false;
    public Canvas canvas;
    public Camera myCam;

    private float mouse_x;
    private float mouse_y;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //metodo que actualiza posiciones del mouse
        this.pos_mouse();

        //metodo que revisa si se activa el menu
        this.activaMenu();

        //Se mira si se pueden seleccionar unidades
        if (this.menuActive == true)
        {
            seleccion_unidades();
        }




        


        //metodo para mover camara
        //
    }

    //------------Metodos------------

    //Activacion de menu con una tecla
    void activaMenu()
    {
        if (Input.GetKeyDown("q"))
        {
            menuActive = !menuActive;
        }

    }

    //
    void pos_mouse()
    {
        //Calculo de posicion del mouse
        float x = canvas.GetComponent<RectTransform>().position.x;
        float y = canvas.GetComponent<RectTransform>().position.y;
        float width = canvas.GetComponent<RectTransform>().rect.width;
        float height = canvas.GetComponent<RectTransform>().rect.height;

        float mouse_abs_x = Input.mousePosition.x - x;
        float mouse_abs_y = Input.mousePosition.y - y;

        float mouse_x = (mouse_abs_x / x) * width / 2;
        float mouse_y = (mouse_abs_y / y) * height / 2;

    }

    void seleccion_unidades()
    {

    }



    //Modo para hacer pruebas
    void modoDebug()
    {
        if (Input.GetKeyDown("x"))
        {
            Manager_Game.Instance.creaSoldado2();
        }

        if (Input.GetKeyDown("c"))
        {
            print("c key was pressed");
        }
    }



    //---------------OTROS---------------

    //Variables booleanas
    public bool isMenuActive()
    {
        return menuActive;
    }

    public Vector2 getInputMouse(){
        return Input.mousePosition;
    }





}
