using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);

    void SaveData(ref GameData data);
//  This allows pass by reference (instead of just pass by value), so that the implementing script actually modifies the data

}
