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
    
    // Use this for initialization
    void Start () {
        connectionHandler = GameObject.FindGameObjectWithTag("Connections").GetComponent<Connections>();
        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<MouseHandler>();

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
	
	// Update is called once per frame
	void Update () {
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


    void OnMouseDown()
    {
        connectionHandler.globalConnectionInitiated = true;        
        localConnectionBeingInitiated = true;
    }

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

    void OnMouseExit()
    {
        if(!localConnectionBeingInitiated)
        {
            connectionHandler.nullifyPoint(2);
            m_Material.color = startColor;
        }
    }

    public void addWallToPoint(Wall newWall)
    {
        connectedWalls.Add(newWall);
    }

    public void addToConnectedPoints(GridPoint point)
    {
        connectedPoints.Add(point);
    }
}
