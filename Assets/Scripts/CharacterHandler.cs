using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
[AddComponentMenu("FirstPerson/Player Health")]
public class CharacterHandler : MonoBehaviour
{
    #region Variables
    //[Header("Character")]
    #region Character 
    //bool to tell if the player is alive
    public bool isAlive;
    //connection to players character controller
    public CharacterController controller;
    #endregion
    //[Header("Health")]
    #region Health
    //max and min health
    public float maxHealth;
    public float curHealth; // cur = current
    public GUIStyle healthBar;
    #endregion
    //[Header("Levels and Exp")]
    #region Level and Exp
    //players current level
    public int level;
    //max and current experience 
    public int maxExp, curExp;
    #endregion
    [Header("Camera Connection")]
    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #endregion
    #region Start

    private void Start()
    {
        //set max health to 100
        maxHealth = 100f;
        //set current health to max
        curHealth = maxHealth;
        //make sure player is alive
        isAlive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();
    }
    //set max health to 100
    //set current health to max
    //make sure player is alive
    //max exp starts at 60
    //connect the Character Controller to the controller variable
    #endregion
    #region Update
    private void Update()
    {
        //if our current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        //then the current experience is equal to our experience minus the maximum amount of experience
        curExp -= maxExp;
        //our level goes up by one
        level++;
        //the maximum amount of experience is increased by 50
        maxExp += 50;
    }
    //curHealth = (int) curHealth;
    #endregion
    #region LateUpdate
    private void LateUpdate() // always makes sure health is contained at the end of the frame (caps values and stops the GUI element from running off the screen)
    {
        //if our current health is greater than our maximum amount of health
        
        if (curHealth > maxHealth) 
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }
        //if our current health is less than 0 or we are not alive
        if (curHealth < 0 || !isAlive)
        {
            //current health equals 0
            curHealth = 0;
            Debug.Log("if less than 0 = 0");
        }

        //if the player is alive
        if (isAlive)
        {
            //and our health is less than or equal to 0
            if (curHealth == 0)
            {
                //alive is false
                isAlive = false;
                //controller is turned off
                controller.enabled = false;
                Debug.Log("Disable on death");
            }

        }

    }

    #endregion
    #region OnGUI
    void OnGUI()
    {
        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = Screen.width / 16;
        //scrH - 9
        float scrH =Screen.height / 9;
        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(6*scrW, 0.25f*scrH, 4*scrW, 0.5f*scrH), "", healthBar);
        //GUI Box for current health that moves but is in same place as the background bar
        //current Health divided by the posistion on screen and timesed by the total max health
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth*(4 * scrW)/maxHealth, 0.5f * scrH), "");

        //GUI Box on screen for the experience background

        //GUI Box for current experience that moves in same place as the background bar
        //current experience divided by the posistion on screen and timesed by the total max experience
        //GUI Draw Texture on the screen that has the mini map render texture attached
        GUI.DrawTexture(new Rect(13.75f*scrW, 0.25f*scrH, 2*scrW, 2*scrH), miniMap);
    }

    #endregion
}









