using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuGesture : MonoBehaviour {

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

    void Start () {
        es = GameObject.Find ("EventSystem_Pointer").GetComponent<EventSystem> ();
        buttonCheck = true;
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
    }
}
