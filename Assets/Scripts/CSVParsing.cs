using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


// based on http://www.theappguruz.com/blog/unity-csv-parsing-unity

public class CSVParsing : MonoBehaviour
{
    //public TextAsset csvFile; // Reference of CSV file
    public InputField nameInputField; // Reference of name input filed
    public Dropdown dropdownMenuTechnique;
    public Dropdown dropdownSelectionTechnique;

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate chracter

    private string filePath;
    private string testText;

    private int dropdownMenuTechniqueValue;
    private int dropdownSelectionTechniqueValue;
    private string menuTechnique;
    private string selectionTechnique;

    void Start()
    {
        filePath = "Saved_data.csv";
    }

    // Add data to CSV file
    public void addData()
    {
		/*
		//check which dropdown options are chosen and set strings aacordingly
        dropdownSelectionTechniqueValue = dropdownSelectionTechnique.value;
        dropdownMenuTechniqueValue = dropdownMenuTechnique.value;

        if(dropdownMenuTechniqueValue == 0)
        {
            menuTechnique = "Button";
        }
        else { menuTechnique = "Gesture"; }

        if(dropdownSelectionTechniqueValue == 0)
        {
            selectionTechnique = "Stick";
        }
        else { selectionTechnique = "Pointer"; }
        */

        // Following line adds data to CSV file
		// mit Menü- und Interaktionstechnik
        //File.AppendAllText(filePath, lineSeperater + nameInputField.text + fieldSeperator + menuTechnique + fieldSeperator + selectionTechnique + fieldSeperator);

		// nur Name/Nummer
		File.AppendAllText(filePath, lineSeperater + nameInputField.text + lineSeperater);

		// Following lines refresh the edotor and print data
        nameInputField.text = "";
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    // Get path for given CSV file
    private static string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
return Application.persistentDataPath;// +fileName;
#elif UNITY_IPHONE
return GetiPhoneDocumentsPath();// +"/"+fileName;
#else
return Application.dataPath;// +"/"+ fileName;
#endif
    }

}