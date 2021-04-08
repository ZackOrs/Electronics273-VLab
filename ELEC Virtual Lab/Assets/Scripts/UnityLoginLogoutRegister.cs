using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;



public class UnityLoginLogoutRegister : MonoBehaviour
{

    public GameObject timeDisplay;
    public int hour;
    public int minute;
    public int second;

    public string baseUrl = "http://sql5.freesqldatabase.com"
   // public string baseUrl = "http://localhost:8888/UnityLoginLogoutRegister/";


    public InputField accountName;
    public InputField accountSID;
    public InputField accountclassCode;
    public InputField accountLabSection;
    public InputField taUsername;
    public InputField taPassword;
    public TMP_Text info;

    private string currentName; //student and TA
    private string ukey = "accountname";
    private string taKey = "taAccount";

    // Start is called before the first frame update
    void Start()
    {
        currentName = "";

        if (PlayerPrefs.HasKey(ukey))
        {
            if (PlayerPrefs.GetString(ukey) != "")
            {
                currentName = PlayerPrefs.GetString(ukey);
             //   info.text = "You are logged in as: " + currentName;
            }
            else
          if (PlayerPrefs.HasKey(taKey)) //DOUBLE CHECK
            {
                if (PlayerPrefs.GetString(taKey) != "")
                {
                    currentName = PlayerPrefs.GetString(taKey);
                    info.text = "You are logged in as: " + currentName;
                }
                else
                {
                    info.text = "You are not logged in. ";
                }

            } // DOUBLE CHECK
            else
            {
                info.text = "You are not logged in. ";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccountLogout()
    {
        currentName = "";
        PlayerPrefs.SetString(ukey, currentName); // do if statement for student or TA
        info.text = "You are now logged out.";
    }

    public void AccountRegister()
    {
        string sName = accountName.text;
        string sID = accountSID.text;
        string cCode = accountclassCode.text;
        string lSection = accountLabSection.text;
        StartCoroutine(RegisterNewAccount(sName, sID, cCode, lSection));
    }

    public void AccountLogin()
    {
        string sName = accountName.text;
        string sID = accountSID.text;
        string cCode = accountclassCode.text;
        string lSection = accountLabSection.text;
        StartCoroutine(LogInAccount(sName, sID, cCode, lSection));
        //StartCoroutine(ChangeScene(1));
    }

    public void taAccountLogin()
    {
        string taName = taUsername.text;
        string taPword = taPassword.text;
        StartCoroutine(taLogInAccount(taName, taPword));
    }

    public void DeleteAccount()
     {
        string sName = accountName.text;
        string sID = accountSID.text;
        string cCode = accountclassCode.text;
        string lSection = accountLabSection.text;
        StartCoroutine(DeleteAccount(sName, sID, cCode, lSection));
     }

    IEnumerator RegisterNewAccount(string sName, string sID, string cCode, string lSection)
    {
        WWWForm form = new WWWForm();
        form.AddField("newAccountname", sName);
        form.AddField("newAccountstudentid", sID);
        form.AddField("newAccountclasscode", cCode);
        form.AddField("newAccountlabsection", lSection);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response = " + responseText);
                info.text = "Response = " + responseText;
            }
        }
    }

    IEnumerator LogInAccount(string sName, string sID, string cCode, string lSection)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("loginname", sName);
        form.AddField("loginstudentid", sID);
        form.AddField("loginclasscode", cCode);
        form.AddField("loginlabsection", lSection);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();


            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if(responseText == "1")
                {
                    PlayerPrefs.SetString(ukey, sName);
                    //info.text = "Login Success with Name: " + sName;

                    StartCoroutine(ChangeScene(2));
                    hour = System.DateTime.Now.Hour;
                    minute = System.DateTime.Now.Minute;
                    second = System.DateTime.Now.Second;
                    timeDisplay.GetComponent<Text>().text = "Time: " + hour + ":" + minute + ":" + second;
                }
                else
                {
                    info.text = "Login Failed. Please try again";
                    
                }
            }
        }
    }

    IEnumerator taLogInAccount(string taName, string taPword)
    {
        Debug.Log("Trying to login");
        WWWForm form = new WWWForm();
        form.AddField("tausername", taName);
        form.AddField("tapassword", taPword);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if (responseText == "1")
                {
                    PlayerPrefs.SetString(taKey, taName);
                    SceneManager.LoadScene(5);
                    //info.text = "Login Success with Name: " + sName;
                }
                else
                {
                    info.text = "Login Failed. Please try again";
                }
            }
        }
    }

    IEnumerator DeleteAccount(string sName, string sID, string cCode, string lSection)
    {
        WWWForm form = new WWWForm();
        form.AddField("Deletename", sName);
        form.AddField("Deletestudentid", sID);
        form.AddField("Deleteclasscode", cCode);
        form.AddField("Deletelabsection", lSection);
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                if (responseText == "0")
                {
                    Debug.Log("Response = " + responseText);
                    info.text = "Student removed successfully" + responseText;
                }
            }
        }
    }

    IEnumerator ChangeScene(int index, float delay = 3f)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(6);
    }
}
