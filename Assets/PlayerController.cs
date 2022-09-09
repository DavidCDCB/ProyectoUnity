
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float rayLength;
    public LayerMask layermask;

    private void Update(){
        if(Input.GetMouseButtonDown (0)  && !EventSystem.current.IsPointerOverGameObject()){
            RaycastHit hit;
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, rayLength, layermask)){
                Debug.Log(hit.collider.gameObject.name);
                MovimientoUnidadB descScript=hit.collider.GetComponent<MovimientoUnidadB>();
                if(descScript){
                    descScript.sigueObjeto();
                }
                //hit.collider.gameObject.sigueObjeto();
            }
        


        }
    }
}
