using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Blue : MonoBehaviour
{
    float initTime;
    float delayTime;
    public Material material_des;
    int num = 0;

    void Start()
    {
        this.delayTime = Random.Range(1,3);
        this.initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-initTime > this.delayTime){
            float numAleatorio = Random.Range(20f,90f);
            Vector3 vectorPosicion = new Vector3(Mathf.Sin(numAleatorio)*5,0f,Mathf.Cos(numAleatorio)*5);

            GameObject tmp=Instantiate(GameObject.Find("UnidadB"), 
                transform.position+vectorPosicion, 
                Quaternion.identity
            );

            tmp.transform.GetComponent<MeshRenderer>().material = this.material_des;
            this.initTime = Time.time;
            this.delayTime = Random.Range(1,5);
        }

        //StartCoroutine(ExampleCoroutine());
    }



/*     IEnumerator ExampleCoroutine()
    {
        while(true) {
            //Does something
            yield return new WaitForSeconds(5f);
            Instantiate(GameObject.Find("UnidadB"), transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    } */
}
