using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{

    //Instancia de control de juego
    private static Game_Controller _instance;
    public static Game_Controller Instance { get { return _instance; } }

    //Torres a crear
    public GameObject torre;
    public List<GameObject> torres = new List<GameObject>();
    public List<GameObject> soldados_seleccionados = new List<GameObject>();

    //Variables
    private int hpTotal=0;
    private int hpActual=0;


    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        comienzaPartida();


    }

    // Update is called once per frame
    void Update()
    {

    }

    //Script si inicia juego

    void comienzaPartida()
    {
        //Se crea la torre inicial
        torres.Add(Instantiate(torre, new Vector3(0, 2.5f, -50), Quaternion.identity));
        torres.Add(Instantiate(torre, new Vector3(5, 2.5f, -40), Quaternion.identity));
        torres.Add(Instantiate(torre, new Vector3(-5, 2.5f, -40), Quaternion.identity));

        foreach (GameObject torre in this.torres)
        {
            /* torre.GetComponent<Tower_Player>().getHp(); */
        }


    }


    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 100), (1.0f / Time.smoothDeltaTime).ToString());
    }
}
