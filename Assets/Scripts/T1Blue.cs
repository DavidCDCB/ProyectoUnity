using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Blue : MonoBehaviour
{
    float initTime;
    float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        this.delayTime = 3;
        this.initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-initTime > this.delayTime){
            print("CREADO");
            Instantiate(GameObject.Find("UnidadB"), 
                transform.position, 
                Quaternion.identity
            );
            this.initTime = Time.time;
        } 

        //StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        while(true) {
            //Does something
            yield return new WaitForSeconds(5f);
            Instantiate(GameObject.Find("UnidadB"), transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
