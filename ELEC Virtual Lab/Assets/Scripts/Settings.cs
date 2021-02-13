using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    public Slider mouseSlider;
    public TMP_Text mouseText;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    const string prefName = "optionvalue";
    const string resName = "resolutionoption";

    public void _UpdateAll()
    {
        if(mouseSlider.value <= 6.0f && mouseSlider.value >= 5.0f)
        {
            mouseText.text = "Normal";
        }
        else if (mouseSlider.value <= 4.9f && mouseSlider.value >= 2.0f)
        {
            mouseText.text = "Low";
        }
        else if (mouseSlider.value <= 1.9f && mouseSlider.value >= 0.1f)
        {
            mouseText.text = "Very Low";
        }
        else if (mouseSlider.value <= 8.0f && mouseSlider.value >= 6.1f)
        {
            mouseText.text = "High";
        }
        else if (mouseSlider.value <= 10.0f && mouseSlider.value >= 8.1f)
        {
            mouseText.text = "Very High";
        }
  
    }

    public void SaveAll()
    {
        PlayerPrefs.SetFloat("mouse", mouseSlider.value);
        SceneManager.LoadScene(0);
    } 

    public void RestoreDefaults()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(3);
    }

    void Awake()
    {
        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));

    }


    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;

        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResIndex);
        resolutionDropdown.RefreshShownValue();

        mouseSlider.value = PlayerPrefs.GetFloat("mouse");



    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    // Update is called once per frame
   /* void Update()
    {
      
    }*/
}
