using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("References")]//
    public GameObject player;
    public GameObject mainCam;
    // public Camera cam;

    // Use this for initialization
    void Start()
    {
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
                    Debug.Log("Talk to NPC");
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
