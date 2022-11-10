using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            Manager_Game.Instance.creaSoldado1();
            print("z key was pressed");
        }

        if (Input.GetKeyDown("x"))
        {
            Manager_Game.Instance.creaSoldado2();
            print("x key was pressed");
        }

        if (Input.GetKeyDown("c"))
        {
            print("c key was pressed");
        }
    }
}
