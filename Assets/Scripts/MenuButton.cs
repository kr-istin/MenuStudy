using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour {

    /// <summary>
    /// Wenn der Haburger Icon auf dem Touch Controller gedrückt wird öffnet bzw. schließt sich das Menü
    /// Button.Start = Hamburger Icon
    /// </summary>

    public Transform canvas;
    public Transform Player;
    private EventSystem es;

    void Start () {
       es = GameObject.Find ("EventSystem_Pointer").GetComponent<EventSystem> ();
    }

    void Update () {

        if (OVRInput.GetDown (OVRInput.Button.Start)) {
            
            //öffnen
            if (canvas.gameObject.activeInHierarchy == false) {
                canvas.gameObject.SetActive (true);

                // sicher stellen, dass der erste Button hervorgehoben wird
                // basierend auf: https://answers.unity.com/questions/1011523/first-selected-gameobject-not-highlighted.html
                es.SetSelectedGameObject (null);
                es.SetSelectedGameObject (es.firstSelectedGameObject);

                /*Time.timeScale = 0;
                Player.GetComponent<OVRPlayerController> ().enabled = false;    // stellt die Bewegung des Players aus*/
            } 
            
            //schließen
            else {
                canvas.gameObject.SetActive (false);
                /*Time.timeScale = 1;
                Player.GetComponent<OVRPlayerController> ().enabled = true;*/
            }
        }
    }
}
