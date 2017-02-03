using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Basic movement variables
    public float speed;
    public float jump;
    public float maxSpeed;
    Rigidbody rb;

    //Frame timer for jumping to feel more resposive
    int upPressedTimer = 0;
    int checkDelay = 5;

    //Variables for directional collision detection
    //Down collision detection
    bool hitGround;
    float RaycastDistGround = 0.5f;
    //Up collision detection
    bool hitCeiling;
    float RaycastDistCeiling = 0.5f;
    //Left and Right collision detection
    bool hitLeftWall;
    bool hitRightWall;
    float RaycastDistWall = 0.5f;
    //Only detect objects marked als ground blocks
    public LayerMask GroundLayer;



    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        //jump *= 100;

	}
	
	// Update is called once per frame
	void Update () {

        //Check if touching ground, wall and/or ceiling and direction
        hitGround = Physics.Raycast(transform.position, Vector3.down, RaycastDistGround, GroundLayer);
        hitCeiling = Physics.Raycast(transform.position, Vector3.up, RaycastDistCeiling, GroundLayer);
        hitLeftWall = Physics.Raycast(transform.position, Vector3.left, RaycastDistGround, GroundLayer);
        hitRightWall = Physics.Raycast(transform.position, Vector3.right, RaycastDistGround, GroundLayer);

        /*
        Collsion checks debuging

        Debug.Log(hitGround);
        Debug.Log(hitCeiling);
        Debug.Log(hitLeftWall);
        Debug.Log(hitRightWall);
        */

        //Prevent double controles
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {}
        //Movement on X
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed,0,0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed, 0, 0);
        }

        //Jump Controles
        //only check on frame when pressed to avoid unnessesary jumping
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //If on wall and pressing button towards that wall allow walljump
            if ((hitLeftWall) && (Input.GetKey(KeyCode.LeftArrow)))
                rb.velocity = new Vector3(jump, jump, rb.velocity.z);
            else if ((hitRightWall) && (Input.GetKey(KeyCode.RightArrow)))
                rb.velocity = new Vector3(-jump, jump, rb.velocity.z);
            upPressedTimer = checkDelay;
        }
        //Check if jump pressed for more that one frame for nicer controling but preventing autojump with a timer
        else if ((Input.GetKey(KeyCode.UpArrow)) && (upPressedTimer > 0))
        {
            //Only allow jumping while on the ground
            if (hitGround)
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
        }
        //Deplete upPressedTimer so that you cannot hold up
        if (upPressedTimer > 0)
            upPressedTimer--;
    }

    void FixedUpdate()
    {
        //Set a max X speed to prevent lightspeed 
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        }

        //Prevent clipping using raycasting in all 4 directions
        if ((hitLeftWall) && (rb.velocity.x < 0))
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        if ((hitRightWall) && (rb.velocity.x > 0))
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        if ((hitGround) && (rb.velocity.y < 0))
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if ((hitCeiling) && (rb.velocity.y > 0))
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

    }
}
