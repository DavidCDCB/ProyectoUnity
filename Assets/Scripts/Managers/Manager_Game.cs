using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Game : MonoBehaviour
{

    //Instancia de control de juego
    private static Manager_Game _instance;
    public static Manager_Game Instance { get { return _instance; } }

    void Awake()
    {
        //Si una instancia existe y no es esta
        //Sino establece esta como la instancia
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public enum Estado_Juego
    {
        EnJuego,
        EnEspera
    }

    [Header("Estado_Juego")]

    [Header("Objetos")]

    public GameObject[] torres_Jugador1;
    public GameObject[] torres_Jugador2;
    public GameObject[] bases;

    public GameObject jugador1;
    public GameObject jugador2;

    private List<GameObject> unidades_jugador1 = new List<GameObject>();

    private List<GameObject> unidades_jugador2 = new List<GameObject>();


    [Header("Prefabs")]

    public GameObject gh;
    public GameObject soldadoA;
    public GameObject soldadoB;
    public GameObject soldadoC;
    public GameObject soldadoD;



    //-----------------------Metodos--------------------
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        muevepj();
    }

    public void creaSoldado1()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();

        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[0], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[1], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[2], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[3], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[4], Quaternion.identity));

    }

    public void creaSoldado2()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();

        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[1], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[2], Quaternion.identity));
        this.unidades_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[3], Quaternion.identity));

    }

    public List<GameObject> get_unidades_jugador1()
    {
        return this.unidades_jugador1;
    }

    public List<GameObject> get_unidades_jugador2()
    {
        return this.unidades_jugador2;
    }

    public GameObject[] get_bases()
    {
        return this.bases;
    }

    public GameObject[] get_torres_jugador1()
    {
        return this.torres_Jugador1;
    }

    public GameObject[] get_torres_jugador2()
    {
        return this.torres_Jugador2;
    }


    //-------------------------------BORRAR LUEGO NO TUVE TIEMPO PARA ORGANIZARLO---------------------
    void muevepj()
    {

        if (Input.GetKey(KeyCode.W))
        {
            this.jugador1.transform.Translate(new Vector3(0.1f, 0, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.jugador1.transform.Translate(new Vector3(-0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.jugador1.transform.Translate(new Vector3(0, 0, 0.1f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.jugador1.transform.Translate(new Vector3(0, 0, -0.1f));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            this.jugador1.transform.Rotate(0, 20, 0);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.jugador1.transform.Rotate(0, -20, 0);
        }






    }




}
