using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // System gives us access to our PC's date time (for this reason it also can't be paused, as you cannot pause your system's clock)

public class TimerClock : MonoBehaviour
{
    // Time in float to be converted to clock time
    public float timer;
    // Displayable clock time
    public string clockTime;
    // How the font looks 
    public GUIStyle text;

    public DateTime time;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = DateTime.Now;
        if(timer != 0)
        {
            timer -= Time.deltaTime;
        } 
        if(timer < 0)
        {
            timer = 0;
        }
    }
    private void OnGUI()
    {
        // pretty much just like an extra Update except it manages events and GUI elements
        int mins = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - mins * 60);
        clockTime = string.Format("{0:0}:{1:00}", mins, seconds);
        GUI.Label(new Rect(10,10,250,100), clockTime, text);
        GUI.Label(new Rect(10,200,250,100), time.Hour + ":" + time.Minute + ":" + time.Second, text);
    }
}
