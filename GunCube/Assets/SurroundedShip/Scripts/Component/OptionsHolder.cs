///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// OptionsHolder handles saving the SaveGame to the actual file. In the future, it will also handle saving the options within the game.
/// </summary>
public class OptionsHolder : MonoBehaviour
{
    public bool saveEnabled = true;
    public static OptionsHolder instance;
    public SaveGame save;

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
            }
        }
    }

}
