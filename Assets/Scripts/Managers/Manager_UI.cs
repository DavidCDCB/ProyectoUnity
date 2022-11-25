using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager_UI : MonoBehaviour
{

    [Header("Managers")]
    public GameObject Manager_Controller;
    public GameObject Manager_Game;

    [Header("Canvas")]

    public GameObject canvas;
    public GameObject Menu_UI;
    public GameObject Mini_Map_UI;
    public GameObject Units_Info_UI;
    public GameObject Health_UI;
    public GameObject Icon_UI;

    public GameObject Base_health;

    public RectTransform seleccion_box;

    public Camera main_camera;


    [Header("Test")]
    public Image player1UI;
    public Image player2UI;




    //----------Para graficas del mapa
    public List<RectTransform> cuadrados_azules;
    public List<RectTransform> cuadrados_rojos;
    public List<RectTransform> cuadrados_normales;


    public int num_cuadrados_azules = 0;
    public int num_cuadrados_rojos = 0;
    public int num_cuadrados_normales = 0;


    public float offset_x_minimenu;
    public float offset_y_minimenu;
    public float offset_w_minimenu;
    public float offset_h_minimenu;

    public float offset_t_minimenu;


    public float offset_x_menu;
    public float offset_y_menu;
    public float offset_w_menu;
    public float offset_h_menu;

    public float offset_t_menu;


    //Parte Grafica
    [SerializeField]
    RectTransform boxVisual;
    //Parte logica
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;


    List<Unit> listaSeleccionados = new List<Unit>();

    public float xt;
    public float yt;

    public float zt;








    //-----------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {


        //Se rellenan los iconos

        for (int i = 0; i < 1000; i++)
        {
            GameObject imgA = Instantiate(GameObject.Find("Icon_Player1"), transform.position, transform.rotation, this.Icon_UI.transform);
            RectTransform imgA_a = imgA.GetComponent<RectTransform>();

            GameObject imgB = Instantiate(GameObject.Find("Icon_Player2"), transform.position, transform.rotation, this.Icon_UI.transform);
            RectTransform imgB_a = imgB.GetComponent<RectTransform>();

            GameObject imgC = Instantiate(GameObject.Find("Icon_Normal"), transform.position, transform.rotation, this.Icon_UI.transform);
            RectTransform imgC_a = imgC.GetComponent<RectTransform>();

            this.cuadrados_azules.Add(imgA_a);

            this.cuadrados_rojos.Add(imgB_a);

            this.cuadrados_normales.Add(imgC_a);
        }
    }

    void Update()
    {

    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Se rellena el HUD segun la vida que tengan los jugadores
        this.player1UI.fillAmount = this.Manager_Game.GetComponent<Manager_Game>().vida_jugador1T();
        this.player2UI.fillAmount = this.Manager_Game.GetComponent<Manager_Game>().vida_jugador2T();


        //Se posicionan los cuadrados segun las posiciones en el mapa

        this.num_cuadrados_azules = 0;
        this.num_cuadrados_rojos = 0;
        this.num_cuadrados_normales = 0;
        this.imprimeInfo();
        this.imprime_minimenu();

        imprime_bases_mapa();



    }

    //-------Sistema que determina las unidades seleccionadas-----
    void DrawVisual()
    {
        Vector2 boxStart = this.startPosition;
        Vector2 boxEnd = this.endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //Calculos para X
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = this.convierte_pos_mouse().x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = this.convierte_pos_mouse().x;
        }

        //Calculos para Y
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = this.convierte_pos_mouse().y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = this.convierte_pos_mouse().y;
        }

    }

    void SelectUnits()
    {
        this.listaSeleccionados = new List<Unit>();
        Debug.Log("Unidades seleccionadas");
        Debug.Log(this.selectionBox);

        foreach (Unit soldado in this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1())
        {
            float x = soldado.transform.position.x;
            float y = soldado.transform.position.z;

            Vector2 boxStart = new Vector2((x * 1) - 10 + 386, (y * 1) - 10 + 180);

            if (this.selectionBox.Contains(boxStart))
            {
                this.listaSeleccionados.Add(soldado);

            }

        }

    }


    //-------Funciones para el mapa


    //Canvas del minimenu
    void imprime_minimenu()
    {
        this.num_cuadrados_azules = 0;
        this.num_cuadrados_rojos = 0;
        this.num_cuadrados_normales = 0;

        //-----Impresion de soldados----

        //imprime soldados jugador 1
        this.imprime_soldados_mapa(0, 0, this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1(), 7);

        //imprime soladdos jugador 2
        this.imprime_soldados_mapa(1, 0, this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador2(), 7);


        //----Impresion de construcciones---

        //Imprime torres jugador 1
        this.imprime_bases_mapa(0, 0, this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador1(), 7);

        //Imprime torres jugador 2

        this.imprime_bases_mapa(1, 0, this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador2(), 7);

        //Imprime bases

        this.imprime_bases_mapa(2, 0, this.Manager_Game.GetComponent<Manager_Game>().get_bases(), 7);
    }


    //Canvas del menu
    void imprimeInfo()
    {

    }

    Vector2 convierte_pos_mouse()
    {
        //Calculo de posicion del mouse
        float x = this.canvas.GetComponent<RectTransform>().position.x;
        float y = this.canvas.GetComponent<RectTransform>().position.y;
        float width = this.canvas.GetComponent<RectTransform>().rect.width;
        float height = this.canvas.GetComponent<RectTransform>().rect.height;

        float mouse_abs_x = Input.mousePosition.x - x;
        float mouse_abs_y = Input.mousePosition.y - y;

        float mouse_x = (mouse_abs_x / x) * width / 2;
        float mouse_y = (mouse_abs_y / y) * height / 2;



        return new Vector2(mouse_x, mouse_y);

    }




    //0 para jugador 1, 1 para jugador 2,2 para normal
    //0 para minimapa, 1 para mapa
    void imprime_soldados_mapa(int tipo, int modo, List<Unit> lista_soldados, float tamanio)
    {

        float off_x = 0;
        float off_y = 0;
        float off_w = 0;
        float off_h = 0;

        //Se define el modo
        if (modo == 0)
        {
            off_x = this.offset_x_minimenu;
            off_y = this.offset_y_minimenu;
            off_w = this.offset_w_minimenu;
            off_h = this.offset_h_minimenu;
        }
        else
        {
            off_x = this.offset_x_menu;
            off_y = this.offset_y_menu;
            off_w = this.offset_w_menu;
            off_h = this.offset_h_menu;
        }



        foreach (Unit soldado in lista_soldados)
        {
            float x = (soldado.transform.position.x + off_x) / off_w;
            float y = (soldado.transform.position.z + off_y) / off_h;

            Vector2 boxStart = new Vector2(x - tamanio, y - tamanio);
            Vector2 boxEnd = new Vector2(x + tamanio, y + tamanio);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Vector3 boxSize = new Vector2(7, 7);

            if (tipo == 0)
            {
                this.cuadrados_azules[this.num_cuadrados_azules].localPosition = boxCenter;
                this.cuadrados_azules[this.num_cuadrados_azules].sizeDelta = boxSize;
                this.num_cuadrados_azules++;
            }
            else if (tipo == 1)
            {
                this.cuadrados_rojos[this.num_cuadrados_rojos].localPosition = boxCenter;
                this.cuadrados_rojos[this.num_cuadrados_rojos].sizeDelta = boxSize;
                this.num_cuadrados_rojos++;
            }
            else
            {
                this.cuadrados_normales[this.num_cuadrados_normales].localPosition = boxCenter;
                this.cuadrados_normales[this.num_cuadrados_normales].sizeDelta = boxSize;
                this.num_cuadrados_normales++;
            }

        }
    }

    void imprime_bases_mapa(int tipo, int modo, Building[] lista_construcciones, float tamanio)
    {
        float off_x = 0;
        float off_y = 0;
        float off_h = 0;
        float off_w = 0;

        //Se define el modo
        if (modo == 0)
        {
            off_x = this.offset_x_minimenu;
            off_y = this.offset_y_minimenu;
            off_h = this.offset_h_minimenu;
            off_w = this.offset_w_minimenu;
        }
        else
        {
            off_x = this.offset_x_menu;
            off_y = this.offset_y_menu;
            off_h = this.offset_h_menu;
            off_w = this.offset_w_menu;
        }



        foreach (Building construccion in lista_construcciones)
        {
            float x = (construccion.transform.position.x + off_x) / off_w;
            float y = (construccion.transform.position.z + off_y) / off_h;

            Vector2 boxStart = new Vector2(x - tamanio, y - tamanio);
            Vector2 boxEnd = new Vector2(x + tamanio, y + tamanio);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Vector3 boxSize = new Vector2(7, 7);

            if (tipo == 0)
            {
                this.cuadrados_azules[this.num_cuadrados_azules].localPosition = boxCenter;
                this.cuadrados_azules[this.num_cuadrados_azules].sizeDelta = boxSize;
                this.num_cuadrados_azules++;
            }
            else if (tipo == 1)
            {
                this.cuadrados_rojos[this.num_cuadrados_rojos].localPosition = boxCenter;
                this.cuadrados_rojos[this.num_cuadrados_rojos].sizeDelta = boxSize;
                this.num_cuadrados_rojos++;
            }
            else
            {
                this.cuadrados_normales[this.num_cuadrados_normales].localPosition = boxCenter;
                this.cuadrados_normales[this.num_cuadrados_normales].sizeDelta = boxSize;
                this.num_cuadrados_normales++;
            }

        }


    }


    void imprime_bases_mapa()
    {
        Building[] buildings = this.Manager_Game.GetComponent<Manager_Game>().get_bases();

        //Se cuentan las bases
        for (int i = 0; i < buildings.Length; i++)
        {
            Building b = buildings[i];
            GameObject bar = this.Base_health.transform.GetChild(i).gameObject;
            //Para cada barra

            bar.transform.position = main_camera.WorldToScreenPoint(b.transform.position+ new Vector3(-45f,10f,50f));

            //Se obtiene los valores
            Image barra_roja=bar.transform.GetChild(1).GetComponent<Image>();
            Image barra_azul=bar.transform.GetChild(2).GetComponent<Image>();

    

            float aliado=b.get_puntos_aliado();
            float enemigo=b.get_puntos_enemigo();

            barra_roja.fillAmount=aliado/500f;
            barra_azul.fillAmount=enemigo/500f;



        }



    }



}




