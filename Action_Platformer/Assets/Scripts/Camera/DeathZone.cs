using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {
    public float leftLimmit;
    public float rightLimmit;
    public float upLimmit;
    public float downLimmit;
    Camera cam;
    GameObject player;
    float hightOffSet;
    float widthOffSet;

    // Use this for initialization
    void Start() {
        cam = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else {
            if ((player.transform.position.x > rightLimmit) || (player.transform.position.x < leftLimmit))
                Destroy(player);
            if ((player.transform.position.y > upLimmit) || (player.transform.position.y < downLimmit))
                Destroy(player);
        }

        hightOffSet = cam.orthographicSize + 0.5f;
        widthOffSet = (cam.orthographicSize * 2.5f) + 0.5f;
        leftLimmit = transform.position.x - widthOffSet;
        rightLimmit = transform.position.x + widthOffSet;
        upLimmit = transform.position.y + hightOffSet;
        downLimmit = transform.position.y - hightOffSet;
    }
}
