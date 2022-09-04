using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoUnidadB : MonoBehaviour
{
    public float speed;
    private GameObject torre;
    private Transform transformPlayer;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 10;
        torre = GameObject.Find("t1Red");
        transformPlayer = torre.GetComponent<Transform>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        followObject(transformPlayer, transform);
    }

    private void followObject(Transform transformObjectToFollow, Transform transformObject){
        transformObject.position = Vector3.MoveTowards(
            transformObject.position, 
            transformObjectToFollow.position, 
            Time.deltaTime * this.speed
        );
        transformObject.LookAt(transformObjectToFollow.position);
    }
}
