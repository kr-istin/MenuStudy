using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReticle : MonoBehaviour {

/*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 touchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) * Time.deltaTime;
		transform.position += new Vector3(touchAxis.x, 0, touchAxis.y);
	}
*/

 
    public GameObject camera;
 
    void Update()
    {
        //reading the input:
        float horizontalAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
        float verticalAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
 
        //camera up and right vectors:
        Vector3 up = camera.transform.up;
        Vector3 right = camera.transform.right;
 
        //project up and right vectors on the horizontal plane (z = 0)
        up.z = 0f;
        right.z = 0f;
        up.Normalize();
        right.Normalize();
 
        //this is the direction in the world space we want to move:
        Vector3 desiredMoveDirection = up * verticalAxis + right * horizontalAxis;
 
        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * Time.deltaTime);

    }

}
