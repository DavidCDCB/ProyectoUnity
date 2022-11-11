using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Header("Test")]

    public RectTransform visual_box;

    //--------------------ELIMINAR LUEGO------------------
    public List<RectTransform> Informacion;
    public List<RectTransform> minimenu;

    //Parte Grafica
    [SerializeField]
    RectTransform boxVisual;

    //Parte logica
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;


    List<GameObject> listaSeleccionados = new List<GameObject>();
    //-----------------------------------------------------


    //---------------ELIMINAR LUEGO---------------------

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //BORRAR LUEGO
        this.imprimeInfo();
        this.imprime_minimenu();

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
            foreach (GameObject soldado in this.listaSeleccionados)
            {
                float x=Input.mousePosition.x;
                float z=Input.mousePosition.y;

                soldado.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(-30,1,21));
                Debug.Log("Se envio a:");
                Debug.Log(new Vector3(x,0,z));
            }

        }
    }

    //---ELIMINAR LUEGO---
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

        /*print(selectionBox.xMin);
        print(selectionBox.xMax);
        print(selectionBox.yMin);
        print(selectionBox.yMax); */
    }

    void SelectUnits()
    {
        this.listaSeleccionados = new List<GameObject>();
        Debug.Log("datos");
        Debug.Log(this.selectionBox);

        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1())
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

    //---ELIMINAR LUEGO---



    //Minimapa
    void imprime_minimenu()
    {
        //Imprime soldados

        int offsetx = 280;
        int offsety = -20;

        int var = 0;
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1())
        {
            float x = soldado.transform.position.x + offsetx;
            float y = soldado.transform.position.z + offsety;

            Vector2 boxStart = new Vector2((x * 1) - 10, (y * 1) - 10);
            Vector2 boxEnd = new Vector2((x * 1) + 10, (y * 1) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            minimenu[var].localPosition = boxCenter;

            Vector3 boxSize = new Vector2(7, 7);
            minimenu[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime jugador

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_bases())
        {
            float x = soldado.transform.position.x + offsetx;
            float y = soldado.transform.position.z + offsety;

            Vector2 boxStart = new Vector2((x * 1) - 10, (y * 1) - 10);
            Vector2 boxEnd = new Vector2((x * 1) + 10, (y * 1) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            minimenu[var].localPosition = boxCenter;

            Vector3 boxSize = new Vector2(20, 20);
            minimenu[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador1())
        {
            float x = soldado.transform.position.x + offsetx;
            float y = soldado.transform.position.z + offsety;

            Vector2 boxStart = new Vector2((x * 1) - 10, (y * 1) - 10);
            Vector2 boxEnd = new Vector2((x * 1) + 10, (y * 1) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            minimenu[var].localPosition = boxCenter;


            Vector3 boxSize = new Vector2(20, 20);
            minimenu[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador2())
        {
            float x = soldado.transform.position.x + offsetx;
            float y = soldado.transform.position.z + offsety;

            Vector2 boxStart = new Vector2((x * 1) - 10, (y * 1) - 10);
            Vector2 boxEnd = new Vector2((x * 1) + 10, (y * 1) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            minimenu[var].localPosition = boxCenter;


            Vector3 boxSize = new Vector2(20, 20);
            minimenu[var].sizeDelta = boxSize;

            var++;
        }




    }


    //Dibuja torrres minimapa

    //Dibuja bases minimapa

    //Dibuja soldados minimapa

    //Dibuja personaje minimapa


    //Mapa
    void imprimeInfo()
    {
        //Imprime soldados
        int var = 0;
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_unidades_jugador1())
        {
            float x = soldado.transform.position.x;
            float y = soldado.transform.position.z;

            Vector2 boxStart = new Vector2((x * 2) - 10, (y * 2) - 10);
            Vector2 boxEnd = new Vector2((x * 2) + 10, (y * 2) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Informacion[var].localPosition = boxCenter;


            Vector3 boxSize = new Vector2(7, 7);
            Informacion[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime jugador

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_bases())
        {
            float x = soldado.transform.position.x;
            float y = soldado.transform.position.z;

            Vector2 boxStart = new Vector2((x * 2) - 10, (y * 2) - 10);
            Vector2 boxEnd = new Vector2((x * 2) + 10, (y * 2) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Informacion[var].localPosition = boxCenter;

            Vector3 boxSize = new Vector2(20, 20);
            Informacion[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador1())
        {
            float x = soldado.transform.position.x;
            float y = soldado.transform.position.z;

            Vector2 boxStart = new Vector2((x * 2) - 10, (y * 2) - 10);
            Vector2 boxEnd = new Vector2((x * 2) + 10, (y * 2) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Informacion[var].localPosition = boxCenter;



            Vector3 boxSize = new Vector2(20, 20);
            Informacion[var].sizeDelta = boxSize;

            var++;
        }

        //Imprime torres
        foreach (GameObject soldado in this.Manager_Game.GetComponent<Manager_Game>().get_torres_jugador2())
        {
            float x = soldado.transform.position.x;
            float y = soldado.transform.position.z;

            Vector2 boxStart = new Vector2((x * 2) - 10, (y * 2) - 10);
            Vector2 boxEnd = new Vector2((x * 2) + 10, (y * 2) + 10);

            Vector2 boxCenter = (boxStart + boxEnd) / 2;
            Informacion[var].localPosition = boxCenter;

            Vector3 boxSize = new Vector2(20, 20);
            Informacion[var].sizeDelta = boxSize;

            var++;
        }




    }


    //Dibuja torrres minimapa

    //Dibuja bases minimapa

    //Dibuja soldados minimapa

    //Dibuja personaje minimapa


}
