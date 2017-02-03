using UnityEngine;
using System.Collections;

public class CameraFolow : MonoBehaviour {
    GameObject player;
    public float maxOfSet;

	// Update is called once per frame
	void FixedUpdate () {

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else {
            //If player is too far to the right the camera will folow the player
            if ((player.transform.position.x - transform.position.x) > maxOfSet)
            {
                transform.position = new Vector3(player.transform.position.x - maxOfSet, transform.position.y, transform.position.z);
            }
            //If player is too far to the left the camera will folow the player
            else if ((player.transform.position.x - transform.position.x) < -maxOfSet)
            {
                transform.position = new Vector3(player.transform.position.x + maxOfSet, transform.position.y, transform.position.z);
            }
        }
    }
}
