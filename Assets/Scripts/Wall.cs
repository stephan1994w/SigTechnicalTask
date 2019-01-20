using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public GridPoint point1, point2;
    LineRenderer line;
    MouseHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {

        inputHandler = GameObject.FindGameObjectWithTag("InputHandler").GetComponent<MouseHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        line.startColor = new Color(0.6f, 0.6f, 0.6f);
        line.endColor = new Color(0.6f, 0.6f, 0.6f);
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

    private void OnMouseOver()
    {
        if (inputHandler.currentMode == MouseHandler.ModeType.DELETE)
        {
            changeLineColour(Color.white);
        }
    }

    //private void OnMouseDrag()
    //{
    //    changeLineColour(Color.cyan);

    //    //Snap on drag
    //    //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //transform.position = new Vector3(Mathf.Round(worldPos.x), Mathf.Round(worldPos.y), worldPos.z);

    //}

    private void OnMouseExit()
    {
        changeLineColour(new Color(0.6f, 0.6f, 0.6f));
    }
    private void OnMouseUp()
    {
        changeLineColour(new Color(0.6f, 0.6f, 0.6f));
    }

    void changeLineColour(Color newColor)
    {
        line.startColor = newColor;
        line.endColor = newColor;
    }
}
