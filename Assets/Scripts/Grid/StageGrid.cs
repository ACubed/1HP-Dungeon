using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageGrid : MonoBehaviour
{
    //private List<Cell> stageCells;
    //private Cell startRoom = null;
    //private Cell bossRoom = null;
    //private Cell shopRoom = null;

    [Header("Cell Lists")]
    //[SerializeField] private Room_Normal[] normalRoomComponents;
    [SerializeField] private Room_Basic[] cellComponents;

    [Header("Room Type Scripts")]
    [SerializeField] private GameObject normal = null;
    [SerializeField] private GameObject start = null;
    [SerializeField] private GameObject boss = null;
    [SerializeField] private GameObject shop = null;

    [SerializeField] private static Camera cam;
   
        
    [Header("Stage Settings")]
    [SerializeField] private static int numCells = 9; // The total number of cells to generate for this stage.
    [SerializeField] private static int rowLength = 3;
    [SerializeField] private static int difficultyLevel = 0;

    private static int currentCameraIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel++;

        SetupCells();
        MakeConnections();
        InitializeWalls();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetCamera(int index)
    {
        if (index == currentCameraIndex)
            return;

        Vector3 vCamera = GridCalculator.vectorFromIndex(index, numCells, rowLength);
        vCamera.z = 1;
        Camera.main.transform.position = vCamera;
        Vector3 vPlayer = GridCalculator.vectorFromIndex(index, numCells, rowLength);
        GameObject.Find("Hero").transform.position = vPlayer;

        currentCameraIndex = index;
    }

    public void ResetGame()
    {
        difficultyLevel = 0;
    }

    // Create all the cells and put them in a List.
    // Default: 3x3 grid with three random rooms selected as the start, boss, and shop rooms.
    void SetupCells()
    {
        int[] indices = new int[numCells - 3];
        int[] randoms = new int[3];
        cellComponents = new Room_Basic[numCells];
        
        
        // generate the below fields randomly later.
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, numCells - 1);
            while (randoms.Contains(num))
                num = Random.Range(0, numCells - 1);
            randoms[i] = num;
        }
        int val = 0;
        for (int i = 0; i < numCells - 3; i++)
        {
            while (randoms.Contains(val))
                val++;
            indices[i] = val;
            val++;
        }

        Debug.Log(string.Join("", randoms));
        Debug.Log(string.Join("", indices));


        // create the starting room
        Vector3 vStart = GridCalculator.vectorFromIndex(randoms[0], numCells, rowLength);
        GameObject objStart = Instantiate(start, vStart, new Quaternion(0f, 0f, 0f, 0f));
        objStart.GetComponent<Room_Start>().SetIndex(randoms[0], rowLength, objStart);
        Debug.Log("Start index: " + randoms[0]);
        cellComponents[randoms[0]] = objStart.GetComponent<Room_Start>();
        

        // set the camera to the player in the starting room.
        Vector3 vCamera = GridCalculator.vectorFromIndex(randoms[0], numCells, rowLength);
        vCamera.z = 1;
        Camera.main.transform.position = vCamera;
        //cam.orthographicSize = 8;


        // create the boss room
        Vector3 vBoss = GridCalculator.vectorFromIndex(randoms[1], numCells, rowLength);
        GameObject objBoss = Instantiate(boss, vBoss, new Quaternion(0f, 0f, 0f, 0f));
        objBoss.GetComponent<Room_Boss>().SetIndex(randoms[1], rowLength, objBoss);
        Debug.Log("Boss index: " + randoms[1]);
        cellComponents[randoms[1]] = objBoss.GetComponent<Room_Boss>();


        // create the shop room
        Vector3 vShop = GridCalculator.vectorFromIndex(randoms[2], numCells, rowLength);
        GameObject objShop = Instantiate(shop, vShop, new Quaternion(0f, 0f, 0f, 0f));
        objShop.GetComponent<Room_Shop>().SetIndex(randoms[2], rowLength, objShop);
        Debug.Log("Shop index: " + randoms[2]);
        cellComponents[randoms[2]] = objShop.GetComponent<Room_Shop>();

        // create components for every room in the grid.
        //for (int i = 0; i < indices.Count(); i++)
        //    gameObject.AddComponent<Room_Normal>();
        //normalRoomComponents = gameObject.GetComponents<Room_Normal>();

        for (int i = 0; i < indices.Count(); i++)
        {
            Vector3 v = GridCalculator.vectorFromIndex(indices[i], numCells, rowLength);
            GameObject objBasic = Instantiate(normal, v, new Quaternion(0f, 0f, 0f, 0f));
            objBasic.GetComponent<Room_Normal>().SetIndex(indices[i], rowLength, objBasic);

            //normalRoomComponents[i].SetIndex(indices[i], rowLength, objBasic);
            Debug.Log("Setting normal room index: " + indices[i]);
            cellComponents[indices[i]] = objBasic.GetComponent<Room_Normal>();
        }
    }

    // Generate the set of adjacent cells for each cell.
    void MakeConnections()
    {
        for (int i = 0; i < numCells; i++)
        {

            Cell currentCell = cellComponents[i];
            int[] cNeighborIndices = GridCalculator.NeighborIndices(i, numCells, rowLength);
            foreach (int neighbourIndex in cNeighborIndices)
            {
                if (neighbourIndex >= 0 && neighbourIndex < numCells)
                    currentCell.addNeighbor(rowLength, cellComponents[neighbourIndex]);
            }
            
        }
    }

    void InitializeWalls()
    {
        for (int i = 0; i < cellComponents.Count(); i++)
        {
            Debug.Log("Initialize");
            cellComponents[i].SetupWalls();
        }
    }
   



}
