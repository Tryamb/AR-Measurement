 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public ARCameraManager arCamera;
    public LineRenderer line;
    private List<ARRaycastHit> hitPoint = new List<ARRaycastHit>();
    public GameObject Mark;
    public GameObject point;
    public Text length_print;
    public Text message;
    public GameObject mobileImg;
    public TextMesh thisDistance;
    public TextMesh thisDistance1;
    private GameObject p1;
    private GameObject p2;
    private LineRenderer l1;
    private GameObject startpoint;
    private GameObject endpoint;
    float length = 0.0f;
    int count = 0;  // it is declared to store the distance between two point
    bool pointgenerate = false;
    bool iscomplete = false;
    bool deleteStatus = false;  //this bool is declared to check whether delete button is pressed or not. 
    //if delete button is pressed instantiated objects will be deleted but if it is pressed again it will show error because now there is nothing to delete. so first click the mark to instantiate point/line then again click delete.
    float unitConverter =1.0f;
    string unit = " m";
    public TMP_Dropdown d1;
    public TMP_Dropdown d2;

    public void Update()
    {
        arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitPoint, TrackableType.Planes);
        if(hitPoint.Count>0)
        {
            mobileImg.SetActive(false);
            message.gameObject.SetActive(false);
            Mark.transform.position = hitPoint[0].pose.position;
            Mark.transform.rotation = hitPoint[0].pose.rotation;
        }
        else
        {
            mobileImg.SetActive(true);
            message.gameObject.SetActive(true);
            mobileImg.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        }
        if(pointgenerate==true)
        {
            l1.SetPosition(1, Mark.transform.position);
            endpoint = Mark;
        }
        if (iscomplete == true)
        {
            length = Vector3.Distance(startpoint.transform.position, endpoint.transform.position);
            thisDistance1.transform.position = (startpoint.transform.position + endpoint.transform.position) / 2;
            thisDistance1.transform.rotation = arCamera.transform.rotation;
        }
        if (deleteStatus == false)
        {
            thisDistance1.text = (length * unitConverter).ToString("F3") + unit;
        }
        length_print.text = (length*unitConverter).ToString("F3")+unit;
    }
    public void btnPressed()
    {
        deleteStatus = false;
        if (pointgenerate == false)
        {
                p1 = Instantiate(point, Mark.transform.position, Quaternion.identity);
                startpoint = p1;
                l1 = Instantiate(line);
                l1.SetPosition(0, p1.transform.position);
                thisDistance1 = Instantiate(thisDistance, p1.transform.position, Quaternion.identity);
                pointgenerate = true;
                iscomplete = true;
        }
        else
        {
                p2 = Instantiate(point, Mark.transform.position, Quaternion.identity);
                l1.SetPosition(1, p2.transform.position);
                endpoint.transform.position = p2.transform.position;
                pointgenerate = false;
                iscomplete = false;
        }
        count = 1;
    }
    public void changing_input()
    {
        if(d1.value==0)
        {
            unitConverter = 1.0f;
            unit = " m";
        }
        else if(d1.value == 1)
        {
            unitConverter = 100.0f;
            unit = " cm";
        }
        else if(d1.value == 2)
        {
            unitConverter = 39.37f;
            unit = " in";
        }
        else
        {
            unitConverter = 3.28f;
            unit = " ft";
        }
    }

    public void exit()
    {
        Application.Quit();
    }
    public void delete(string tag)
    {
        if (deleteStatus == false && count == 1)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
            }
            deleteStatus = true;
            iscomplete = false;
            pointgenerate = false;
            length = 0.000f;
        }
    }
    public void colorChange()
    {
        if (count == 1)
        {
            if (d2.value == 0)
            {
                Color c1 = Color.green;
                Color c2 = Color.green;
                l1.SetColors(c1, c2);
            }
            else if (d2.value == 1)
            {
                Color c1 = Color.red;
                Color c2 = Color.red;
                l1.SetColors(c1, c2);
            }
            else if (d2.value == 2)
            {
                Color c1 = Color.white;
                Color c2 = Color.white;
                l1.SetColors(c1, c2);
            }
            else if (d2.value == 3)
            {
                Color c1 = Color.black;
                Color c2 = Color.black;
                l1.SetColors(c1, c2);
            }
            else
            {
                Color c1 = Color.blue;
                Color c2 = Color.blue;
                l1.SetColors(c1, c2);
            }
        }
    }
}