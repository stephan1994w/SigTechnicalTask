using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCorner : MonoBehaviour {

    public GameObject GridLineWithPivot;

    Material m_Material;
    Color startColor;
    Connections connectionHandler;

    LineRenderer line;
    Vector3 displacedPos;
    float displacement=-0.2F;
    
    bool localConnectionBeingInitiated = false;

    // Use this for initialization
    void Start () {
        connectionHandler = GameObject.FindGameObjectWithTag("Connections").GetComponent<Connections>();

        displacedPos = new Vector3(transform.position.x, transform.position.y, displacement);
        m_Material = GetComponent<Renderer>().material;
        startColor = m_Material.color;
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.05F;
        line.endWidth = 0.05F;
        line.positionCount = 2;
        line.SetPosition(0, displacedPos);
    }
	
	// Update is called once per frame
	void Update () {
		if(localConnectionBeingInitiated)
        {
            m_Material.color = Color.green;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = displacement;
            line.SetPosition(1, mousePos);
        }
        else
        {
            line.SetPosition(1, transform.position);
        }
	}


    void OnMouseDown()
    {
        connectionHandler.globalConnectionInitiated = true;        
        localConnectionBeingInitiated = true;
    }

    void OnMouseOver()
    {
        if (!localConnectionBeingInitiated && connectionHandler.globalConnectionInitiated && !connectionHandler.HasPoint2())
        {
            connectionHandler.setPoint(2, this);
        }
        m_Material.color = Color.blue;
    }

    void OnMouseUp()
    {
        //Debug.Log("test1");
        //if (!localConnectionBeingInitiated && connectionHandler.globalConnectionInitiated)
        //{
        //    connectionHandler.setPoint(2, this);

        //    connectionHandler.globalConnectionInitiated = false;
        //}

        if(localConnectionBeingInitiated)
        {
            connectionHandler.setPoint(1, this);
            connectionHandler.globalConnectionInitiated = false;
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
    void TerminateConnection()
    {

    }
}
