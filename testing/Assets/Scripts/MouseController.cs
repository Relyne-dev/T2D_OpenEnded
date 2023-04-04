using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    bool inPlay = false;
    bool selected = false;
    int health = 5;
    int strength = 5;
    int cost = 5;
    int resource = 0;

    // Dragging variables
    Plane dragPlane;
    Vector3 offset;
    Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 39f));

        if (Mouse.current.leftButton.wasPressedThisFrame && selected == true)
        {
            GameObject.Find("Player").GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            selected = false;
            Debug.Log("Deselected");
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && selected == false)
        {
            CheckHitBox();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            resource += 1;
            Debug.Log("+1 Resource (" + resource + ")");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && selected == true)
        {
            health += 1;
            Debug.Log("+1 Health (" + health + ")");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && selected == true)
        {
            health -= 1;
            Debug.Log("-1 Health (" + health + ")");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && selected == true)
        {
            strength += 1;
            Debug.Log("+1 Strength (" + strength + ")");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && selected == true)
        {
            strength -= 1;
            Debug.Log("-1 Strength (" + strength + ")");
        }


    }

    void CheckHitBox()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 39f));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(worldPosition, Vector3.forward, Color.green);
        if (Physics.Raycast(ray, out hit, 49f))
        {
            Cost();
        }
    }

    void Cost()
    {
        if (resource >= cost && inPlay == false)
        {
            Debug.Log("Played card");
            inPlay = true;
            resource -= cost;
            GameObject.Find("Player").GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
        else if (resource < cost && inPlay == false)
        {
            Debug.Log("Not enough resources");
        }
        else
        {
            Debug.Log("Selected");
            selected = true;
            GameObject.Find("Player").GetComponent<Renderer>().material.color = new Color(0, 204, 0);
        }
    }

    void OnMouseDown()
    {
            dragPlane = new Plane(mainCamera.transform.forward, transform.position);
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(cameraRay, out planeDist);
            offset = transform.position - cameraRay.GetPoint(planeDist);
    }

    void OnMouseDrag()
    {
        if (inPlay == true){
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(cameraRay, out planeDist);
            transform.position = cameraRay.GetPoint(planeDist) + offset;
        }
    }

}
