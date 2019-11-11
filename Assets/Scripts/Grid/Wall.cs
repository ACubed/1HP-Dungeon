using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] private bool isVertical = false;
    [SerializeField] private bool isDoor = false;
    [SerializeField] private bool active = false;
    private bool opened = false;
    public int roomIndex = 0;

    [SerializeField] private BoxCollider2D wall_collider;

    // Start is called before the first frame update
    void Start()
    {
        string fileName = isVertical ? "vertical_" : "horizontal_";
        fileName += isDoor ? "door" : "wall";
        fileName += ".png";

        wall = (GameObject)Resources.Load("GameObject");
        wall.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(fileName);
        wall_collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!opened && !active)
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StageGrid.SetCamera(roomIndex);
    }

    public void OpenDoor()
    {
        wall = (GameObject)Resources.Load("GameObject");
        wall_collider = gameObject.GetComponent<BoxCollider2D>();

        Debug.Log("Door opened");
        Color c = wall.GetComponent<SpriteRenderer>().color;
        c.a = 50f;
        wall.GetComponent<SpriteRenderer>().color = c;
        wall_collider.isTrigger = true;
        opened = true;
    }
}
    
