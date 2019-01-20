using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connections : MonoBehaviour
{
    public bool globalConnectionInitiated = false;
    public bool hasPoint1 = false, hasPoint2 = false;
    public GameObject wallPlaceholder;

    List<GridPoint[]> pointConnections = new List<GridPoint[]>();
    GameObject[] wallsCount;
    List<GameObject> walls = new List<GameObject>();

    GridPoint holderPoint1, holderPoint2;
    
    void Update()
    {
        //If a connection is being made, and both points in the handler class have been set by nodes in the grid
        if(hasPoint1 && hasPoint2)
        {
            //If the two points are perpendicular (at 90 degrees in any direction) to each other
            if(holderPoint1.transform.position.x == holderPoint2.transform.position.x || holderPoint1.transform.position.y == holderPoint2.transform.position.y)
            {
                AddConnection(holderPoint1, holderPoint2);
                holderPoint1.addToConnectedPoints(holderPoint2);
                holderPoint2.addToConnectedPoints(holderPoint1);
            }
            nullifyPoint(1); nullifyPoint(2);
        }

    }
    
    //Set the handler point being used in the active attempted connection (dynamic mutator method)
    public void setPoint(int n, GridPoint point)
    {
        if(n==1)
        {
            holderPoint1 = point;
            hasPoint1 = true;
        }
        else if(n==2)
        {
            holderPoint2 = point;
            hasPoint2 = true;
        }
    }

    //Clear the set points in the handles
    public void nullifyPoint(int n)
    {
        if(n==1)
        {
            hasPoint1 = false; holderPoint1 = null;
        }
        else if(n == 2)
        {
            hasPoint2 = false; holderPoint2 = null;
        }
    }

    //Add a wall connection between two points
    void AddConnection(GridPoint pointOne, GridPoint pointTwo)
    {
        GridPoint[] connect = { pointOne, pointTwo };
        pointConnections.Add(connect);
        GameObject newWall = Instantiate(wallPlaceholder);
        newWall.GetComponent<Wall>().intiateWall(pointOne, pointTwo);
        walls.Add(newWall);
    }

    //hasPoint1 accessor
    public bool HasPoint1()
    {
        return hasPoint1;
    }

    //hasPoint2 accessor
    public bool HasPoint2()
    {
        return hasPoint2;
    }
}
