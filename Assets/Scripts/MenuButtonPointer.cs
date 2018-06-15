using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class MenuButtonPointer : MonoBehaviour {

	/// <summary>
	/// Wenn der Haburger Icon auf dem Touch Controller gedrückt wird öffnet bzw. schließt sich das Menü
	/// Button.Start = Hamburger Icon
	/// </summary>

	public Transform canvas1;
	public Transform canvas2;
	public Transform canvas3;
	public Transform Player;
	private EventSystem es;

	//Für Menü
	public GameObject menuSizeController;
	public RandomMenuSize randomMenuSize;
	public GameObject[] menuButtons;
	private bool[] boolArray = new bool[26];
	public int random;
	public int count = 0;
	//Variablen für addData
	private char fieldSeperator = ';';
	private string filePath;
	private string buttonString;
	private float timeFloat1;
	private float timeFloat2;
	private float timeFloatPrint;

	private bool menuCheck;
	private int menuCounter;
	private int counter;

	void Start () {
		es = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		setFalse ();
		filePath = "Saved_data.csv";
		menuCheck = false;
		menuCounter = 0;
		//counter = 0;
	}

	void Update () {

		if (OVRInput.GetDown (OVRInput.Button.Start)) {
			if (menuCheck == false && menuCounter <= 2) {
				MenuOpened ();
				menuCheck = true;
				menuCounter += 1;
			}

			//öffnen
			/*   if (canvas.gameObject.activeInHierarchy == false) {
                canvas.gameObject.SetActive (true);

                // sicher stellen, dass der erste Button hervorgehoben wird
                // basierend auf: https://answers.unity.com/questions/1011523/first-selected-gameobject-not-highlighted.html
                es.SetSelectedGameObject (null);
                es.SetSelectedGameObject (es.firstSelectedGameObject);
            } 
            
            //schließen
            else {
                canvas.gameObject.SetActive (false);
            }*/
		}
	}

	//Array Werte auf false setzen
	void setFalse() {
		for (int i = 0; i < 26; i++) {
			boolArray[i] = false;
			menuButtons [i].SetActive (false);
			Button btn = menuButtons [i].GetComponent<Button>();
			btn.onClick.RemoveListener(OpenLargeMenu);
			btn.onClick.RemoveListener(OpenLargeMenu1);
			btn.onClick.RemoveListener(OpenLargeMenu2);
			btn.onClick.RemoveListener(OpenMiddleMenu);
			btn.onClick.RemoveListener(OpenMiddleMenu1);
			btn.onClick.RemoveListener(OpenMiddleMenu2);
			btn.onClick.RemoveListener(OpenSmallMenu);
			btn.onClick.RemoveListener(OpenSmallMenu1);
			btn.onClick.RemoveListener(OpenSmallMenu2);
			btn.GetComponent<Image> ().color = Color.white;
		}
	}

	//Random einen großen Button auswählen
	void randomButtonLarge () {
		random = Random.Range (0, 8);
		if(boolArray[random] == false) {
			boolArray[random] = true;
		} 
		else {
			randomButtonLarge();
		}
	}

	//Random einen mittleren Button auswählen
	void randomButtonMiddle () {
		random = Random.Range (9, 17);
		if(boolArray[random] == false) {
			boolArray[random] = true;
		} 
		else {
			randomButtonMiddle();
		}
	}

	//Random einen kleinen Button auswählen
	void randomButtonSmall () {
		random = Random.Range (18, 26);
		if(boolArray[random] == false) {
			boolArray[random] = true;
		} 
		else {
			randomButtonSmall();
		}
	}

	//je nach Größe, die buttons auf aktiv setzen
	void MenuOpened() {
		//randomMenuSize.pickNumber ();
		//randomMenuSize.SetRandomActive(counter);
		//counter++;
		randomMenuSize.SetRandomActive();
		menuCheck = false;

		//0-8
		if (randomMenuSize.randomNumber == 0) {
			OpenLargeMenu ();
			timeFloat1 = Time.realtimeSinceStartup;
		}

		//9-17
		if (randomMenuSize.randomNumber == 1) {
			OpenMiddleMenu ();
			timeFloat1 = Time.realtimeSinceStartup;
		}

		//18-26
		if (randomMenuSize.randomNumber == 2) {
			OpenSmallMenu ();
			timeFloat1 = Time.realtimeSinceStartup;
		}
	}

	void OpenLargeMenu() {
		setFalse ();
		randomButtonLarge ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (OpenLargeMenu1);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonLarge ();
		menuButtons [random].SetActive (true); 
		randomButtonLarge ();
		menuButtons [random].SetActive (true);
	}

	void OpenLargeMenu1() {
		setFalse ();
		randomButtonLarge ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (OpenLargeMenu2);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonLarge ();
		menuButtons [random].SetActive (true);
		randomButtonLarge ();
		menuButtons [random].SetActive (true);
	}

	void OpenLargeMenu2() {
		setFalse ();
		randomButtonLarge ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonLarge ();
		menuButtons [random].SetActive (true);
		randomButtonLarge ();
		menuButtons [random].SetActive (true);
	}

	void OpenMiddleMenu() {
		setFalse ();
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image>().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button>();
		btn.onClick.AddListener (OpenMiddleMenu1);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
	}

	void OpenMiddleMenu1() {
		setFalse ();
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image>().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button>();
		btn.onClick.AddListener (OpenMiddleMenu2);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
	}

	void OpenMiddleMenu2() {
		setFalse ();
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image>().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button>();
		btn.onClick.AddListener (TaskOnClick);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
	}


	void OpenSmallMenu() {
		setFalse ();
		randomButtonSmall ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (OpenSmallMenu1);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonSmall ();
		menuButtons [random].SetActive (true);
		randomButtonSmall ();
		menuButtons [random].SetActive (true);
	}

	void OpenSmallMenu1() {
		setFalse ();
		randomButtonSmall ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (OpenSmallMenu2);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonSmall ();
		menuButtons [random].SetActive (true);
		randomButtonSmall ();
		menuButtons [random].SetActive (true);
	}

	void OpenSmallMenu2() {
		setFalse ();
		randomButtonSmall ();
		menuButtons [random].SetActive (true);

		menuButtons [random].GetComponent<Image> ().color = Color.red;
		Button btn = menuButtons [random].GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);

		buttonString = random.ToString ();
		timeFloat2 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat2 - timeFloat1;
		addData ();
		timeFloat1 = timeFloat2;

		randomButtonSmall ();
		menuButtons [random].SetActive (true);
		randomButtonSmall ();
		menuButtons [random].SetActive (true);
	}

	void TaskOnClick() {
		setFalse ();
		MenuOpened ();
	}

	//Daten zur Tabelle hinzufügen
	public void addData() {
		// Following line adds data to CSV file
		File.AppendAllText(filePath, buttonString + fieldSeperator + timeFloatPrint + fieldSeperator);
		// Following lines refresh the edotor and print data
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}
}

