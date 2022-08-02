using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewObject : MonoBehaviour
{
    public List<Collider> col = new List<Collider>();
    public Material red;
    public Material yellow;
    public bool buildable;
    protected foundationBotCollider onFloorScr;
    protected wallBotCollider onFloorWall;
    protected roofConnectCheck[] roofConnected;
    protected stairConnector[] stairConnected;
    public itemtype type;
    public bool otherConditions = true;

    // Start is called before the first frame update
    void Start()
    {

        if (type == itemtype.foundation)
        {
            onFloorScr = GetComponentInChildren<foundationBotCollider>();
        }
        else if (type == itemtype.wall)
        {
            onFloorWall = GetComponentInChildren<wallBotCollider>();
        }
        else if (type == itemtype.floor)
        {
            roofConnected = GetComponentsInChildren<roofConnectCheck>();
        }
        else if (type == itemtype.stair)
        {
            stairConnected = GetComponentsInChildren<stairConnector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        if (type == itemtype.foundation)
        {
            otherConditions = onFloorScr.OnFloor;
        }
        else if (type == itemtype.wall)
        {
            otherConditions = onFloorWall.onBase;
        }
        else if (type == itemtype.floor)
        {
            for (int i = 0; i < 4; i++)
            {
                if (roofConnected[0].Connected == false && roofConnected[1].Connected == false && roofConnected[2].Connected == false && roofConnected[3].Connected == false)
                {
                    otherConditions = false;
                }
                else
                {
                    otherConditions = true;
                }
                

            }
        }
        else if (type == itemtype.stair)
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.Log(stairConnected.Length);
                if (stairConnected[0].Connected == true && stairConnected[1].Connected == true)
                {
                    otherConditions = true;
                }
                else
                {
                    otherConditions = false;
                }
            }
        }
        else
        {
            otherConditions = true;
        }
        if (buildable ==true && otherConditions == true)
        {
            GetComponentInChildren<MeshRenderer>().material = yellow;

        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material = red;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 10 && other.gameObject.layer != 14)
        {
            col.Add(other);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 10 && other.gameObject.layer != 14)
        {
            col.Remove(other);
        }

    }
    void ChangeColor()
    {
        if (col.Count == 0)
        {
            buildable = true;
        }
        else
        {
            buildable = false;
        }
    }
}
public enum itemtype
{
    foundation,
    wall,
    DRAWER,
    furnace,
    stair,
    floor
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FoundationCollider : MonoBehaviour
//{

//    Foundation foundationScript;
//    Vector3 sizeOfFoundation;

//    // Use this for initialization
//    void Start()
//    {
//        foundationScript = transform.parent.GetComponent<Foundation>();
//        sizeOfFoundation = transform.parent.GetComponent<Collider>().bounds.size;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Snapping ability
//        if (BuildingManager.isBuilding && other.tag == "Foundation" && foundationScript.isPlaced && !other.GetComponent<Foundation>().isSnapped)
//        {
//            other.GetComponent<Foundation>().isSnapped = true;

//            float sizeX = sizeOfFoundation.x;
//            float sizeZ = sizeOfFoundation.z;

//            switch (this.transform.tag)
//            {
//                case "WestCollider":
//                    other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, 0, transform.parent.position.z);
//                    break;
//                case "EastCollider":
//                    other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, 0, transform.parent.position.z);
//                    break;
//                case "NorthCollider":
//                    other.transform.position = new Vector3(transform.parent.parent.position.x, 0, transform.parent.position.z + sizeZ);
//                    break;
//                case "SouthCollider":
//                    other.transform.position = new Vector3(transform.parent.parent.position.x, 0, transform.parent.position.z + sizeZ);
//                    break;
//            }
//        }
//    }
//}