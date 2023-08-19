using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyingobject : MonoBehaviour
{
    public GameObject[] objectsOnDestination = new GameObject[2]; // Array of objects on the final destination
    animationController animationController;
    public GameObject toDestroy;
    public bool isAnimating;
    public Transform leftPosition; // Reference to the left position
    public Transform rightPosition; // Reference to the right position
    public Transform center;
    public GameObject object1;
    public GameObject object2;

    private void Awake()
    {
        animationController = toDestroy.GetComponent<animationController>();
        
    }
    private void Update()
    {
        if (objectsOnDestination[0] == null)
        {
          //  print(" zero position is null now ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DragableObject"))
        {
            GameObject draggableObject = other.gameObject;
            

            if (objectsOnDestination[0] == null)
            {
                // Set the first object in the array
                objectsOnDestination[0] = draggableObject;
                

            }
            else if (objectsOnDestination[1] == null)
            {
                // Set the second object in the array
                objectsOnDestination[1] = draggableObject;
               

                  object1 = objectsOnDestination[0];
               
                  object2 = objectsOnDestination[1];
                

                // Check if both objects are of the same type
                if (object1.name == object2.name)
                {
                    //StartCoroutine(DestroyObjectsWithDelay(object1, object2, 1f));
                    // Destroy both objects


                   
                    object1.transform.position = Vector3.MoveTowards( object1.transform.position,center.position,0.2f);
                    object2.transform.position = Vector3.MoveTowards(object2.transform.position, center.position, 0.2f);

                    //object2.transform.position = center.position;

                    object1.transform.localScale += new Vector3(0.2f,0.2f,0.2f);

                    Invoke("destruction", 0.5f);
                }
                else 
                {
                    // Objects are not of the same type, remove the second object from the array
                    objectsOnDestination[1] = null;

                    Rigidbody rb = object2.GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 750, ForceMode.Impulse);

                    //Debug.Log("Two objects of different types are on the final destination. Resetting count.");
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DragableObject"))
        {
            GameObject draggableObject = other.gameObject;

            if (objectsOnDestination[0] == draggableObject)
            {
                Rigidbody rb = draggableObject.GetComponent<Rigidbody>();
                // Set isKinematic to true to disable physics simulation
                rb.isKinematic = true;
                // Set the position of object1 to the left position
                draggableObject.transform.position = leftPosition.position;

                // Get the Rigidbody component of object1

            }
            else if (objectsOnDestination[1] == draggableObject)
            {
                Rigidbody rb = draggableObject.GetComponent<Rigidbody>();
                // Set isKinematic to true to disable physics simulation
                rb.isKinematic = true;
                // Set the position of object2 to the right position
                draggableObject.transform.position = rightPosition.position;
            }
        }
    }
    
    private void destruction()
    {
        Destroy(object1);
        Destroy(object2);
        isAnimating = true;
        //print("called method destroyanim");
        animationController.destroyAnim();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DragableObject"))
        {
            GameObject draggableObject = other.gameObject;

            if (objectsOnDestination[0] == draggableObject)
            {
                // Remove the first object from the array
                objectsOnDestination[0] = null;
                Rigidbody rb = draggableObject.GetComponent<Rigidbody>();
                // Set isKinematic to false to enable physics simulation
                rb.isKinematic = false;

            }
            else if (objectsOnDestination[1] == draggableObject)
            {
                // Remove the second object from the array
                objectsOnDestination[1] = null;
                Rigidbody rb = draggableObject.GetComponent<Rigidbody>();
                // Set isKinematic to false to enable physics simulation
                rb.isKinematic = false;
            }
        }
    }
}
