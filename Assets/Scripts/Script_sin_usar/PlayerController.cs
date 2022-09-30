using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float rayLength;
    public LayerMask layermask;
    private MovimientoUnidadB currentObject;

    private void Update()
    {
        checkEventClick();
    }

    private void checkEventClick()
    {
        RaycastHit hit;
        Ray ray;
        if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, rayLength, layermask)){
                checkClickOnObject(Input.GetMouseButtonDown(0), hit, "UnidadB(Clone)");
            }
        }
    }

    private void checkClickOnObject(bool isLeftClick, RaycastHit hit, string targetNameObject)
    {
        string nameOfObject = hit.collider.gameObject.name;
        if(nameOfObject == targetNameObject){
            Debug.Log(nameOfObject);
            this.currentObject = hit.collider.GetComponent<MovimientoUnidadB>();
        }else if(this.currentObject && !isLeftClick){
            Debug.Log(hit.point);
            this.currentObject.followStart(hit.point);
        }
    }
}
