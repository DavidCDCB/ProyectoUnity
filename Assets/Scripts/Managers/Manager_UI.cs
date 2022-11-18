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

    public GameObject Menu_UI;
    public GameObject Mini_Map_UI;
    public GameObject Units_Info_UI;
    public GameObject Health_UI;
    public GameObject Time_UI;

    public GameObject Icon_UI;


    [Header("Test")]

    public RectTransform visual_box;

    public Image player1UI;
    public Image player2UI;




    //--------------------ELIMINAR LUEGO------------------
    public List<RectTransform> cuadrados_azules;
    public List<RectTransform> cuadrados_rojos;
    public List<RectTransform> cuadrados_normales;


    public int num_cuadrados_azules = 0;
    public int num_cuadrados_rojos = 0;
    public int num_cuadrados_normales = 0;


    public float offset_x_minimenu = 1;
    public float offset_y_minimenu = 1;
    public float offset_w_minimenu = 1;
    public float offset_h_minimenu = 1;

    public float offset_t_minimenu = 1;


    public float offset_x_menu = 1;
    public float offset_y_menu = 1;
    public float offset_w_menu = 1;
    public float offset_h_menu = 1;

    public float offset_t_menu = 1;


    //Parte Grafica
    [SerializeField]
    RectTransform boxVisual;

    //Parte logica
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;


    List<Unit> listaSeleccionados = new List<Unit>();
    //-----------------------------------------------------


    //---------------ELIMINAR LUEGO---------------------

    // Start is called before the first frame update
    void Start()
    {

        //Variables para el offset del menu
        offset_x_minimenu = 210;
        offset_y_minimenu = -40;
        offset_w_minimenu = 1;
        offset_h_minimenu = 1;
        offset_t_minimenu = 1;

        offset_x_menu = 200;
        offset_y_menu = -40;
        offset_w_menu = 0.6f;
        offset_h_menu = 1;
        offset_t_menu = 1;

        //Se rellenan los iconos

        for (int i = 0; i < 300; i++)
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
        //BORRAR LUEGO

        if (this.Manager_Game.GetComponent<Manager_Controller>().isMenuActive())
        {
            this.Menu_UI.GetComponent<CanvasGroup>().alpha = 0.0f;

            //En click
            if (Input.GetMouseButtonDown(0))
            {

                this.startPosition = Input.mousePosition;
                this.selectionBox = new Rect();
            }

            //Durante
            if (Input.GetMouseButton(0))
            {

                this.endPosition = Input.mousePosition;
                DrawVisual();
                DrawSelection();
            }

            //Al final
            if (Input.GetMouseButtonUp(0))
            {

                SelectUnits();
                this.startPosition = Vector2.zero;
                this.endPosition = Vector2.zero;
                DrawVisual();
                DrawSelection();

            }

        }
        //BORRAR LUEGO  
        if (this.Manager_Game.GetComponent<Manager_Controller>().isMenuActive())
        {
            this.Menu_UI.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            this.Menu_UI.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
        //BORRAR LUEGO

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Unit soldado in this.listaSeleccionados)
            {
                float x = Input.mousePosition.x;
                float z = Input.mousePosition.y;

                soldado.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(-30, 1, 21));
                Debug.Log("Se envio a:");
                Debug.Log(new Vector3(x, 0, z));
            }

        }
        //Agregar agarre de mouse
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
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //Calculos para Y
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
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
        this.imprime_soldados_mapa(0, 1, this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1(), 7);

        //imprime soladdos jugador 2
        this.imprime_soldados_mapa(1, 1, this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador2(), 7);

        //imprime vigias jugador 1
        this.imprime_soldados_mapa(2, 1, this.Manager_Game.GetComponent<Manager_Game>().get_vigias_jugador1(), 4);

        //imprime vigias jugador 2
        this.imprime_soldados_mapa(2, 1, this.Manager_Game.GetComponent<Manager_Game>().get_vigias_jugador2(), 4);

        //----Impresion de construcciones---

        //Imprime torres jugador 1
        this.imprime_bases_mapa(0, 1, this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador1(), 7);

        //Imprime torres jugador 2

        this.imprime_bases_mapa(1, 1, this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador2(), 7);

        //Imprime bases

        this.imprime_bases_mapa(2, 1, this.Manager_Game.GetComponent<Manager_Game>().get_bases(), 7);
    }


    //Canvas del menu
    void imprimeInfo()
    {




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
        if (modo == 1)
        {
            off_x = this.offset_x_menu;
            off_y = this.offset_y_menu;
            off_w = this.offset_w_menu;
            off_h = this.offset_h_menu;
        }
        else
        {
            off_x = this.offset_x_minimenu;
            off_y = this.offset_y_minimenu;
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
        if (modo == 1)
        {
            off_x = this.offset_x_menu;
            off_y = this.offset_y_menu;
            off_h = this.offset_h_menu;
            off_w = this.offset_w_menu;
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



}
