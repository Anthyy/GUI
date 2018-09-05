using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script can be found in the Component section under the option NPC/Dialogue
[AddComponentMenu("Skyrim2.0/NPC/Dialogue")]
public class Dialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDialogue;
    //index for our current line of dialogue and an index for a set question marker of the dialogue 
    public int index, optionIndex;
    //script reference to the player movement
    public CharacterMovement playerMovement;
    //mouselook script reference for the maincamera
    public MouseLook camLook;
    //mouselook script reference for the player
    public MouseLook charLook;
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public string npcName;
    //array for text for our dialogue
    public string[] dialogueText;
    [Header("Screen Ratio")]
    //screen x and y
    public Vector2 scr;
    #endregion
    #region Start
    private void Start()
    {
        //find and reference the player object by tag, get mouselook and movement
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        //get mouselook
        charLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
        //find and reference the maincamera by tag and get the mouse look component 
        camLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        //if our dialogue can be seen on screen
        if (showDialogue) // we only need
        {
            //set up our ratio measurements for 16:9
            if(scr.x != Screen.width/16 || scr.y != Screen.height / 9)
            {
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;
            }
            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0,6*scr.y, Screen.width, 3*scr.y), npcName + ": " + dialogueText[index]);
            //if not at the end of the dialogue or not at the options part
            if (!(index >= dialogueText.Length-1 || index == optionIndex)) // 
            {
                //next button allows us to skip forward to the next line of dialogue
                if(GUI.Button(new Rect(15*scr.x, 8.5f*scr.y, scr.x, 0.5f*scr.y), "Next"))
                {
                    //move forward in our dialogue array
                    index++;
                }
            }
            //else if we are at options
            else if(index == optionIndex)
            {
                //Accept button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(13 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Accept"))
                {
                    //move forward in our dialogue array
                    index++;
                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(14 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Decline"))
                {
                    //skip to end of dialogue
                    index = dialogueText.Length - 1;
                }
            }
            //else we are at the end
            else
            {
                //the Bye button allows up to end our dialogue
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Bye."))
                {
                    //close the dialogue box
                    showDialogue = false;
                    //set index back to 0 
                    index = 0;
                    //allow cameras mouselook to be turned back on
                    camLook.enabled = true;
                    //get the component mouselook on the character and turn that back on
                    charLook.enabled = true;
                    //get the component movement on the character and turn that back on
                    playerMovement.enabled = true;
                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible
                    Cursor.visible = false;
                }

            }

        }

    }
    #endregion
}
