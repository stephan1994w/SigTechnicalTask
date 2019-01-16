using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCorner : MonoBehaviour {

    public GameObject GridLineWithPivot;

    Material m_Material;
    Color startColor;

    // Use this for initialization
    void Start () {
        m_Material = GetComponent<Renderer>().material;
        startColor = m_Material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        m_Material.color = Color.blue;
    }

    void OnMouseDown()
    {
        m_Material.color = Color.green;
    }

    void OnMouseExit()
    {
        m_Material.color = startColor;
    }

    void OnMouseUp()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 center = new Vector3(transform.position.x + mousePos.x, transform.position.y + mousePos.y) / 2f;
        //float scaleX = Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(mousePos.x, 0, 0));
        //float scaleY = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, mousePos.y, 0));
        Instantiate(GridLineWithPivot, center, Quaternion.identity);

        m_Material.color = startColor;
    }

    private void OnMouseDrag()
    {
        m_Material.color = Color.green;
    }

    Transform GetClosestPoint(Transform[,] points)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in points)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
