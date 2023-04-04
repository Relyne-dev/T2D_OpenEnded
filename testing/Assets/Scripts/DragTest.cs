using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTest : MonoBehaviour
{
    Plane dragPlane;

    Vector3 offset;

    Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
    }


    void OnMouseDown()
    {
        {
            dragPlane = new Plane(mainCamera.transform.forward, transform.position);
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(cameraRay, out planeDist);
            offset = transform.position - cameraRay.GetPoint(planeDist);
        }
        }

    void OnMouseDrag()
        {
            {
                Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

                float planeDist;
                dragPlane.Raycast(cameraRay, out planeDist);
                transform.position = cameraRay.GetPoint(planeDist) + offset;
            }
        }
    }

