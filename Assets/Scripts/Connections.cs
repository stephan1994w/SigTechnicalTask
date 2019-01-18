using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connections : MonoBehaviour
{
    public bool globalConnectionInitiated = false;
    public bool hasPoint1 = false, hasPoint2 = false;
    public GameObject wallPlaceholder;

    List<GridCorner[]> pointConnections = new List<GridCorner[]>();
    GameObject[] wallsCount;
    List<GameObject> walls = new List<GameObject>();

    GridCorner holderPoint1, holderPoint2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPoint1 && hasPoint2)
        {
            AddConnection(holderPoint1, holderPoint2);
            nullifyPoint(1); nullifyPoint(2);
        }

    }

    void LateUpdate()
    {
    }

    public void setPoint(int n, GridCorner point)
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

    void AddConnection(GridCorner pointOne, GridCorner pointTwo)
    {
        GridCorner[] connect = { pointOne, pointTwo };
        pointConnections.Add(connect);
        Debug.Log("Connection added from: " + pointOne.transform.position + " to " + pointTwo.transform.position);
        RenderPipeline();
    }

    void RenderConnection(GameObject renderer, Vector3 pos1, Vector3 pos2)
    {
        LineRenderer line = renderer.GetComponent<LineRenderer>();
        line.startWidth = 0.15F;
        line.endWidth = 0.15F;
        line.positionCount = 2;
        line.startColor = Color.cyan;
        line.endColor = Color.cyan;

        Debug.Log("Pos1: " + pos1);
        Debug.Log("Pos2: " + pos2);
        line.SetPosition(0, pos1);
        line.SetPosition(1, pos2);
    }

    void RenderPipeline()
    {
        wallsCount = GameObject.FindGameObjectsWithTag("Walls");
        if (wallsCount.Length < pointConnections.Count)
        {
            GameObject newWall = new GameObject("Wall");
            newWall.AddComponent<LineRenderer>();
            newWall.tag = "Walls";
            walls.Add(newWall);
        }
        RenderWalls();
    }

    void RenderWalls()
    {
        for(int i = 0; i < walls.Count; i++)
        {
            RenderConnection(walls[i], pointConnections[i][0].gameObject.transform.position, pointConnections[i][1].gameObject.transform.position);
        }
    }

    public bool HasPoint1()
    {
        return hasPoint1;
    }
    public bool HasPoint2()
    {
        return hasPoint2;
    }
}
