using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public float speed;
    public float initTime = 0;
    float delayTime;
    public string nombreAtacado = "";
    public Transform enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 50;
        this.delayTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy != null){
            this.followObject(enemy, transform);
        }
        if(this.initTime > 0){
            if(Time.time-initTime > this.delayTime){
                Destroy(this.gameObject);
            }
        }
    }
    

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag.Contains(nombreAtacado) || collider.gameObject.tag.Contains(this.gameObject.name)){
            Destroy(this.gameObject);
        }
    }

    private void followObject(Transform transformObjectToFollow, Transform transformObject){
        transformObject.position = Vector3.MoveTowards(
            transformObject.position, 
            transformObjectToFollow.position, 
            Time.deltaTime * this.speed
        );
        //transform.right = transformPlayer.position - transform.position;
        transformObject.LookAt(transformObjectToFollow.position);
    }
}
