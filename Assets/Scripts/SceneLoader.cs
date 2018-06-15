using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public Dropdown dropdownMenuTechnique;          // Menü Technik Dropdown im Editor hinzufügen
    public Dropdown dropdownSelectionTechnique;     // Menü Technik Dropdown im Editor hinzufügen

    private int dropdownMenuTechniqueValue;         // Variable um aktuelles Value des Menü Technik Dropdowns zwischenzuspeichern 
    private int dropdownSelectionTechniqueValue;


	
	public void loadScene()
    {
        //check which dropdown options are chosen and set strings aacordingly
        dropdownSelectionTechniqueValue = dropdownSelectionTechnique.value;
        dropdownMenuTechniqueValue = dropdownMenuTechnique.value;

        // choose which scene to load

        // Button und Stick
        if (dropdownMenuTechniqueValue == 0)
        {
            if (dropdownSelectionTechniqueValue == 0)
            {
                SceneManager.LoadScene("ButtonStick", LoadSceneMode.Single);
            }

            else
            {
                SceneManager.LoadScene("ButtonPointer", LoadSceneMode.Single);
            }
        }

        else
        {
            if (dropdownSelectionTechniqueValue == 0)
            {
                SceneManager.LoadScene("GestureStick", LoadSceneMode.Single);
            }

            else
            {
                SceneManager.LoadScene("GesturePointer", LoadSceneMode.Single);
            }
        }

/*
        // Button und Pointer
        else if ((dropdownMenuTechniqueValue == 0) && (dropdownSelectionTechniqueValue == 1))
        {
            SceneManager.LoadScene("ButtonPointer", LoadSceneMode.Single);
        }

        // Geste und Stick
        else if ((dropdownMenuTechniqueValue == 1) && (dropdownSelectionTechniqueValue == 1))
        {
            SceneManager.LoadScene("GestureStick", LoadSceneMode.Single);
        }

        // Geste und Pointer
        else if ((dropdownMenuTechniqueValue == 1) && (dropdownSelectionTechniqueValue == 1))
        {
            SceneManager.LoadScene("GesturePointer", LoadSceneMode.Single);
        }

*/    
    }


}
