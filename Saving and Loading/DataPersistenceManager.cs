using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gives us nicer syntax for finding IDataPersistence objects
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    // Call it like DataPersistenceManager.instance.LoadGame()


    [Header("File Storage Config")]
    [SerializeField]
    private string fileName;

    private FileDataHandler dataHandler;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one data persistence manager. There should only be 1");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        // TODO pass data to other scripts so that they can update it
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref gameData);
        }

        // save to file using the data handler
        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        // Load any save data using the data handler
        // If no data to load, initialize a new game. Because if it doesn't exist, dataHandler.Load() will return null
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewGame();
        }

        // TODO - Push loaded data to all other scripts
        foreach (IDataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistence.LoadData(gameData);
        }
    }


    // Load game on startup and close on exiting
    private void Start()
    {
        // Application.persistentDataPath - Give the OS standard directory for persisting data in a unity project
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        // These scripts need to extend from monobeahviour as well
        // Finds all objects that are of type IDataPersistence

        return new List<IDataPersistence>(dataPersistences);
    }
}
