using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Basic : Cell
{
    //[SerializeField] GameObject roomImage;
    //[SerializeField] SpriteRenderer render;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject upWall;
    [SerializeField] GameObject downWall;
   

    public Room_Basic(int index, int rowLength, GameObject room) : base(index, rowLength, room)
    {
        //roomImage = (GameObject)Resources.Load("GameObject");
        //roomImage.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("standard_map.png");
    }

    public void SetupWalls()
    {
        if (left != null)
        {
            Debug.Log("Left removed for room at index: " + index);

            leftWall.GetComponent<SpriteRenderer>().enabled = false;
            //leftWall.GetComponent<SpriteRenderer>().transform.localPosition = new Vector3(0, 0, 0);
            leftWall.GetComponent<Wall>().OpenDoor();
            leftWall.GetComponent<Wall>().roomIndex = index;
        }
        if (right != null)
        {
            Debug.Log("Right removed for room at index: " + index);
            rightWall.GetComponent<SpriteRenderer>().enabled = false;
            //rightWall.GetComponent<SpriteRenderer>().transform.localPosition = new Vector3(0, 0, 0);
            rightWall.GetComponent<Wall>().OpenDoor();
            rightWall.GetComponent<Wall>().roomIndex = index;
        }
        if (up != null)
        {
            Debug.Log("Up removed for room at index: " + index);
            upWall.GetComponent<SpriteRenderer>().enabled = false;
            //upWall.GetComponent<SpriteRenderer>().transform.localPosition = new Vector3(0, 0, 0);
            upWall.GetComponent<Wall>().OpenDoor();
            upWall.GetComponent<Wall>().roomIndex = index;
        }
        if (down != null)
        {
            Debug.Log("Down removed for room at index: "+index);
            downWall.GetComponent<SpriteRenderer>().enabled = false;
            //downWall.GetComponent<SpriteRenderer>().transform.localPosition = new Vector3(0, 0, 0);
            downWall.GetComponent<Wall>().OpenDoor();
            downWall.GetComponent<Wall>().roomIndex = index;
        }
    }
}
