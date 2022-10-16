using System.Collections;
using UnityEngine;

public class ScriptUnidad : MonoBehaviour
{
    private GameObject unidadApuntada;
    private bool atacando = false;
    public string nombreAtacado = "Red";

    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
        UnitSelections.Instance.unitsSelected.Remove(this.gameObject);
    }

    void Update(){}

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
        if(collider.gameObject.tag.Contains(nombreAtacado)){
            Debug.Log(collider.gameObject.name);
            StartCoroutine(HaceDanio(Random.Range(5,20), 3, collider.gameObject));
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.tag.Contains(nombreAtacado)){
            Debug.Log("desactivado");
            this.unidadApuntada = null;
        }
    }

    IEnumerator HaceDanio(double cantidad, int segundos, GameObject unidadApuntada)
    {
        ScriptAtacado scriptUnidad = unidadApuntada.GetComponent<ScriptAtacado>();
        
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