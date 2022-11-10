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

    private List<GameObject> unidadesAliado= new List<GameObject>();
    private List<GameObject> unidadesEnemigo= new List<GameObject>();



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
    }

    public void creaSoldado1()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();


        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoA,posiciones[0],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoA,posiciones[1],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoA,posiciones[2],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoA,posiciones[3],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoA,posiciones[4],Quaternion.identity)); 

    }

    public void creaSoldado2()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();

        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoB,posiciones[1],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoB,posiciones[2],Quaternion.identity));
        this.unidadesAliado.Add(UnityEngine.Object.Instantiate(this.soldadoB,posiciones[3],Quaternion.identity)); 

    }






}
