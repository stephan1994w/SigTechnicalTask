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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPoint1 && hasPoint2)
        {
            if(holderPoint1.transform.position.x == holderPoint2.transform.position.x || holderPoint1.transform.position.y == holderPoint2.transform.position.y)
            {
                AddConnection(holderPoint1, holderPoint2);
                holderPoint1.addToConnectedPoints(holderPoint2);
                holderPoint2.addToConnectedPoints(holderPoint1);


            }
            nullifyPoint(1); nullifyPoint(2);
        }

    }

    void LateUpdate()
    {

    }

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

    void AddConnection(GridPoint pointOne, GridPoint pointTwo)
    {
        GridPoint[] connect = { pointOne, pointTwo };
        pointConnections.Add(connect);
        Debug.Log("Connection added from: " + pointOne.transform.position + " to " + pointTwo.transform.position);
        GameObject newWall = Instantiate(wallPlaceholder);
        newWall.GetComponent<Wall>().intiateWall(pointOne, pointTwo);
        walls.Add(newWall);
    }

    public bool HasPoint1()
    {
        return hasPoint1;
    }
    public bool HasPoint2()
    {
        return hasPoint2;
    }

    //private void AddColliderToWall(GameObject wall, LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    //{
    //    BoxCollider lineCollider = wall.AddComponent<BoxCollider>();

    //    float lineWidth = line.endWidth;
    
    //    float lineLength = Vector3.Distance(startPoint, endPoint);

    //    lineCollider.size = new Vector3(lineLength, lineWidth, 0.2f);

    //    Vector3 midPoint = (startPoint + endPoint) / 2;

    //    lineCollider.transform.position = midPoint;
    //    lineCollider.center = Vector3.zero;

    //    float angle = Mathf.Atan2(startPoint.y - endPoint.y, startPoint.x - endPoint.x) * 180 / Mathf.PI;
        
    //    lineCollider.transform.Rotate(0, 0, angle);
    //}
    //void RenderConnection(GameObject renderer, Vector3 pos1, Vector3 pos2)
    //{
    //    LineRenderer line = renderer.GetComponent<LineRenderer>();
    //    line.material = new Material(Shader.Find("Sprites/Default"));
    //    line.startWidth = 0.3F;
    //    line.endWidth = 0.3F;
    //    line.positionCount = 2;
    //    line.startColor = new Color(0.6f, 0.6f, 0.6f);
    //    line.endColor = new Color(0.6f, 0.6f, 0.6f);

    //    Debug.Log("Pos1: " + pos1);
    //    Debug.Log("Pos2: " + pos2);
    //    line.SetPosition(0, pos1);
    //    line.SetPosition(1, pos2);
    //    if (renderer.GetComponent<BoxCollider>() == null)
    //    {
    //        AddColliderToWall(renderer, line, pos1, pos2);
    //    }
    //}

    //void RenderPipeline()
    //{
    //    wallsCount = GameObject.FindGameObjectsWithTag("Walls");
    //    if (wallsCount.Length < pointConnections.Count)
    //    {
    //        GameObject newWall = new GameObject("Wall");
    //        newWall.AddComponent<LineRenderer>();
    //        newWall.tag = "Walls";
    //        walls.Add(newWall);

    //    }
    //    RenderWalls();
    //}

    //void RenderWalls()
    //{
    //    for (int i = 0; i < walls.Count; i++)
    //    {
    //        RenderConnection(walls[i], pointConnections[i][0].gameObject.transform.position, pointConnections[i][1].gameObject.transform.position);
    //    }
    //}
}
