using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider), typeof (Rigidbody))]
public class Test : MonoBehaviour {

	public Transform right;
	public float sensitivy;
	public Transform canvas;
	public Transform Player;

	private Collider c;
	private Rigidbody rb;
	private Vector3 startPos;

	bool menuState = false;
	bool gestureState;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		c = GetComponent<Collider> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		bool indexState = OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger);
		bool thumbState = OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
		float middleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		bool leftState = OVRInput.GetDown(OVRInput.RawButton.LHandTrigger);

		if (!indexState && thumbState && middleState == 1.0 && leftState) {
			Vector3 pos = right.position;
			if (Vector3.Distance (c.ClosestPointOnBounds (pos), pos) < sensitivy) {
				if (!menuState) {
					canvas.gameObject.SetActive (true);
					Time.timeScale = 0;
					Player.GetComponent<OVRPlayerController> ().enabled = false;
					menuState = true;
				} else if (menuState) {
					canvas.gameObject.SetActive (false);
					Time.timeScale = 1;
					Player.GetComponent<OVRPlayerController> ().enabled = true;
					menuState = false;
				}
			}
		}
	}
}


