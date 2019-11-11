using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    private Vector3 offset;
    public GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        offset = transform.position - hero.transform.position;
    }

    // Update is called once per frame but it is guaranteed to run after ALL OTHER items
    void LateUpdate()
    {
        // as we move our player, the camera will follow the player.
        transform.position = hero.transform.position + offset;
    }
} //end of camera controller class
