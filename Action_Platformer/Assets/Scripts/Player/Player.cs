using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float acc;
    public float drag;
    public float maxSpeed;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (speed == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed -= acc;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                speed += acc;
            }
        }

        else if (speed < 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed -= acc;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                speed += acc;
            }
            else
            {
                if (-drag < speed)
                {
                    speed = 0;
                }
                else
                {
                    speed += drag;
                }
            }
        }
        else if (speed > 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                speed -= acc;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                speed += acc;
            }
            else
            {
                if (drag > speed)
                {
                    speed = 0;
                }
                else
                {
                    speed -= drag;
                }
            }
        }
        if (speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
	}
}
