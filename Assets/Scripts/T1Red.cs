using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1Red : MonoBehaviour
{
    float initTime;
    float delayTime;
    public Material material_des;

    //Contador aleatorio, cambiar a metodo mas optimo proximamente
    //No se que ventaja tiene usar contadores frente a usar rangos de tiempo
    int num = 0;
    int contador = 0;

    void Start()
    {
        //this.num = Random.Range(40,400);
        //this.delayTime = 3;
        //this.initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.contador >= 100){
            //Debug.Log("se creo una unidad");
            float numAleatorio = Random.Range(20f,90f);
            Vector3 vectorPosicion = new Vector3(Mathf.Sin(numAleatorio)*10,0f,Mathf.Cos(numAleatorio)*10);

            GameObject tmp=Instantiate(GameObject.Find("UnidadR"), 
                transform.position+vectorPosicion, 
                Quaternion.identity
            );

            tmp.transform.GetComponent<MeshRenderer>().material = this.material_des;
            this.contador=0;
            this.num=Random.Range(40,400);
        }
        contador++;

/*      if(Time.time-initTime > this.delayTime){
            print("CREADO");
            Instantiate(GameObject.Find("UnidadB"), 
                transform.position, 
                Quaternion.identity
            );
            this.initTime = Time.time;
        }  */

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
