using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cell class encapsulates all types of rooms. 
public class Cell : MonoBehaviour
{
    public Cell(int index, int rowLength, GameObject room)
    {
        //SetIndex(index, rowLength);
        //this.room = room;
    }

    // 
    [SerializeField] GameObject room;

    private HashSet<Cell> neighbours;
    [SerializeField] public int index;
    private int x;
    private int y;

    [SerializeField] protected Cell left = null;
    [SerializeField] protected Cell right = null;
    [SerializeField] protected Cell up = null;
    [SerializeField] protected Cell down = null;

    public void SetIndex(int index, int rowLength, GameObject room)
    {
        this.room = room;
        this.index = index;
        x = index / rowLength;
        y = index % rowLength;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void addNeighbor(int rowLength, Cell other)
    {
        int neighborIndex = other.index;
        Debug.Log("This: " + index + ", Other: " + other.index);
        if (neighborIndex == this.index - 1)
        {
            left = other;
        } else if (neighborIndex == this.index + 1)
        {
            right = other;
        } else if (neighborIndex == this.index - rowLength)
        {
            up = other;
        } else if (neighborIndex == this.index + rowLength)
        {
            down = other;
        }
    }


}
