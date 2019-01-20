using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour {
    public GameObject GridSquare;
    public GameObject GridPoints;
    public GameObject[,] points;
    public GameObject connections;
    public int GridXSize;
    public int GridYSize;
    float xDisplacement, yDisplacement;
    GameObject[,] grid;

    void Start () {
        //Center the grid on the screen
        xDisplacement = (float)GridXSize / 2;
        yDisplacement = (float)GridYSize / 2;
        generateTiles();
    }

    
    void generateTiles()
    {
        //Create the tiled background for line references
        grid = new GameObject[GridXSize, GridYSize];
        for (int i = 0; i < GridXSize; i++)
        {
            for(int j = 0; j < GridYSize; j++)
            {
                grid[i, j] = Instantiate(GridSquare, new Vector3((i - xDisplacement) * 2 , (j - yDisplacement) * 2, 0), Quaternion.identity);
            }
        }

        //Create the grid points that will be clicked
        points = new GameObject[GridXSize + 1, GridYSize + 1];
        for (int i = 0; i <= GridXSize; i++)
        {
            for (int j = 0; j <= GridYSize; j++)
            {
                points[i, j] = Instantiate(GridPoints, new Vector3((i - xDisplacement) * 2, (j - yDisplacement) * 2, -0.2F), Quaternion.identity);
            }
        }
    }
}
