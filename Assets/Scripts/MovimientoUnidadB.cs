using UnityEngine;

public class MovimientoUnidadB : MonoBehaviour
{
    public float speed;
    private GameObject torre;
    private Transform transformPlayer;
    private Vector3 targetPosition;
    private bool sigue=false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.sigue){
            followObject(targetPosition, transform);
        }

        if(Vector3.Distance(transform.position,this.targetPosition) < 1){
            this.sigue = false;
        }
    }

    private void followObject(Vector3 objectToFollow, Transform transformObject)
    {
        transformObject.position = Vector3.MoveTowards(
            transformObject.position, 
            objectToFollow, 
            Time.deltaTime * this.speed
        );
        transformObject.LookAt(objectToFollow);
    }

    public void followStart(Vector3 position)
    {
        this.sigue = true;
        this.speed = 10;
        torre = GameObject.Find("t1Red");
        this.targetPosition = position;
    }
}
