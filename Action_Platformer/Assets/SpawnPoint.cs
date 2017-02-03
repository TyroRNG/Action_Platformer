using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public GameObject player;
    

	// Update is called once per frame
	void FixedUpdate () {
        //If there is no child (player) spawn a player and make it a child
        if (transform.childCount == 0)
        {
            GameObject playerClone = (GameObject)Instantiate(player, transform.position, Quaternion.identity);
            playerClone.transform.parent = gameObject.transform;
        }
	}
}
