using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class MenuGesturePointer : MonoBehaviour {

    /// <summary>
    /// Das Menü wird durch eine Geste geöffnet und geschlossen.
    /// Zum Öffnen müssen die Hände/Controller zuerst aneinander gehalten und dann auseinander bewegt werden.
    /// Zum Schließen bewegt man die Hände in der selben Bewegung wieder aufeinader zu. 
    /// Die hinteren Trigger müssen auf beiden Controllern gedrückt gehalten und zum bestätigen der Bewegung losgelassen werden.
    /// </summary>

    private Vector3 positionStartRight = new Vector3 ();
    private Vector3 positionStartLeft = new Vector3 ();
    private Vector3 positionEndRight = new Vector3 ();
    private Vector3 positionEndLeft = new Vector3 ();
    private Vector3 positionDifferenceRight = new Vector3 ();
    private Vector3 positionDifferenceLeft = new Vector3 ();
    float rightMiddleState;
    float leftMiddleState;
    int check;      //als int und nicht als bool, da er sonst bei jedem update die funktion ausführt
    bool buttonCheck;  
    bool check2;
    public Transform canvas;
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
	//private int counter;

    void Start () {
        es = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
        buttonCheck = true;
		setFalse ();
		filePath = "Saved_data.csv";
		menuCheck = false;
		menuCounter = 0;
    }

    void Update () {
		rightMiddleState = OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger);
		leftMiddleState = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger);

		// prüft ob trigger gedrückt sind
		if (rightMiddleState == 1.0 && leftMiddleState == 1.0) {
			check++;
		}

		// setzt Startposition wenn trigger gedrückt wurden
		if (check == 1) {
			positionStartRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
			positionStartLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
			check2 = true;
		}

		// wenn trigger los gelassen werden, wird die Differenz zwischen der jetzigen Position und dem Start geprüft
		if (check2 == true) {
			if (rightMiddleState == 0.0 && leftMiddleState == 0.0) {
				positionEndRight = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
				positionEndLeft = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
				positionDifferenceRight = positionEndRight - positionStartRight;
				positionDifferenceLeft = positionEndLeft - positionStartLeft;
				/*Debug.Log ("Left:" + positionDifferenceLeft);
                Debug.Log ("Right:" + positionDifferenceRight);
                Debug.Log ("MiddleState geändert");*/
				check2 = false;
				check = 0;
			}
		}


		// Menü öffnen
		if (positionDifferenceRight.x >= 0.1 && positionDifferenceLeft.x <= -0.1) {
			if (menuCheck == false && menuCounter <= 2) {
				positionDifferenceLeft.x = 1;
				positionDifferenceRight.x = -1;
				menuCheck = true;
				menuCounter ++;
				MenuOpened ();
			}

			/*
			canvas.gameObject.SetActive (true);
            Player.GetComponent<OVRPlayerController> ().enabled = false;
            //check2 = false;

            // damit der zu highlightende Button nicht bei jedem Update zurück gesetzt wird
            if (buttonCheck == true) { 
                es.SetSelectedGameObject (null);
                es.SetSelectedGameObject (es.firstSelectedGameObject);
                buttonCheck = false;
            }
        }

        // Menü schließen
        if (positionDifferenceRight.x <= -0.1 && positionDifferenceLeft.x >= 0.1) {
            canvas.gameObject.SetActive (false);
            Player.GetComponent<OVRPlayerController> ().enabled = true;
            buttonCheck = true;
        }
		*/
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
		//randomMenuSize.SetRandomActive (counter);
		randomMenuSize.SetRandomActive();
		Debug.Log ("Menu opened");
		//counter++;
		//menuCheck = false;
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
		Debug.Log ("openLageMenu");
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
		Debug.Log ("openLageMenu1");
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
		menuCheck = false;
		setFalse ();
		randomButtonLarge ();
		menuButtons [random].SetActive (true);
		Debug.Log ("openLageMenu2");
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
		Debug.Log ("openMiddleMenu");
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
		Debug.Log ("openMiddleMenu1");
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
		menuCheck = false;
		setFalse ();
		randomButtonMiddle ();
		menuButtons [random].SetActive (true);
		Debug.Log ("openMiddleMenu2");
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
		Debug.Log ("openSmallMenu");
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
		Debug.Log ("openSmallMenu1");
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
		menuCheck = false;
		setFalse ();
		randomButtonSmall ();
		menuButtons [random].SetActive (true);
		Debug.Log ("openSmallMenu2");
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


