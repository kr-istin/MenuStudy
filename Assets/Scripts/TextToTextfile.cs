using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class TextToTextfile : MonoBehaviour {

    //public GameObject textfield;
    private string text;
    private string filePath;
    private string testText;

    public void toTextfile (string textfeld) {
        filePath  = "Saved_data.csv";
        text = textfeld;
        testText = "yoyoyoyo";
        //textfield = textfield.GetComponent<InputField>().Text;
        //System.IO.File.WriteAllText ("UnityTest.txt", textfield.ToString());
        System.IO.File.WriteAllText (filePath, text + ";" + "blabla" + ";" + testText + ";");
    }
}
