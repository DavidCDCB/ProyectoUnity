using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        //Si una instancia existe y no es esta
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        //Sino establece esta como la instancia
        else
        {
            _instance = this;
        }
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
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            this.marcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
        else
        {
            unitsSelected.Remove(unitToAdd);
            this.desmarcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
        }

    }

    public void DragSelect(GameObject unitToAdd)
    {

        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            this.marcaObjeto(unitToAdd);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }




    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {

            unit.GetComponent<UnitMovement>().enabled = false;
            Debug.Log("Deselecciono la unidad");
            this.desmarcaObjeto(unit);
            
        }

    }

    public void Deselect()
    {

        unitsSelected.Clear();

    }




    void Start()
    {
this.Deselect();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Cambiar si se agrega otro metodo de visualizar seleccion
    //Evento que marca el objeto si se selecciona
    public void marcaObjeto(GameObject unit)
    {
        unit.transform.GetChild(0).gameObject.SetActive(true);
    }

    //Evento que desmarca el objeto si se selecciona
    public void desmarcaObjeto(GameObject unit)
    {
        unit.transform.GetChild(0).gameObject.SetActive(false);
    }
}
