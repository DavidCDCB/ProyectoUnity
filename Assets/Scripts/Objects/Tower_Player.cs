using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Player : MonoBehaviour
{

    private int hpInicial;
    private int hpActual;

    //Objetos
    public GameObject soldado;
    public List<GameObject> lista;



    // Start is called before the first frame update
    void Start()
    {
        this.hpInicial = 500;
        this.hpActual = 500;

        creaSoldados();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void creaSoldados()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        this.lista.Add(Instantiate(this.soldado, new Vector3(x-0.5f, y, z +1), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x-0.3f, y, z +1), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x+0.3f, y, z +1), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x+0.5f, y, z +1), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x-0.3f, y, z +3), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x-0, y, z +3), Quaternion.identity));
        this.lista.Add(Instantiate(this.soldado, new Vector3(x+0.3f, y, z +3), Quaternion.identity));
    }



    //Obtiene el Hp de la torre
    public int getHpInicial()
    {
        return this.hpInicial;
    }

    //Obtiene el Hp de la torre
    public int getHpActual()
    {
        return this.hpActual;
    }

    //Modifica el Hp de la torre, el segundo parametro establece si se ignora la invencibiidad
    public void modificaHP(int hp, int tipo)
    {
        if (this.hpActual + hp > this.hpInicial)
        {
            this.hpActual = this.hpInicial;
        }
        else if (this.hpActual + hp < 0)
        {
            this.hpActual = 0;
        }
        else
        {
            this.hpActual = this.hpActual + hp;
        }
    }


    //Obtiene la posicion de la torre
    public float[] getPosicion()
    {
        return new float[2] { this.transform.position.x, this.transform.position.y };
    }


}
