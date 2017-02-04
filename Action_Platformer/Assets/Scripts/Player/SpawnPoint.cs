using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public GameObject player;
    

	// Update is called once per frame
	void FixedUpdate () {
        //If there is no child (player) spawn a player and make it a child
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameObject playerClone = (GameObject)Instantiate(player, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            //playerClone.transform.parent = gameObject.transform;
        }
	}
}
