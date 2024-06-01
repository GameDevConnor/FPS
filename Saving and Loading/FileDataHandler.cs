using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class FileDataHandler : MonoBehaviour
{
    private string dataDirPath = ""; // Directory path of the file
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // Use Path.Combine to account for different OSs having different file path structures
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData gameData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                // Load the Serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // We've read the Serialized data, now we need to Deserialize it
                gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred when trying to load the file: " + fullPath + "\n" + e);
            }
        }

        return gameData;

    }

    public void Save(GameData data)
    {
        // Use Path.Combine to account for different OSs having different file path structures
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // Create the directory path in case it doesn't exist yet
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the GameData object to a JSON string
            string dataToStore = JsonUtility.ToJson(data, true);


            // Write the file to the file system
            // When dealing with reading or writing to a file, it's best to use using blocks as they ensure that the connection to that file is closed once we're done reading or writing to it
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to write data to the file: " + fullPath + "\n" + e);
        }
    }
}
