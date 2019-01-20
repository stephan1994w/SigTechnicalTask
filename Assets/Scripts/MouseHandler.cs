using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MouseHandler : MonoBehaviour
{
    public Button DrawButton, MoveButton, AddButton, DeleteButton;
    public Dropdown itemsDropdown;
    public GameObject scrollHelper;
    public GameObject[] items;

    bool hasItem = false;
    GameObject currentItem;
    Vector3 worldPos;

    public enum ModeType
    {
        DRAW,
        MOVE,
        ADD,
        DELETE
    }

    public ModeType currentMode;

    // Start is called before the first frame update
    void Start()
    {
        DrawButton.GetComponent<Image>().color = Color.green;
        currentMode = ModeType.DRAW;
        DrawButton.onClick.AddListener(delegate { SetMode(ModeType.DRAW); });
        MoveButton.onClick.AddListener(delegate { SetMode(ModeType.MOVE); });
        AddButton.onClick.AddListener(delegate { SetMode(ModeType.ADD); });
        DeleteButton.onClick.AddListener(delegate { SetMode(ModeType.DELETE); });
        itemsDropdown.onValueChanged.AddListener(delegate {changeItem();});
    }

    private void changeItem()
    {
        GameObject holder = currentItem;
        Destroy(holder);
        currentItem = Instantiate(items[itemsDropdown.value], worldPos, Quaternion.identity);
        hasItem = true;
    }

    void SetMode(ModeType newMode)
    {
        currentMode = newMode;

        if(newMode == ModeType.DRAW)
        {
            DrawButton.GetComponent<Image>().color = Color.green;
            AddButton.GetComponent<Image>().color = Color.white;
            MoveButton.GetComponent<Image>().color = Color.white;
            DeleteButton.GetComponent<Image>().color = Color.white;
            itemsDropdown.gameObject.SetActive(false);
            scrollHelper.SetActive(false);
        }
        else if (newMode == ModeType.ADD)
        {
            DrawButton.GetComponent<Image>().color = Color.white;
            AddButton.GetComponent<Image>().color = Color.green;
            MoveButton.GetComponent<Image>().color = Color.white;
            DeleteButton.GetComponent<Image>().color = Color.white;
            itemsDropdown.gameObject.SetActive(true);
            scrollHelper.SetActive(true);
        } else if (newMode == ModeType.MOVE)
        {
            DrawButton.GetComponent<Image>().color = Color.white;
            AddButton.GetComponent<Image>().color = Color.white;
            MoveButton.GetComponent<Image>().color = Color.green;
            DeleteButton.GetComponent<Image>().color = Color.white;
            itemsDropdown.gameObject.SetActive(false);
            scrollHelper.SetActive(false);
        }
        else if (newMode == ModeType.DELETE)
        {
            DrawButton.GetComponent<Image>().color = Color.white;
            AddButton.GetComponent<Image>().color = Color.white;
            MoveButton.GetComponent<Image>().color = Color.white;
            DeleteButton.GetComponent<Image>().color = Color.green;
            itemsDropdown.gameObject.SetActive(false);
            scrollHelper.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = -0.3f;
        if (currentMode == ModeType.ADD)
        {
            if(!hasItem)
            {
                currentItem = Instantiate(items[itemsDropdown.value], worldPos, Quaternion.identity);
                hasItem = true;
            }
            else
            {
                currentItem.transform.position = new Vector3(Mathf.Round(worldPos.x*2)/2, Mathf.Round(worldPos.y*2)/2, worldPos.z);

                if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                {
                    currentItem.transform.Rotate(0,0,90);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                {

                    currentItem.transform.Rotate(0, 0, -90);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (hasItem && !EventSystem.current.IsPointerOverGameObject())
                    {
                        currentItem.transform.position = new Vector3(Mathf.Round(worldPos.x*2)/2, Mathf.Round(worldPos.y*2)/2, worldPos.z);
                        currentItem = null;
                        hasItem = false;
                    }
                }
            }
        }
        else
        {
            GameObject holder = currentItem;
            Destroy(holder);
            hasItem = false;
        }

        if (currentMode == ModeType.DELETE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
