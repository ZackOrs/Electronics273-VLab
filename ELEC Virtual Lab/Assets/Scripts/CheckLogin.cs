﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckLogin : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField Name;

    [SerializeField]
    private TMP_InputField StudentID;

    [SerializeField]
    private TMP_InputField ClassCode;

    [SerializeField]
    private TMP_InputField LabSection;

    public void checkLoginInfo()
    {
        string name = Name.text.Trim().ToLower();
        string studentID = StudentID.text;
        string classCode = ClassCode.text;
        string labSection = LabSection.text;

        if ((name != "Nabil" && studentID != "40045344" && classCode != "A" && labSection != "AI")||
            (name != "Sara" && studentID != "40029374" && classCode != "A" && labSection != "AI")||
            (name != "Zachary" && studentID != "40004528" && classCode != "A" && labSection != "AI")||
            (name != "Kenneth" && studentID != "40045560" && classCode != "A" && labSection != "AI"))
        {
        
            Debug.Log("Incorrect! Please try again.");
        }
        else
        {
            SceneManager.LoadScene(2);

        }

    }
}