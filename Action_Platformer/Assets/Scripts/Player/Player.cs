using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float jump;
    public float maxSpeed;
    Rigidbody rb;

    bool hitGround;
    float RaycastDistGround = 0.5f;
    bool hitLeftWall;
    bool hitRightWall;
    float RaycastDistWall = 0.5f;
    public LayerMask GroundLayer;



    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        jump *= 100;

	}
	
	// Update is called once per frame
	void Update () {

        hitGround = Physics.Raycast(transform.position, Vector3.down, RaycastDistGround, GroundLayer);
        hitLeftWall = Physics.Raycast(transform.position, Vector3.left, RaycastDistGround, GroundLayer);
        hitRightWall = Physics.Raycast(transform.position, Vector3.right, RaycastDistGround, GroundLayer);


        Debug.Log(hitGround);
        Debug.Log(hitLeftWall);
        Debug.Log(hitRightWall);
        

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed,0,0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed, 0, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (hitGround)
                rb.AddForce(0, jump, 0);
            else if ((hitLeftWall) && (Input.GetKey(KeyCode.LeftArrow)))
                rb.AddForce(0, jump, 0);
            else if ((hitRightWall) && (Input.GetKey(KeyCode.RightArrow)))
                rb.AddForce(0, jump, 0);
        }


        if ((hitGround) && (rb.velocity.y < 0))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        }
    }
}
