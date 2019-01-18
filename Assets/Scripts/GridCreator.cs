using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour {


    public GameObject GridSquare;
    public GameObject GridCorners;
    public GameObject connections;
    public int GridXSize;
    public int GridYSize;

    float xDisplacement, yDisplacement;

    GameObject[,] grid;

    GameObject[,] corners;

    // Use this for initialization
    void Start () {
        xDisplacement = (float)GridXSize / 2;

        yDisplacement = (float)GridYSize / 2;

        generateTiles();

    }
	
	void LateUpdate () {

    }

    void generateTiles()
    {
        grid = new GameObject[GridXSize, GridYSize];

        for (int i = 0; i < GridXSize; i++)
        {
            for(int j = 0; j < GridYSize; j++)
            {
                grid[i, j] = Instantiate(GridSquare, new Vector3((i - xDisplacement) * 2 , (j - yDisplacement) * 2, 0), Quaternion.identity);
            }
        }

        corners = new GameObject[GridXSize + 1, GridYSize + 1];
        for (int i = 0; i <= GridXSize; i++)
        {
            for (int j = 0; j <= GridYSize; j++)
            {
                corners[i, j] = Instantiate(GridCorners, new Vector3((i - xDisplacement) * 2, (j - yDisplacement) * 2, -0.2F), Quaternion.identity);
            }
        }
    }
}
