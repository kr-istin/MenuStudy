using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class MenuButton : MonoBehaviour {

	/// <summary>
	/// Wenn der Haburger Icon auf dem Touch Controller gedrückt wird öffnet bzw. schließt sich das Menü
	/// Button.Start = Hamburger Icon
	/// </summary>

	//zum Schreiben in die Tabelle
	private string technique = "Button";
	private string interaction = "Stick";
	private string menuSize;
	private int modButton;
	private char lineSeperater = '\n'; // It defines line seperate character

	public Transform Player;
	private EventSystem es;

	//Für Menü
	public GameObject menuSizeController;
	public RandomMenuSize randomMenuSize;
	public GameObject[] largeButtons;
	public GameObject[] middleButtons;
	public GameObject[] smallButtons;
	private List<int> numberList = new List<int>{0,1,2,3,4,5,6,7,8};

	private List<int> menuList = new List<int>{0,1,2,0,1,2,0,1,2};
	private int n = -1;
	private int largeCounter = 0;
	private int middleCounter = 3;
	private int smallCounter = 6;

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

	public GameObject menuCanvas;

	void Start () {
		es = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		setFalse ();
		filePath = "Saved_data.csv";
		menuCheck = false;
		menuCounter = 0;
		//counter = 0;

		shuffleList ();
		shuffleMenuList ();
	}

	void Update () {

		if (OVRInput.GetDown (OVRInput.Button.Start)) {
			if (menuCheck == false && menuCounter <= 2) {
				MenuOpened ();
				menuCheck = true;
				menuCounter += 1;
				menuCanvas.SetActive (true);
			}
		}
	}

	//Array Werte auf false setzen
	void setFalse() {
		for (int i = 0; i < 9; i++) {
			largeButtons [i].SetActive (false);
			middleButtons [i].SetActive (false);
			smallButtons [i].SetActive (false);
		}
	}

	//je nach Größe, die buttons auf aktiv setzen
	void MenuOpened() {
		n++;

		timeFloat1 = Time.realtimeSinceStartup;
		timeFloatPrint = timeFloat1 - timeFloat2;
		addData ();
		timeFloat2 = Time.realtimeSinceStartup;

		if (n == 9) {
			SceneManager.LoadScene("Test", LoadSceneMode.Single);
		}

		if (menuList [n] == 0) {
			OpenLargeMenu ();
			Debug.Log ("0");
		}

		if (menuList [n] == 1) {
			OpenMiddleMenu ();
			Debug.Log ("1");
		}

		if (menuList [n] == 2) {
			OpenSmallMenu ();
			Debug.Log ("2");
		}
	}

	void OpenLargeMenu() {
		menuSize = "L";

		timeFloat2 = Time.realtimeSinceStartup;

		setFalse ();
		largeButtons [numberList [largeCounter]].SetActive (true);
		Button btn = largeButtons [numberList [largeCounter]].GetComponent<Button> ();
		btn.onClick.AddListener (MenuOpened);

		//für add data
		modButton = numberList[largeCounter];
		buttonString = modButton.ToString ();

		largeCounter++;
	}

	void OpenMiddleMenu() {
		menuSize = "M";

		timeFloat2 = Time.realtimeSinceStartup;

		setFalse ();

		middleButtons [numberList [middleCounter]].SetActive (true);
		Button btn = middleButtons [numberList [middleCounter]].GetComponent<Button> ();
		btn.onClick.AddListener (MenuOpened);

		//für add data
		modButton = numberList[middleCounter];
		buttonString = modButton.ToString ();

		middleCounter++;
	}

	void OpenSmallMenu() {
		menuSize = "S";

		timeFloat2 = Time.realtimeSinceStartup;

		setFalse ();

		//für add data
		smallButtons [numberList [smallCounter]].SetActive (true);
		Button btn = smallButtons [numberList [smallCounter]].GetComponent<Button> ();
		btn.onClick.AddListener (MenuOpened);

		modButton = numberList[smallCounter];
		buttonString = modButton.ToString ();

		smallCounter++;
	}
		
	public void shuffleList() {
		for (int i = 0; i < numberList.Count; i++) {
			int temp = numberList [i];
			int randomIndex = Random.Range (i, numberList.Count);
			numberList [i] = numberList [randomIndex];
			numberList [randomIndex] = temp;
		}
	}

	public void shuffleMenuList() {
		for (int i = 0; i < menuList.Count; i++) {
			int temp = menuList [i];
			int randomIndex = Random.Range (i, menuList.Count);
			menuList [i] = menuList [randomIndex];
			menuList [randomIndex] = temp;
		}
	}


	//Daten zur Tabelle hinzufügen
	public void addData() {
		// Following line adds data to CSV file
		//File.AppendAllText(filePath, buttonString + fieldSeperator + timeFloatPrint + fieldSeperator);

		File.AppendAllText(filePath, fieldSeperator + technique + fieldSeperator + interaction + fieldSeperator + menuSize + fieldSeperator + buttonString + fieldSeperator + timeFloatPrint + lineSeperater);

		// Following lines refresh the edotor and print data
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}
}

