using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckLogin : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField Name = null;

    [SerializeField]
    private TMP_InputField StudentID = null;

    [SerializeField]
    private TMP_InputField ClassCode = null;

    [SerializeField]
    private TMP_InputField LabSection = null;

    public void checkLoginInfo()
    {
        string name = Name.text.Trim().ToLower();
        string studentID = StudentID.text;
        string classCode = ClassCode.text;
        string labSection = LabSection.text;

        if(studentID == "40004528")
        {
            SceneManager.LoadScene(2);
        }

        if ((name != "Nabil" && studentID != "40045344" && classCode != "A" && labSection != "AI")||
            (name != "Sara" && studentID != "40029374" && classCode != "A" && labSection != "AI")||
            (name != "Zachary" && studentID != "40004528" && classCode != "A" && labSection != "AI")||
            (name != "Kenneth" && studentID != "40045560" && classCode != "A" && labSection != "AI"))
        {
        
            Debug.Log("Incorrect! Please try again.");
        }
        else
        {
            

        }

    }
}
