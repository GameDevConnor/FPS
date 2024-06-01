using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Want a class to show up in the inspector so we can edit it
[System.Serializable]
public class Dialogue
{
    // Object we can pass into the manager whenever we want to start a new dialogue
    // Hold all the info we need to for a single dialogue

    [TextArea(3, 10)] // Minimum and maximum amount of lines the text will use
    public string[] sentences;
    public string name;


}
