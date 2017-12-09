using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulling : MonoBehaviour {
	public Vector3 positionStartRight = new Vector3();
	public Vector3 positionStartLeft = new Vector3();
	public Vector3 positionEndRight = new Vector3();
	public Vector3 positionEndLeft = new Vector3();
	public Vector3 positionDifferenceRight = new Vector3();
	public Vector3 positionDifferenceLeft = new Vector3();
	float rightMiddleState;
	float leftMiddleState;
	int check;
	enum State {True, False, Waiting};
	public Transform canvas;
	public Transform Player;

/*	void Start() {
		State handState;
		handState = State.False;
	}

	void Update() {
		if (handState == State.False) {
			rightMiddleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
			leftMiddleState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);

			Invoke (CheckState(rightMiddleState, leftMiddleState), 2);

			handState = State.Waiting;
		}
	}

	void CheckState(float startRight, float startLeft) {
		Debug.Log ("i bims");
		handState = State.False;
	}*/


/*Variante klappt halbwegs
	// Update is called once per frame
	void Update () {
		rightMiddleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		leftMiddleState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);

		if (rightMiddleState == 1.0 && leftMiddleState == 1.0) {
			check++;
		}

		if (check == 1) {
			positionStartRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
			positionStartLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
			Debug.Log ("yoyoyoyo, i bims!");
			StartCoroutine(Waiting());}
	}

	IEnumerator Waiting() {
		yield return new WaitForSeconds (1.5f);
		Debug.Log ("i bims 1,5 sekunde später!");
		positionEndRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
		positionEndLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
		check = 0;
		positionDifferenceRight = positionEndRight - positionStartRight;
		//if (positionDifferenceRight.x = 0.3) {
			Debug.Log (positionDifferenceRight);
		//}
	}*/

	// Update is called once per frame
	void Update () {
		rightMiddleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		leftMiddleState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);

		if (rightMiddleState == 1.0 && leftMiddleState == 1.0) {
			check++;
		}

		if (check == 1) {
			positionStartRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
			positionStartLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
			Debug.Log ("START POSITION -----------------------------------------" + positionStartRight);
		}
		positionEndRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
		positionEndLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
		positionDifferenceRight = positionEndRight - positionStartRight;
		positionDifferenceLeft = positionEndLeft - positionStartLeft;
		Debug.Log (positionDifferenceRight);

		if (positionDifferenceRight.x >= 0.3) {
			canvas.gameObject.SetActive(true);
			Time.timeScale = 0;
			Player.GetComponent<OVRPlayerController>().enabled = false;
		}
	}
}
