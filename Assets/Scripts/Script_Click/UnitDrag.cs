using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    //Parte Grafica
    [SerializeField]
    RectTransform boxVisual;

    //Parte logica
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        //En click
        if (Input.GetMouseButtonDown(0)){
            this.startPosition = Input.mousePosition;
            this.selectionBox = new Rect();
        }

        //Durante
        if (Input.GetMouseButton(0)){
            this.endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //Al final
        if (Input.GetMouseButtonUp(0)){
            SelectUnits();
            this.startPosition = Vector2.zero;
            this.endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = this.startPosition;
        Vector2 boxEnd = this.endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //Calculos para X
        if (Input.mousePosition.x < startPosition.x){
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }else{
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //Calculos para Y
        if (Input.mousePosition.y < startPosition.y){
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }else{
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }

        /*print(selectionBox.xMin);
        print(selectionBox.xMax);
        print(selectionBox.yMin);
        print(selectionBox.yMax); */
    }

    void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.unitList){
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position))){
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }

}
