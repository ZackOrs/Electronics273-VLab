using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;


public class UnityLoginLogoutRegister : MonoBehaviour
{

    public string baseUrl = "http://localhost:8888/UnityLoginLogoutRegister/";
      

    public InputField accountName;
    public InputField accountSID;
    public InputField accountclassCode;
    public InputField accountLabSection;
    public TMP_Text info;

    private string currentName;
    private string ukey = "accountname";

    // Start is called before the first frame update
    void Start()
    {
        currentName = "";

        if(PlayerPrefs.HasKey(ukey))
        {
            if (PlayerPrefs.GetString(ukey) != "") {
                currentName = PlayerPrefs.GetString(ukey);
                info.text = "You are logged in as: " + currentName;
            }
            else
            {
                info.text = "You are not logged in. ";
            }

        }
        else
        {
            info.text = "You are not logged in. ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccountLogout()
    {
        currentName = "";
        PlayerPrefs.SetString(ukey, currentName);
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
                    SceneManager.LoadScene(1);
                    //info.text = "Login Success with Name: " + sName;
                }
                else
                {
                    info.text = "Login Failed. Please try again";
                }
            }
        }
    }
}
