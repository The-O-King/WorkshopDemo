using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public GameObject nearby = null; // The object that we can pickup that is nearby
    public Transform holdPosition;   // The position where held objects should be kept
    public FixedJoint holdJoint;     // The Fixed Joint that will allow us to attach the object the controller
    public bool right = false;       // Whether this is the left or right hand
    private string inputName;        // The string defining what the input name for this controller is



	// Use this for initialization
	void Start () {
        // Decides which input to use based on the hand
        if (right)
        {
            inputName = "Right Pickup";
        }
        else
        {
            inputName = "Left Pickup";
        }

        // Gets the Fixed Joint component to allow us to attach a pickup-able object
        holdJoint = GetComponent<FixedJoint>();
    }
	
	// Update is called once per frame
	void Update () {
        // If the trigger has been pressed down
        if (Input.GetButtonDown(inputName))
        {
            // Check to see if we are not alrady holding something 
            // and that there is something nearby to pickup
            if (holdJoint.connectedBody == null && nearby != null)
            {
                // Move the nearby object to the holding position and attach
                nearby.transform.position = holdPosition.position;
                holdJoint.connectedBody = nearby.GetComponent<Rigidbody>();
            }
        }
        // If the trigger has been released
        if (Input.GetButtonUp(inputName))
        {
            // Get rid of any connected body that was attached
            holdJoint.connectedBody = null;
        }
    }

    // Function that gets called when our controllers intersect a trigger
    // such as those on the pickup-able cube in the scene
    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Entered: " + other.name);
        GameObject collided = other.gameObject;
        // Check to see that the object we trigger is something we can pickup
        if (collided.tag == "pickup")
        {
            // Set it to being nearby
            nearby = collided;
        }
    }

    // Parallels the above except it gets called when we exit the trigger volume
    private void OnTriggerExit(Collider other)
    {
        print("Trigger Exited: " + other.name);
        // Clear out any nearby objects
        nearby = null;
    }
}