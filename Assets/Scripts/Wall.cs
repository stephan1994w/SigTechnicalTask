using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GridPoint point1, point2;
    LineRenderer line;
    Color startColor = new Color(0.5f, 0.5f, 0.5f);
    MouseHandler inputHandler;
    
    void Start()
    {
        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<MouseHandler>();
    }

    //Create a wall between 2 Grid points, using a line renderer, and give it a collider to fit the size of the wall
    public void intiateWall(GridPoint p1, GridPoint p2)
    {
        line = GetComponent<LineRenderer>();
        point1 = p1;
        point2 = p2;
        Vector3 startPos = point1.gameObject.transform.position;
        Vector3 endPos = point2.gameObject.transform.position;

        //Set up the line renderer
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.15F;
        line.endWidth = 0.15F;
        line.positionCount = 2;
        line.startColor = startColor;
        line.endColor = startColor;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

        //Set up the collider of the line
        BoxCollider lineCollider = gameObject.AddComponent<BoxCollider>();
        float lineWidth = line.endWidth;
        float lineLength = Vector3.Distance(startPos, endPos);
        Vector3 midPoint = (startPos + endPos) / 2;
        float angle = Mathf.Atan2(startPos.y - endPos.y, startPos.x - endPos.x) * 180 / Mathf.PI;

        lineCollider.size = new Vector3(lineLength, lineWidth, 0.2f);
        lineCollider.transform.position = midPoint;
        lineCollider.center = Vector3.zero;
        lineCollider.transform.Rotate(0, 0, angle);
    }

    //On mouse over, if the mode is set to "Delete", highlight wall red
    private void OnMouseOver()
    {
        if (inputHandler.currentMode == MouseHandler.ModeType.DELETE)
        {
            changeLineColour(Color.red);
        }
    }

    //Revert to base colour on mouse exit
    private void OnMouseExit()
    {
        changeLineColour(startColor);
    }

    //Change line colour to specified colour;
    void changeLineColour(Color newColor)
    {
        line.startColor = newColor;
        line.endColor = newColor;
    }
}
