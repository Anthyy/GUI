using System.Collections;
using UnityEngine;
[AddComponentMenu("Skyrim2.0/First-Person/Interact")]
public class Interact : MonoBehaviour
{
    [Header("References")]//
    public GameObject player;
    public GameObject mainCam;
    // public Camera cam;

    // Use this for initialization
    void Start()
    {
        // Set cursor lock state to locked
        Cursor.lockState = CursorLockMode.Locked;
        // Hide cursor
        Cursor.visible = false;
        //

        player = GameObject.Find("Player"); // finding by name
        mainCam = GameObject.FindGameObjectWithTag("MainCamera"); // finding by tag. It finds the game object and then the element in it.
       // cam = mainCam.GetComponent<Camera>();
       // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if(Physics.Raycast(interact, out hitInfo, 10f)) // out = output
            {
                #region NPC Dialogue
                if(hitInfo.collider.CompareTag("NPC")) 
                {
                    //dlg = hitinfo check for dlg on the hit npc
                    Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>(); // check to see if there's a dialogue script on the NPC
                    //if player has dialogue
                    if(dlg != null)
                    {
                        //show dialogue
                        dlg.showDialogue = true;
                        //turn off camera and player movements (all 3)
                        player.GetComponent<CharacterMovement>().enabled = false;
                        player.GetComponent<MouseLook>().enabled = false;
                        mainCam.GetComponent<MouseLook>().enabled = false;
                        //set the cursor to unlocked
                        Cursor.lockState = CursorLockMode.None;
                        //set the cursor to visible
                        Cursor.visible = true;

                        //print this message to the deug log
                        Debug.Log("Talk to NPC"); // Always put debugs at the end of the secton you are testing
                    }
                    
                }
                #endregion
                #region Chest
                if(hitInfo.collider.CompareTag("Chest"))
                {
                    Debug.Log("Open Chest");
                }
                #endregion
                #region Item
                if(hitInfo.collider.CompareTag("Item"))
                {
                    Debug.Log("Pick up Item");
                }   
                #endregion

            }
        }

    }
}
