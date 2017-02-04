using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Basic movement variables
    public float speed;
    public float jump;
    public float maxSpeed;
    public float drag;
    Rigidbody rb;

    //Frame timer for jumping to feel more resposive
    int upPressedTimer = 0;
    int checkDelay = 5;

    //Variables for directional collision detection
    //Down collision detection
    bool hitGround;
    bool hitGround1;
    bool hitGround2;
    float RaycastDistGround = 0.50f;
    //Up collision detection
    bool hitCeiling;
    float RaycastDistCeiling = 0.50f;
    //Left and Right collision detection
    bool hitLeftWall;
    bool hitRightWall;
    float RaycastDistWall = 0.50f;
    //Only detect objects marked als ground blocks
    public LayerMask GroundLayer;



    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //Check if touching ground, wall and/or ceiling and direction
        //Check the edges of the cube if standing on a ledge (allows jumping while more than halve over a ledge)
        hitGround1 = Physics.Raycast(new Vector3(transform.position.x - 0.49f, transform.position.y, transform.position.z), Vector3.down, RaycastDistGround, GroundLayer);
        hitGround2 = Physics.Raycast(new Vector3(transform.position.x + 0.49f, transform.position.y, transform.position.z), Vector3.down, RaycastDistGround, GroundLayer);
        if (hitGround1 || hitGround2)
            hitGround = true;
        else
            hitGround = false;
        hitCeiling = Physics.Raycast(transform.position, Vector3.up, RaycastDistCeiling, GroundLayer);
        hitLeftWall = Physics.Raycast(transform.position, Vector3.left, RaycastDistGround, GroundLayer);
        hitRightWall = Physics.Raycast(transform.position, Vector3.right, RaycastDistGround, GroundLayer);

        /*
        Debug.Log(hitCeiling);
        Debug.Log(hitRightWall);
        Debug.Log(hitLeftWall);
        Debug.Log(hitGround);
        */

        //Prevent double controles
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (hitGround)
                Drag();
        }
        //Movement on X
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed,0,0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed, 0, 0);
        }
        else
        {
            if (hitGround)
                Drag();
        }

        //Jump Controles
        //only check on frame when pressed to avoid unnessesary jumping
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upPressedTimer = checkDelay;
        }
        //Check if jump pressed for more that one frame for nicer controling but preventing autojump with a timer
        else if ((Input.GetKey(KeyCode.UpArrow)) && (upPressedTimer > 0))
        {
            //Only allow jumping while on the ground
            //If on wall and pressing button towards that wall allow walljump
            if ((hitLeftWall) && (Input.GetKey(KeyCode.LeftArrow)))
                rb.velocity = new Vector3(jump, jump, rb.velocity.z);
            else if ((hitRightWall) && (Input.GetKey(KeyCode.RightArrow)))
                rb.velocity = new Vector3(-jump, jump, rb.velocity.z);
            else if (hitGround)
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
        }
        //Deplete upPressedTimer so that you cannot hold up
        if (upPressedTimer > 0)
            upPressedTimer--;

        //Gravity simulation
        if (hitGround)
        { }
        //When on wall slide down slower
        else if (((hitLeftWall) && (Input.GetKey(KeyCode.LeftArrow))) || ((hitRightWall) && (Input.GetKey(KeyCode.RightArrow))))
        {
            //Gravity isn't weaker when sliding up a wall
            if (rb.velocity.y >= -0.01f)
                rb.AddForce(0, -9.81f * rb.mass, 0);
            //Slide down slower when already going down
            else
                rb.AddForce(0, -2.4525f * rb.mass, 0);
        }
        //If not on ground or on wall apply normal gravity 
        else
        {
            rb.AddForce(0, -9.81f * rb.mass, 0);
        }
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

    void Drag()
    {
        //Custom drag
        if (rb.velocity.x > 0)
        {
            if (rb.velocity.x < drag)
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            else
                rb.velocity = new Vector3(rb.velocity.x - drag, rb.velocity.y, rb.velocity.z);
        }
        else if (rb.velocity.x < 0)
        {
            if (rb.velocity.x > -drag)
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            else
                rb.velocity = new Vector3(rb.velocity.x + drag, rb.velocity.y, rb.velocity.z);
        }
    }
}
