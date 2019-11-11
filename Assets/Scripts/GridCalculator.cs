using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCalculator
{
    // Calculate the relative coordinates of an adjacent cell.
    // return [x+1, y] or [x-1, y] or [x, y-1], or [x, y+1]
    public static int[] getRandomAdjacent(int x, int y)
    {
        int[] result = new int[] { x, y };


        int index = Random.Range(0, 4); // 0 for x, 1 for y

        switch (index)
        {
            case 0:
                result[0]++;
                break;
            case 1:
                result[0]--;
                break;
            case 2:
                result[1]++;
                break;
            case 3:
                result[1]--;
                break;
        }

        return result;
    }



    // Return a list of valid neghbours for the given index, assuming a square layout.
    public static int[] NeighborIndices(int index, int numCells, int rowLength)
    {
        int[] result = new int[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = -1;
        }

        // left
        if ((index / rowLength) == ((index-1) / rowLength))
        {
            result[0] = index - 1;
        }

        //right
        if ((index / rowLength) == ((index + 1) / rowLength))
        {
            result[1] = index + 1;
        }

        //up
        if ((index - rowLength) >= 0)
        {
            result[2] = index - rowLength;
        }

        //down
        if ((index + rowLength) < numCells)
        {
            result[3] = index + rowLength;
        }

        return result;
    }

    public static Vector3 vectorFromIndex(int index, int numCells, int rowLength)
    {
        int x = index % rowLength;
        int y = index / rowLength;
        int z = 2;

        return new Vector3((float)x, (float)y * -0.75f, (float)z) * 20;
    }

    public static Vector3 relativeVectorFromIndex(int lastIndex, int index, int numCells, int rowLength)
    {
        Vector3 center = vectorFromIndex(index, numCells, rowLength);


        int directionOffset = index - lastIndex;

        if (directionOffset == 1)
        {
            center.x -= 6;
        } else if (directionOffset == -1)
        {
            center.x += 6;
        } else if (directionOffset == rowLength)
        {
            center.y -= 5;
        } else if (directionOffset == -rowLength)
        {
            center.y += 5;
        } else
        {
            return center;
        }


        return center;
    }

}
