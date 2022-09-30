using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    public Material material_sel;
    public Material material_des;

    void Awake()
    {
        //Si una instancia existe y no es esta
        //Sino establece esta como la instancia
        if (_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{ 
            _instance = this;
        }
    }

    void Start()
    {
        this.Deselect();
        
    }

    void Update()
    {

    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        this.marcaObjeto(unitToAdd);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd)){
            unitsSelected.Add(unitToAdd);
            this.marcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }else{
            unitsSelected.Remove(unitToAdd);
            this.desmarcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd)){
            unitsSelected.Add(unitToAdd);
            this.marcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected){
            unit.GetComponent<UnitMovement>().enabled = false;
            Debug.Log("Deselecciono la unidad");
            this.desmarcaObjeto(unit);
        }
    }

    public void Deselect()
    {
        unitsSelected.Clear();
    }

    //Cambiar si se agrega otro metodo de visualizar seleccion
    //Evento que marca el objeto si se selecciona
    public void marcaObjeto(GameObject unit)
    {
        if(unit.name.Contains("AutoTorre")){
            unit.transform.GetChild(0).gameObject.SetActive(true);
        }else{
            unit.transform.GetComponent<MeshRenderer>().material = this.material_sel;
        }
    }

    //Evento que desmarca el objeto si se selecciona
    public void desmarcaObjeto(GameObject unit)
    {
        if(unit.name.Contains("AutoTorre")){
            unit.transform.GetChild(0).gameObject.SetActive(false);
        }else{
            unit.transform.GetComponent<MeshRenderer>().material = this.material_des;
        }
    }
}
