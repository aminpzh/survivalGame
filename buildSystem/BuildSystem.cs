using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildSystem : MonoBehaviour
{
    public List<buildObjects> objects = new List<buildObjects>();
    public buildObjects currentObj;
    private Vector3 currentPos;
    public Transform currentPreview;
    public LayerMask layer;
    public GameObject buildingparent;
    public float offset = 1f;
    public float gridsize = 1f;
    public bool isSnapped = false;
    public bool inBuildingMode = false;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    previewObject PO;
    public int lastID = 0;
    public int chestID;
    public void ChangeCurrentObj(int cur)
    {
        currentObj = objects[cur];
        if (currentPreview != null)
        {
            Destroy(currentPreview.gameObject);
        }
        //Debug.Log(currentPreview);
        //Debug.Log(currentObj);
        GameObject currentPre = Instantiate(currentObj.preview, transform.position, transform.rotation) as GameObject;
        currentPreview = currentPre.transform;
    }

    public void StartPreview()
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);

        if (Physics.Raycast(ray, out hit, 15, layer))
        {
            if (hit.transform != this.transform)
            {
                ShowPreview(hit);
            }
        }
    }
    public void ShowPreview(RaycastHit hit2)
    {
        //foundation
        if (PO.type == itemtype.foundation)
        {
            var pos = hit2.transform.position;
            //Debug.Log(hit2.collider.tag);
            switch (hit2.collider.tag)
            {
                case "foundationWest":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(-6, 0, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;
                case "foundationEast":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(6, 0, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;
                case "foundationNorth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 0, 6));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;
                case "foundationSouth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 0, -6));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;
                default:
                    isSnapped = false;
                    break;
            }
        }
        //End of foundation

        //start Wall
        if (PO.type == itemtype.wall)
        {
            var pos = hit2.transform.position;

            switch (hit2.collider.tag)
            {
                case "foundationWest":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(-3f, 1.5f, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3 (0, 90, 0);
                    isSnapped = true;
                    break;
                case "foundationEast":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(3f, 1.5f, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, -90, 0);
                    isSnapped = true;
                    break;
                case "foundationNorth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 1.5f, 3f));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 180, 0);
                    isSnapped = true;
                    break;
                case "foundationSouth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 1.5f, -3f));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 0, 0);
                    isSnapped = true;
                    break;
                case "wallTop":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 6, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 0, 0);
                    isSnapped = true;
                    break;
                default:
                    isSnapped = false;
                    break;
            }
        }
        //End Wall

        //Start floor
        if (PO.type == itemtype.floor)
        {
            var pos = hit2.transform.position;

            switch (hit2.collider.tag)
            {
                case "WallToRoof1":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 6f, 3));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;
                case "WAllToRoof2":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 6f, -3));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles;
                    isSnapped = true;
                    break;

                default:
                    isSnapped = false;
                    break;
            }
        }


        //End floor

        //start stair
        if (PO.type == itemtype.stair)
        {
            var pos = hit2.transform.position;

            switch (hit2.collider.tag)
            {
                case "foundationWest":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(-3, 4.5f, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 0, 0);
                    isSnapped = true;
                    break;
                case "foundationEast":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(3, 4.5f, 0));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 180, 0);
                    isSnapped = true;
                    break;
                case "foundationNorth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 4.5f, -3));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, 90, 0);
                    isSnapped = true;
                    break;
                case "foundationSouth":
                    currentPreview.transform.position = hit2.transform.position + hit2.transform.TransformDirection(new Vector3(0, 4.5f, 3));
                    currentPreview.transform.localEulerAngles = hit2.transform.localEulerAngles + new Vector3(0, -90, 0);
                    isSnapped = true;
                    break;
                default:
                    isSnapped = false;
                    break;
            }
        }


        //end stair


        if (isSnapped == false)
        {
            currentPos = hit2.point;
            //currentPos -= Vector3.one * offset;
            //currentPos /= gridsize;
            //currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
            //currentPos *= gridsize;
            //currentPos += Vector3.one * offset;
            currentPreview.position = currentPos;
        }


        if (Input.GetButtonDown("Fire2"))
        {
            currentPreview.Rotate(new Vector3(0, 90, 0));

        }
    }

    public void Build()
    {
        //previewObject PO = currentPreview.GetComponent<previewObject>();
        //if (PO.type == itemtype.DRAWER && PO.buildable && PO.otherConditions)
        //{
        //    GameObject built = Instantiate(currentObj.prefab, currentPreview.position, currentPreview.rotation);
        //    built.transform.parent = buildingparent.transform;
        //    //built.GetComponent<chestBase>().chestID = lastID + 1;
        //    lastID += 1;
        //}
        //else if (PO.buildable && PO.otherConditions)
        //{
        //    GameObject built = Instantiate(currentObj.prefab, currentPreview.position, currentPreview.rotation);
        //    built.transform.parent = buildingparent.transform;


        //}
        GameObject built = Instantiate(currentObj.prefab, currentPreview.position, currentPreview.rotation);

    }



    void Start()
    {

        currentPreview = null;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {

        currentPreview = null;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inBuildingMode = true;
        }

        if (inBuildingMode == true && PO !=null)
        {
            StartPreview();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Build();

        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeCurrentObj(0);
            PO = currentPreview.GetComponent<previewObject>();
            PO.col.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCurrentObj(1);
            PO = currentPreview.GetComponent<previewObject>();
            PO.col.Clear();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCurrentObj(2);
            PO = currentPreview.GetComponent<previewObject>();
            PO.col.Clear();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCurrentObj(3);
            PO = currentPreview.GetComponent<previewObject>();
            PO.col.Clear();

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeCurrentObj(4);
            PO = currentPreview.GetComponent<previewObject>();
            PO.col.Clear();

        }
    }

}
[System.Serializable]
public class buildObjects 
{
    public string name;
    public GameObject prefab;
    public GameObject preview;

}