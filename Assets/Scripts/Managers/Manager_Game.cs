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

    public Building[] torres_Jugador1;
    public Building[] torres_Jugador2;
    public Building[] bases;

    public GameObject jugador1;
    public GameObject jugador2;

    private List<Unit> soldados_jugador1 = new List<Unit>();
    private List<Unit> vigias_jugador1 = new List<Unit>();

    private List<Unit> soldados_jugador2 = new List<Unit>();
    private List<Unit> vigias_jugador2 = new List<Unit>();


    [Header("Prefabs")]

    public GameObject vigia;

    public GameObject vigia_enemigo;
    public GameObject soldadoA;
    public GameObject soldadoB;
    public GameObject soldadoC;
    public GameObject soldadoD;


    [Header("Datos para AI")]
    public List<List<Building>> camino_J1aJ2;
    public List<List<Building>> camino_J2aJ1;

    public int tiempo_crea_vigia = 200;

    public int posTA = 0;

    public int posTB = 0;

    //-----------------------Metodos--------------------
    // Start is called before the first frame update
    void Start()
    {

        //Se crea la lista de caminos para el jugador 1
        camino_J1aJ2 = this.caminos_A_a_B(this.torres_Jugador1[0], this.torres_Jugador2[0]);
        camino_J2aJ1 = this.caminos_A_a_B(this.torres_Jugador2[0], this.torres_Jugador1[0]);

        /*//Se crea la lista de caminos para el jugador 2
                camino_J2aJ1 = this.caminos_A_a_B(this.torres_Jugador2[0], this.torres_Jugador1[0]);
         */
    }

    // Update is called once per frame
    void Update()
    {
        //Apenas se inicia se revisan que variables se eliminarion
        limpiaListas();

    }

    void FixedUpdate()
    {
        muevepj();
        creadores();
    }

    void creadores()
    {
        if (tiempo_crea_vigia <= 0)
        {
            StartCoroutine(crea_vigia());
            /*             StartCoroutine(crea_vigia_enemi()); */
            tiempo_crea_vigia = 2000;
        }


        tiempo_crea_vigia--;
    }


    IEnumerator crea_vigia()
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(1f);
            Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();

            GameObject _vigia = Instantiate(this.vigia, posiciones[2], Quaternion.identity);

            Watch u = _vigia.GetComponent<Watch>();

            this.vigias_jugador1.Add(u);

            Debug.Log("test");
            Debug.Log(this.posTA);
            Debug.Log(camino_J1aJ2.Count);

            List<Transform> puntos = new List<Transform>();
            //TODO:Cambiar a Transform
            foreach (Building b in camino_J1aJ2[this.posTA])
            {
                puntos.Add(b.transform);
            }
            u.Inicializa(puntos);


            if (this.camino_J1aJ2.Count > this.posTA + 1)
            {
                this.posTA++;
            }

            else
            {
                this.posTA = 0;
            }

        }



    }


    IEnumerator crea_vigia_enemi()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            Vector3[] posiciones = this.torres_Jugador2[0].GetComponent<Tower>().devuelve_posiciones();

            GameObject _vigia = Instantiate(this.vigia, posiciones[2], Quaternion.identity);

            Watch u = _vigia.GetComponent<Watch>();

            this.vigias_jugador2.Add(u);

            List<Transform> puntos = new List<Transform>();
            //TODO:Cambiar a Transform
            foreach (Building b in camino_J2aJ1[this.posTB])
            {
                puntos.Add(b.transform);
            }
            u.Inicializa(puntos);


            if (this.camino_J2aJ1.Count > this.posTB + 1)
            {
                this.posTB++;
            }

            else
            {
                this.posTB = 0;
            }

        }

    }


    public void creaSoldado1()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();

        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[0], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[1], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[2], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[3], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoA, posiciones[4], Quaternion.identity).GetComponent<Soldier>());

    }

    public void creaSoldado2()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[1], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[2], Quaternion.identity).GetComponent<Soldier>());
        this.soldados_jugador1.Add(UnityEngine.Object.Instantiate(this.soldadoB, posiciones[3], Quaternion.identity).GetComponent<Soldier>());

    }



    //Devuelve los caminos de una 
    public List<List<Building>> caminos_A_a_B(Building inicio, Building fin)
    {
        List<List<Building>> pila = new List<List<Building>>(); //Pila que conserva los caminos
        List<List<Building>> caminos = new List<List<Building>>(); //Pila que lee los caminos

        //Se crean temporales
        List<Building> list_temp = new List<Building>();
        list_temp.Add(inicio);
        pila.Add(list_temp);

        //valor de


        while (pila.Count > 0)
        {
            //Se elimina el primero de la lista
            List<Building> lb_temp = pila[0];
            pila.RemoveAt(0);


            //Se revisa para encontrar que mas caminos hay
            Building b_ultimo = lb_temp[lb_temp.Count - 1];

            foreach (Building construccion in b_ultimo.get_caminos())
            {
                if (!lb_temp.Contains(construccion))
                {
                    List<Building> nueva_lista = new List<Building>(lb_temp);
                    nueva_lista.Add(construccion);
                    pila.Add(nueva_lista);

                    Debug.Log(construccion);
                    Debug.Log(fin);

                    if (construccion.Equals(fin))
                    {
                        List<Building> camino_nuevo = new List<Building>(nueva_lista);
                        caminos.Add(camino_nuevo);
                    }
                }
            }

        }
        return caminos;
    }



    //-------------------------------BORRAR LUEGO NO TUVE TIEMPO PARA ORGANIZARLO---------------------

    public float vida_jugador1T()
    {
        return this.torres_Jugador1[0].porc_vida();
    }

    public float vida_jugador2T()
    {
        return this.torres_Jugador2[0].porc_vida();

    }



    public List<Unit> get_unidades_jugador1()
    {
        return this.soldados_jugador1;
    }

    public List<Unit> get_vigias_jugador1()
    {
        return this.vigias_jugador1;
    }

    public List<Unit> get_vigias_jugador2()
    {
        return this.vigias_jugador1;
    }

    public List<Unit> get_unidades_jugador2()
    {
        return this.soldados_jugador2;
    }

    public Building[] get_bases()
    {
        return this.bases;
    }

    public Building[] get_torres_jugador1()
    {
        return this.torres_Jugador1;
    }

    public Building[] get_torres_jugador2()
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

    private void limpiaListas()
    {

        for (int i = this.soldados_jugador1.Count - 1; i >= 0; i--)
        {
            if (this.soldados_jugador1[i] == null)
            {
                this.soldados_jugador1.RemoveAt(i);
            }

        }

        for (int i = this.vigias_jugador1.Count - 1; i >= 0; i--)
        {
            if (this.vigias_jugador1[i] == null)
            {
                this.vigias_jugador1.RemoveAt(i);
            }


        }

        for (int i = this.soldados_jugador2.Count - 1; i >= 0; i--)
        {
            if (this.soldados_jugador2[i] == null)
            {
                this.soldados_jugador2.RemoveAt(i);
            }


        }

        for (int i = this.vigias_jugador2.Count - 1; i >= 0; i--)
        {

            if (this.vigias_jugador2[i] == null)
            {
                this.vigias_jugador2.RemoveAt(i);
            }

        }



    }

}
