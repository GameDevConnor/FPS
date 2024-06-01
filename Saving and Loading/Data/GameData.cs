using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int boxesCollected;

    // When we start a new game, the values in this constructor will be the initial values to start with
    public GameData()
    {
        boxesCollected = 0;
    }
}
