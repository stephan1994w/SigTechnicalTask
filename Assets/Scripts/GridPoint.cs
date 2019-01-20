using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour {

    public GameObject GridLineWithPivot;

    Material m_Material;
    Color startColor;
    LineRenderer line;
    Vector3 displacedPos;
    float displacement = -0.2F;
    bool localConnectionBeingInitiated = false;
    Connections connectionHandler;
    MouseHandler inputHandler;

    List<Wall> connectedWalls = new List<Wall>();
    List<GridPoint> connectedPoints = new List<GridPoint>();

    void Start () {
        //Find the connection and input handlers
        connectionHandler = GameObject.FindGameObjectWithTag("Connections").GetComponent<Connections>();
        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<MouseHandler>();

        //Set up the position of the point, and the line renderer that will be used to show the user what they're drawing
        displacedPos = new Vector3(transform.position.x, transform.position.y, displacement);
        m_Material = GetComponent<Renderer>().material;
        startColor = m_Material.color;
        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.1F;
        line.endWidth = 0.1F;
        line.positionCount = 2;
        line.SetPosition(0, displacedPos);
    }

	void Update () {
        //Draw the green line displaying the users current input, when the DRAW mode is selected
        if (inputHandler.currentMode == MouseHandler.ModeType.DRAW)
        {
            if (localConnectionBeingInitiated)
            {
                m_Material.color = Color.green;
                line.startColor = Color.green;
                line.endColor = Color.green;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = displacement;
                line.SetPosition(1, mousePos);
            }
            else
            {
                line.SetPosition(1, transform.position);
            }
        }
	}
    
    //Initiate global connection when a node is clicked, so that other nodes know not to react, and initiate local connection
    void OnMouseDown()
    {
        connectionHandler.globalConnectionInitiated = true;        
        localConnectionBeingInitiated = true;
    }

    //If draw mode is set, if the user is creating a connection, set the target point of the connection
    void OnMouseOver()
    {
        if(inputHandler.currentMode == MouseHandler.ModeType.DRAW)
        {
            if (!localConnectionBeingInitiated && connectionHandler.globalConnectionInitiated && !connectionHandler.HasPoint2())
            {
                connectionHandler.setPoint(2, this);
            }
            m_Material.color = Color.cyan;
        }
    }

    //If the user tries to make a connection, and the target point has been set in the connection handler, set this point as point 1
    void OnMouseUp()
    {
        if(localConnectionBeingInitiated && connectionHandler.hasPoint2)
        {
            connectionHandler.setPoint(1, this);
        }

        localConnectionBeingInitiated = false;
        connectionHandler.globalConnectionInitiated = false;
        m_Material.color = startColor;
    }

    //Ensure that if a user doesn't "MouseUp" on a point that it's no longer the target point
    void OnMouseExit()
    {
        if(!localConnectionBeingInitiated)
        {
            connectionHandler.nullifyPoint(2);
            m_Material.color = startColor;
        }
    }

    //Add wall to walls list
    public void addWallToPoint(Wall newWall)
    {
        connectedWalls.Add(newWall);
    }

    //Add point to connected points list
    public void addToConnectedPoints(GridPoint point)
    {
        connectedPoints.Add(point);
    }
}
