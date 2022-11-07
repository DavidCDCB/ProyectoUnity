using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAtacado : MonoBehaviour
{
    public double hp = 100;
    public bool atacado;
    public Material mAtacado;
    public Material normal;

    // Start is called before the first frame update
    void Start()
    {
        this.atacado = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetComponent<MeshRenderer>().material = this.normal;
        if(atacado == true){
            this.transform.GetComponent<MeshRenderer>().material = this.mAtacado;
            this.atacado = false;
        }
    }

    public double getHp(){
        return this.hp;
    }
    public void setHp(double newHp){
        this.hp = newHp;
    }
}
