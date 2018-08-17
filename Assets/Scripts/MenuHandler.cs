using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//interacting with scene chage
using UnityEngine.UI;//interacting with GUI elements 
using UnityEngine.EventSystems;//control the event (button shiz)
public class MenuHandler : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu, optionsMenu;// If this was not 'Public', you would not be able to interact with it in the scene
    public bool showOptions;
    public Slider volSlider, brightSlider, ambientSlider;
    public AudioSource mainAudio;
    public Light dirLight;
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullScreen;
    public Dropdown resDropdown;
    #endregion
    private void Start()
    {
       mainAudio = GameObject.Find("HelloWorld").GetComponent<AudioSource>();
       dirLight = GameObject.Find("DirectLight").GetComponent<Light>();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");// or (1)
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//For use in the Unity scene
        #endif
        Application.Quit();//Generally used in any .exe application
    }

    public void ToggleOptions()
    {
        OptionToggle();
    }
    bool OptionToggle()
    {
        if (showOptions) //showOptions == true or showOptions is true (basically it's true by default). (!showOptions) would mean it's false
        {
            showOptions = false;
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            return true;
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            volSlider = GameObject.Find("AudioSlider").GetComponent<Slider>(); // so it matches what you called it to the in-built actual thing
            volSlider.value = mainAudio.volume; //  
            brightSlider = GameObject.Find("BrightSlider").GetComponent<Slider>();
            brightSlider.value = dirLight.intensity;
            ambientSlider = GameObject.Find("AmbLightSlider").GetComponent<Slider>();           
            ambientSlider.value = RenderSettings.ambientIntensity; // didn't have to create own variable at the top or reference it here because there's only one ambient variable in Unity - "RenderSettings"
            resDropdown = GameObject.Find("Resolution").GetComponent<Dropdown>();
            return false;        
        }
        
   
    }
    public void Volume()
    {
        mainAudio.volume = volSlider.value; // matches audio to positioning of the slider
    }
    public void Brightness()
    {
        dirLight.intensity = brightSlider.value;
    }
    public void Ambient()
    {
        RenderSettings.ambientIntensity = ambientSlider.value;
    } 
    public void Resolution()
    {
        resIndex = resDropdown.value;
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullScreen);
    }
}    
