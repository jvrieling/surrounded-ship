using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class OptionsHolder : MonoBehaviour
{
    public bool saveEnabled = true;
    public static OptionsHolder instance;
    public SaveGame save;
    

    //public SaveGame optionOnAwake;

    private static string gameSaveLocation = "";

    private void Awake()
    {
        instance = this;
        if (save.name != "test")
            save = new SaveGame();

        DontDestroyOnLoad(gameObject);

        save.name = "DefaultOptions";
        gameSaveLocation = Application.persistentDataPath + "/balls.gcsav";

        LoadGame();
    }

    public void SaveGame()
    {
        if (saveEnabled)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(gameSaveLocation);
            bf.Serialize(file, save);
            file.Close();
        }
    }
    public void LoadGame()
    {
        if (saveEnabled)
        {
            if (File.Exists(gameSaveLocation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(gameSaveLocation, FileMode.Open);
                save = (SaveGame)bf.Deserialize(file);
                file.Close();

                Debug.Log("Game Loaded");
                Debug.Log(save);
            }
            else
            {
                Debug.Log("No game saved!");
            }
        }
    }

}
