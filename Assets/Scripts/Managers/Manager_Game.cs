using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{

    //Instancia de control de juego
    private static Manager_Game _instance;
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

    public List<Unit> soldados_jugador1 = new List<Unit>();
    public List<Unit> soldados_jugador2 = new List<Unit>();
    public List<Unit> soldados_seleccionados = new List<Unit>();


    [Header("Prefabs")]

    public GameObject soldadoA;
    public GameObject soldadoB;

    public int contador;

    //-----------------------Metodos--------------------
    // Start is called before the first frame update
    void Start()
    {
    this.contador=600;
    }

    // Update is called once per frame
    void Update()
    {
        //Apenas se inicia se revisan que variables se eliminarion
        limpiaListas();
        Ai_enemigo();
        Visual_seleccion();
        contador++;

        //Crea los personajes del jugador cada tiempo
        if (contador % 700 == 0)
        {
            if (this.soldados_jugador1.Count < 50)
            {
                this.creaSoldadosJugador();
            }

        }

         //Crea los personajes del enemigo cada tiempo
        if (contador % 1000 == 0)
        {
        if (this.soldados_jugador2.Count < 50)
        {
            this.creaSoldadosEnemigo();
        }
        }

        //Revisa la salud de las torres
        if(this.torres_Jugador1[0].get_vida()<=0f){
            SceneManager.LoadScene("Perdio");
        }

        if(this.torres_Jugador2[0].get_vida()<=0f){
            SceneManager.LoadScene("Gano");
        }



    }

    void FixedUpdate()
    {
        muevepj();
    }




    //---------Movimiento de la camara-------
    void muevepj()
    {

        if (Input.GetKey(KeyCode.W))
        {
            this.jugador1.transform.Translate(new Vector3(-1f, 0, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.jugador1.transform.Translate(new Vector3(+1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.jugador1.transform.Translate(new Vector3(0, 0, 1f));
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.jugador1.transform.Translate(new Vector3(0, 0, -1f));
        }

        //Datos de debug
        //-------
        if (Input.GetKeyDown(KeyCode.O))
        {
            this.creaSoldadosEnemigo();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            creaSoldadosJugador();
        }
    }


    //------Datos de la AI--------
    void Ai_enemigo()
    {
        //Crea algunos soldados en cierto tiempo
        foreach (Soldier soldier in this.soldados_jugador2)
        {
            //Revisa los soldados que esten en espera
            if (soldier.isEnEspera() && baseRuta() != null)
            {
                //Selecciona una base aleatoria para atacar
                soldier.envia_a_pos(baseRuta().position);
            }
        }
    }

    //----Envia a un soldado a una zona especifica
    public void enviaSoldado(Vector3 punto)
    {
        foreach (Soldier soldier in this.soldados_seleccionados)
        {
            soldier.setEnCamino();
            soldier.envia_a_pos(punto);
        }
    }


    Transform baseRuta()
    {
        Building torreEnemigo = this.torres_Jugador2[0];
        List<Building> visitados = new List<Building>();
        List<Building> pila = new List<Building>();
        pila.Add(torreEnemigo);


        while (pila.Count > 0)
        {
            //Se elimina de la lista
            Building aux = pila[0];
            pila.RemoveAt(0);
            visitados.Add(aux);

            foreach (Building construccion in aux.get_caminos())
            {


                if (!construccion.tipo().Equals("Oponente"))
                {
                    return construccion.transform;
                }
                else
                {
                    if (!visitados.Contains(construccion))
                    {
                        pila.Add(construccion);
                        visitados.Add(construccion);
                    }
                }
            }

        }
        return null;

    }




    //Crea soldados aliado
    void creaSoldadosJugador()
    {
        Vector3[] posiciones = this.torres_Jugador1[0].GetComponent<Tower>().devuelve_posiciones();
        for (int i = 0; i < 5; i++)
        {
            //Se crea el objeto
            GameObject game = UnityEngine.Object.Instantiate(this.soldadoA, posiciones[i], Quaternion.identity);
            Soldier soldado = game.GetComponent<Soldier>();

            //Se inicializa
            soldado.Inicializa(500, 100, "Jugador");

            //Se agregan a la lista
            this.soldados_jugador1.Add(soldado);
        }
    }


    //Crea soldados del enemigo
    void creaSoldadosEnemigo()
    {
        Vector3[] posiciones = this.torres_Jugador2[0].GetComponent<Tower>().devuelve_posiciones();
        for (int i = 2; i < 4; i++)
        {
            //Se crea el objeto
            GameObject game = UnityEngine.Object.Instantiate(this.soldadoB, posiciones[i], Quaternion.identity);
            Soldier soldado = game.GetComponent<Soldier>();

            //Se inicializa
            soldado.Inicializa(500, 50, "Oponente");

            //Se agregan a la lista
            this.soldados_jugador2.Add(soldado);
        }
    }

    public void Visual_seleccion()
    {
        foreach (Unit u in this.soldados_jugador1)
        {
            if (this.soldados_seleccionados.Contains(u))
            {
                u.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                u.transform.GetChild(0).gameObject.SetActive(false);
            }

        }

    }

    public void agregaSoldadoSeleccion(Unit u)
    {
        if (!this.soldados_seleccionados.Contains(u))
        {
            this.soldados_seleccionados.Add(u);
        }
        else
        {
            this.soldados_seleccionados.Remove(u);
        }
    }

    public void deseleccionaSoldados()
    {
        foreach (Unit u in this.soldados_seleccionados)
        {
            u.transform.GetChild(0).gameObject.SetActive(false);

        }

        this.soldados_seleccionados.Clear();
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


        for (int i = this.soldados_jugador2.Count - 1; i >= 0; i--)
        {
            if (this.soldados_jugador2[i] == null)
            {
                this.soldados_jugador2.RemoveAt(i);
            }


        }

        for (int i = this.soldados_seleccionados.Count - 1; i >= 0; i--)
        {
            if (this.soldados_seleccionados[i] == null)
            {
                this.soldados_seleccionados.RemoveAt(i);
            }

        }



    }


    //---Get and set
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










}
