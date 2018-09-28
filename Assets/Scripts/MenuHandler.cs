using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//interacting with scene chage
using UnityEngine.UI;//interacting with GUI elements 
using UnityEngine.EventSystems;//control the event (button shiz)
public class MenuHandler : MonoBehaviour
{
    #region Variables
    [Header("OPTIONS")]
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool showOptions;
    public bool isFullScreen;
    [Header("Keys")]
    public KeyCode holdingKey; // or "tempKey". Basically a control that is able to be changed/set to different keys
    public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;
    [Header("References")]
    public AudioSource mainAudio;
    public GameObject mainMenu, optionsMenu;// If this was not 'Public', you would not be able to interact with it in the scene  
    public Slider volSlider, brightSlider, ambientSlider;
    public Light dirLight;
    public Dropdown resDropdown;
    [Header("KeyBind References")]
    public Text forwardText;
    public Text backwardText, leftText, rightText, jumpText, crouchText, springText, interactText;
    #endregion
    void Start()
    {
       mainAudio = GameObject.Find("HelloWorld").GetComponent<AudioSource>();
       dirLight = GameObject.Find("DirectLight").GetComponent<Light>();
        #region Set Up Keys
        // Set out keys to the preset keys we may have saved , else set the keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W")); // the first part "(KeyCode)System.Enum.Parse(typeof(KeyCode)" is how you convert a string/word into an enum so you can use it as a KeyCode, as string and enum are two different types of data
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));       
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
        #endregion
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
    public void Save()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());
    

    }
    private void OnGUI()
    {
        Event e = Event.current;
        if(forward == KeyCode.None)
        {
            Debug.Log("KeyCode:" + e.keyCode);
            if(e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
                {
                    forward = e.keyCode;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
                else
                {
                    forward = holdingKey;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
            }
        }   
    }
    public void Forward()
    {
        if(!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
}    
