using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class dragObjects : MonoBehaviour
{

    public RaycastHit hit;
    public Transform currentObj;
    public Vector3 position;

    void Update()
    {

        // Check for touch input
        if (Input.touchCount > 0)
        {
            print("touch");

            Touch touch = Input.GetTouch(0);


            // Check if touch phase began
            if (touch.phase == TouchPhase.Began)
            {
                print("began touch ");

                position = Input.GetTouch(0).position;
                // Cast a ray from the camera to the touch position.


                Ray ray = Camera.main.ScreenPointToRay(position);
                print("ray cast formed  ");


                // Check if the ray hits the target gameobject.
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    print(hit.collider.name);
                    if (hit.collider.CompareTag("DragableObject"))
                    {
                        print("hitted dragableobject");

                        print(hit.transform.name);
                        // If the ray hits the target, assign the current object.
                        currentObj = hit.transform;

                    }


                }
            }
            // Check if touch phase moved and there is a current object
            if (touch.phase == TouchPhase.Moved && currentObj != null)
            {
                // Update the position of the current object based on touch position.
                position = touch.position;
                currentObj.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 30));
                currentObj.rotation = Quaternion.Euler(1f, 1f, 1f);
            }
            // Check if touch phase ended or canceled
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // Clear the current object reference.
                currentObj = null;
            }
        }
    }
}




