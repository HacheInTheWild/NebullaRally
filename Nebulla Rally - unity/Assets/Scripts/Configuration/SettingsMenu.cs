using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider brigSlider;
    public Slider volSlider;
    public Image imagenMute;
    public Dropdown resolutionDropdown;
    public AudioMixer audioMixer;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        imagenMute.enabled = false;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        volSlider.value = volume;
        audioMixer.SetFloat("volumen", volume);
        MuteOn();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetBright(float rgbValue)
    {
        brigSlider.value = rgbValue;
        RenderSettings.ambientLight = new Color(rgbValue, rgbValue, rgbValue, rgbValue);
    }

    public void MuteOn()
    {
        if (volSlider.value == -80)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

}
