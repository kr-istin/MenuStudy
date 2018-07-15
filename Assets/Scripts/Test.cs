using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	RaycastHit2D Hit;
	public float ZValue = 0;
	public Camera camera = new Camera();

	void Start () {
	}


/*
	void FixedUpdate()
	{
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZValue);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
		transform.position = cursorPosition;
		transform.LookAt(Camera.main.transform);
	}
}
*/


	// Use this for initialization
	void Update () 
	{
		Vector2 touchAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) * Time.deltaTime;
		transform.position += new Vector3(touchAxis.x, touchAxis.y, 0);
		transform.LookAt(camera.transform);


		if (Physics2D.Raycast (transform.position, transform.forward)) {
			Debug.Log (Hit.transform.name);
		}
	}

}
