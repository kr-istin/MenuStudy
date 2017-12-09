using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowCamera : MonoBehaviour {
	public Transform cameraToFollow;
	public Vector3 addPos = new Vector3();

	// Update is called once per frame
	void Update () {
		transform.position = cameraToFollow.position + addPos;
	}
}
