using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public LayerMask Clickable;
    public LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable)){
                if (Input.GetKey(KeyCode.A)){
                    Debug.Log("leftshift");
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }else{
                    Debug.Log("normal");
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }else{
                if(!Input.GetKey(KeyCode.LeftShift)){
                    Debug.Log("deseleccionar");
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
