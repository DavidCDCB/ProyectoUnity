using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre_aliada : MonoBehaviour
{

    public GameObject soldado_A;
    public GameObject soldado_B;
    public GameObject soldado_C;

    public int vida;


    public List<GameObject> soldados = new List<GameObject>();


    //banderas de estado
    public bool bool_corrutina = false;

    // Start is called before the first frame update
    void Start()
    {
        this.vida = 500;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.bool_corrutina == false)
        {

            StartCoroutine(HaceDanio(3));
        }



        //Se espera una cantidad para crear el otro soldado
        /*         if(bool_creaSoldado==false){
                StartCoroutine(HaceDanio(5));
                } */
    }


    /*         IEnumerator Crea_soldado(int segundos)
        {
            bool_creaSoldado=true;


            //Organiza la nueva posicion del soldado creado, se puede cambiar a futuro
            int angulo=Random.Range(-180, 180);
            float posx=Mathf.Sin(angulo)*2+transform.position.x;
            float posy=Mathf.Cos(angulo)*2+transform.position.y;  


            //Crea y organiza el soldado
            GameObject soldado_tmp=Instantiate(soldado_A, new Vector3(posx, 0, posy), Quaternion.identity);
            soldados.Add(soldado_tmp);

            //Espera 5 segundos
            yield return new WaitForSeconds(5);
            bool_creaSoldado=false;
        } */


    IEnumerator HaceDanio(int segundos)
    {
        this.bool_corrutina = true;
        this.vida = this.vida - 10;
        yield return new WaitForSeconds(5);
        this.bool_corrutina = false;
    }



}
