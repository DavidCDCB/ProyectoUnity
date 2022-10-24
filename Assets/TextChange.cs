using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{

     private GameObject[] enemies;
     private GameObject[] towers;

    public Text changingText;
    public int numero=5;

    // Start is called before the first frame update
    void Start()
    {
       this.enemies= GameObject.FindGameObjectsWithTag("Carro_Ali");
       this.towers= GameObject.FindGameObjectsWithTag("Torre_Ali");
        
    }

    // Update is called once per frame
    void Update()
    {
        int num=this.enemies.Length;
        changingText.text   = "x"+num;
        Debug.Log(this.enemies.Length);


    }
}
