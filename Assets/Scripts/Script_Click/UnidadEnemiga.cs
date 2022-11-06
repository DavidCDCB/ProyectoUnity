using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class UnidadEnemiga : MonoBehaviour
{
    private GameObject unidadApuntada;
    private List<GameObject> objetivos = new List<GameObject>(); 
    private bool atacando = false;
    public string nombreAtacado = "Blue";
    private NavMeshAgent myAgent;

    private int targetTower = 0;
    private int initialSize = 0;
    private int currentSize = 0;

    // Intervalo de elección de torre
    private float initTime;
    private float delayTime;

    void Start(){
        this.myAgent = GetComponent<NavMeshAgent>();
        this.objetivos = GameObject.FindGameObjectsWithTag("BlueTower").ToList();
        this.delayTime = 5;
    }

    void Update(){
        this.objetivos = GameObject.FindGameObjectsWithTag("BlueTower").ToList();
        this.currentSize = this.objetivos.Count;

        if(this.currentSize != this.initialSize){
            this.initialSize = this.currentSize;
            this.initTime = Time.time;

            // Criterio de elección de torre ...
            this.targetTower = Random.Range(0, this.currentSize);
        }

        if(Time.time-initTime > this.delayTime && this.currentSize > 0){
            myAgent.SetDestination(this.objetivos[targetTower].GetComponent<Transform>().position);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag.Contains(nombreAtacado)){
            this.atacando = false;
            Debug.Log(collider.gameObject.tag);
            if(unidadApuntada == null){
                this.unidadApuntada = collider.gameObject;
            }
        }
    }

    void OnTriggerStay(Collider collider) 
    {
        if(collider.gameObject.tag.Contains(nombreAtacado) && unidadApuntada != null){
            StartCoroutine(HaceDanio(Random.Range(5,20), 3, this.unidadApuntada));
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.tag.Contains(nombreAtacado)){
            Debug.Log("desactivado");
            this.unidadApuntada = null;
        }
    }

    void Disparar()
    {
        BalaScript enemigo = Instantiate(GameObject.Find("Bala"), 
            transform.position, 
            Quaternion.identity
        ).GetComponent<BalaScript>();
        enemigo.nombreAtacado = this.nombreAtacado;
        enemigo.enemy = this.unidadApuntada.transform;
        enemigo.initTime = Time.time;
    }

    IEnumerator HaceDanio(double cantidad, int segundos, GameObject unidadApuntada)
    {
        ScriptAtacado scriptUnidad = unidadApuntada.GetComponent<ScriptAtacado>();
        
        if(this.atacando == false){
            //Debug.Log(scriptUnidad.getHp());
            this.Disparar();
            scriptUnidad.setHp(scriptUnidad.getHp() - cantidad);
            this.atacando = true;
            scriptUnidad.atacado = true;
            yield return new WaitForSeconds(segundos);
            this.atacando = false;
            scriptUnidad.atacado = false;
        }
        if(scriptUnidad.getHp() < 10){
            Destroy(unidadApuntada);
        }
    }
}