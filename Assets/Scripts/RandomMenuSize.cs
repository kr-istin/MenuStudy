using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMenuSize : MonoBehaviour {

	public GameObject[] menuSizes;
	private bool[] boolArray = new bool[3];
	public int randomNumber;
	private int loopCounter;

	//private int index;

	void Start() {
		boolArray[0] = false;
		boolArray[1] = false;
		boolArray[2] = false;
		loopCounter = 0;

		//Shuffle (menuSizes);
	}

/*	void Shuffle(GameObject[] a) {
		for (int i = a.Length - 1; i > 0; i--) {
			int rnd = Random.Range (0, i);

			GameObject temp = a [i];

			a [i] = a [rnd];
			a [rnd] = temp;
		}
	}
*/

	public void SetRandomActive () {
		//	menuSizes[index].SetActive (true);
			randomNumber = Random.Range (0, 3);
			if (boolArray [randomNumber] == false) {
				boolArray [randomNumber] = true;
				menuSizes [randomNumber].SetActive (true);
			} /*else if (boolArray [0] == true && boolArray [1] == true && boolArray [2] == true) {
				return;
			} */
			else {
				SetRandomActive ();
			}
	}
}


