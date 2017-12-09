using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPulling : MonoBehaviour {
	public Vector3 positionStartRight = new Vector3();
	public Vector3 positionStartLeft = new Vector3();
	public Vector3 positionEndRight = new Vector3();
	public Vector3 positionEndLeft = new Vector3();
	public Vector3 positionDifferenceRight = new Vector3();
	public Vector3 positionDifferenceLeft = new Vector3();
	float rightMiddleState;
	float leftMiddleState;
	int check;		//als int und nicht als bool, da er sonst bei jedem update die funktion ausführt
	bool check2;
	public Transform canvas;
	public Transform Player;
	
	// Update is called once per frame
	void Update () {
		rightMiddleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		leftMiddleState = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);

		// prüft ob trigger gedrückt sind
		if (rightMiddleState == 1.0 && leftMiddleState == 1.0) {
			check++;
		}

		// setzt Startposition wenn trigger gedrückt wurden
		if (check ==  1) {
			positionStartRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
			positionStartLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
			check2 = true;
		}

		/*if (check2 == true) {
			positionEndRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
			positionEndLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
			positionDifferenceRight = positionEndRight - positionStartRight;
			positionDifferenceLeft = positionEndLeft - positionStartLeft;
			Debug.Log (positionDifferenceLeft);
		}*/


		// wenn trigger los gelassen werden, wird die Differenz zwischen der jetzigen Position und dem Start geprüft
		if(check2 == true){
			if (rightMiddleState == 0.0 && leftMiddleState == 0.0) {
				positionEndRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
				positionEndLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
				positionDifferenceRight = positionEndRight - positionStartRight;
				positionDifferenceLeft = positionEndLeft - positionStartLeft;
				Debug.Log ("Left:" + positionDifferenceLeft);
				Debug.Log ("Right:" + positionDifferenceRight);
				Debug.Log ("MiddleState geändert");
				check2 = false;
				check = 0;
			}
		}


		// Menü öffnen
		if (positionDifferenceRight.x >= 0.1 && positionDifferenceLeft.x <= -0.1) {
			canvas.gameObject.SetActive (true);
			//Time.timeScale = 0;
			Player.GetComponent<OVRPlayerController> ().enabled = false;
			//check2 = false;
		}

		// Menü schließen
		if (positionDifferenceRight.x <= -0.1 && positionDifferenceLeft.x >= 0.1) {
			canvas.gameObject.SetActive (false);
			//Time.timeScale = 0;
			Player.GetComponent<OVRPlayerController> ().enabled = true;
		}
	}
}
