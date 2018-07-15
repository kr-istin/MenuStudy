using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollideMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("Treffer");
		col.GetComponent<Button> ().Select ();
	}

	void OnTriggerExit(Collider col){
		Debug.Log ("Weg");
		EventSystem.current.SetSelectedGameObject (null);
	}
}
