using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneradorUnidades : MonoBehaviour
{
    private float initTime;
    private float delayTime;
    public float delayTimeMax;
    public Material material_des;
    private int unidades;

    public string towerId = "";
    public string originalName = "";
    public string prefixTag = "";

    void Start()
    {
        this.delayTime = Random.Range(1,delayTimeMax);
        this.initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-initTime > this.delayTime && this.unidades < 5){
            float numAleatorio = Random.Range(20f,90f);
            Vector3 vectorPosicion = new Vector3(Mathf.Sin(numAleatorio)*5,0f,Mathf.Cos(numAleatorio)*5);

            GameObject tmp = Instantiate(GameObject.Find(originalName), 
                transform.position+vectorPosicion, 
                Quaternion.identity
            );
            tmp.tag = this.prefixTag+this.towerId;
            this.unidades = GameObject.FindGameObjectsWithTag(tmp.tag).ToList().Count;

            tmp.transform.GetComponent<MeshRenderer>().material = this.material_des;
            this.initTime = Time.time;
            this.delayTime = Random.Range(1,delayTimeMax);
        }

        if(this.unidades > 0){
            this.unidades = GameObject.FindGameObjectsWithTag(this.prefixTag+this.towerId).ToList().Count;
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
