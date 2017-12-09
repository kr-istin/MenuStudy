using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMode : MonoBehaviour {
	public Transform canvas;
	public Transform Player;
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.Button.Start))
		{
			if (canvas.gameObject.activeInHierarchy == false)
			{
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				Player.GetComponent<OVRPlayerController>().enabled = false;
			} else
			{
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
				Player.GetComponent<OVRPlayerController>().enabled = true;
			}
		}
	}
}
