using System.Collections;
using UnityEngine;

public class ScriptUnidad : MonoBehaviour
{
    private int hp = 100;
    private int cantidad = 0;
    private GameObject unidadApuntada;
    private bool atacando = false;

    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }

    void Update(){}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name.Contains("Red")){
            this.atacando = false;
            Debug.Log("ACTIVADO");
            if(unidadApuntada == null){
                this.unidadApuntada = collider.gameObject;
            }
        }
    }

    void OnTriggerStay(Collider collider) 
    {
        if(collider.gameObject.name.Contains("Red")){
            Debug.Log("Dentro");
            StartCoroutine(HaceDanio(Random.Range(5,20), 3, collider.gameObject));
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.name.Contains("Red")){
            Debug.Log("desactivado");
            this.unidadApuntada = null;
        }
    }

    public int getHp()
    {
        return this.hp;
    }
    public void setHp(int newHp)
    {
        this.hp = newHp;
    }

    IEnumerator HaceDanio(double cantidad, int segundos, GameObject unidadApuntada)
    {
        scriptTorre scriptUnidad = unidadApuntada.GetComponent<scriptTorre>();
        
        if(this.atacando == false){
            Debug.Log(scriptUnidad.getHp());
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