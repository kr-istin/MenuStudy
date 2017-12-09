using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointing : MonoBehaviour {

	void Update () {
		bool indexState = OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger);
		bool thumbState = OVRInput.Get(OVRInput.Touch.SecondaryThumbRest);
		float middleState = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
		if(!indexState && thumbState && middleState == 1.0) {
			Debug.Log("Touch funktioniert");
		}
	}
}
