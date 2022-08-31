using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMovement : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody rigidBody;
    private GameObject player = null;
    private Transform transformPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("OK");
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = player.GetComponent<Transform>();
        //test de comentario 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
        //transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        followObject(transformPlayer,transform);
        
        if(Input.GetKeyDown(KeyCode.Space)){
            rigidBody.AddForce(Vector2.up * 500);
            Instantiate(this, transform.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(0)){
            Vector3 mouse = Input.mousePosition;
            Debug.Log(mouse);
            //Instantiate(this, new Vector3(mouse.x,mouse.y,transform.position.z), Quaternion.identity);
        }
    }
//TEST
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Cube"){
            Debug.Log("FPSController");
            rigidBody.AddForce(Vector3.up * 500);
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
