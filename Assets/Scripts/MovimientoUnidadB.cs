using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoUnidadB : MonoBehaviour
{
    public float speed;
    private GameObject torre;
    private Transform transformPlayer;
     
    private bool sigue=false;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.sigue){
        followObject(transformPlayer, transform);
        }
    }

    private void followObject(Transform transformObjectToFollow, Transform transformObject){
        transformObject.position = Vector3.MoveTowards(
            transformObject.position, 
            transformObjectToFollow.position, 
            Time.deltaTime * this.speed
        );
        transformObject.LookAt(transformObjectToFollow.position);
    }

    public void sigueObjeto(){
        this.sigue=true;
        this.speed = 10;
        torre = GameObject.Find("t1Red");
        transformPlayer = torre.GetComponent<Transform>();


    }
}
