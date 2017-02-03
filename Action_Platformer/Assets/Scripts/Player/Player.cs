using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float jump;
    public float maxSpeed;
    Rigidbody rb;
    

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        jump *= 100;
	}
	
	// Update is called once per frame
	void Update () {
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
            rb.AddForce(0, jump, 0);
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
