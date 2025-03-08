using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static  SaveManager instance { get; private set; }
    [SerializeField] private string fileName;
    public bool encriptData;
    private GameData gameData;
    private List<ISaveManager> saveManagers = new List<ISaveManager>();
    private FileDataHandler dataHandler;

    [ContextMenu("删除保存的数据")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encriptData);//"F:RPGData/gameData"
        dataHandler.Delete();
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;
    }
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encriptData);//"F:RPGData/gameData"
        saveManagers = FindAllOfISaveManager();

        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("没有游戏数据");
            NewGame();
        }

        foreach (ISaveManager manager in saveManagers)
            manager.LoadData(gameData);
    }

    public void SaveGame()
    {
        foreach (ISaveManager manager in saveManagers)
            manager.SaveData(ref gameData);

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllOfISaveManager()
    {
        IEnumerable<ISaveManager> saveMagager = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveMagager);
    }

    //查看是否存有数据
    public bool HaveSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encriptData);//"F:RPGData/gameData"
        if (dataHandler.Load() != null)
            return true;

        return false;
    }
}
