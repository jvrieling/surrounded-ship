///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// OptionsHolder handles saving the SaveGame to the actual file. In the future, it will also handle saving the options within the game.
/// </summary>
public class OptionsHolder : MonoBehaviour
{
    public bool saveEnabled = true;
    public static OptionsHolder instance;
    public SaveGame save;

    private static string gameSaveLocation = "";

    public UnityEvent onLoadEvent;

    public static PlayGamesPlatform platform;

    public Text statusText;

    private void Awake()
    {
        statusText.text = "waking up";

        instance = this;
        if (save.name != "test")
            save = new SaveGame();

        DontDestroyOnLoad(gameObject);

        save.name = "DefaultOptions";
        gameSaveLocation = Application.persistentDataPath + "/balls.gcsav";

        statusText.text = "Logging in...";
        //Authenticate GPG
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in!");
                statusText.text = "Logged in!";
                GPGSSaveGame.OpenSavedGame("sssave", OnOpenForLoad);
            }
            else
            {
                statusText.text = "login failed";
                LoadGame();
                Debug.LogWarning("Failed to log in.");
            }
        });
    }
    public void OnOpenForLoad(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            GPGSSaveGame.openedGame = game;
            statusText.text = "Opened the savegame";
            LoadGame();
        }
        else
        {
            Debug.LogError("Failed to open the GPG save game!");
            statusText.text = "failed to open";
        }
    }
    public void LoadGame()
    {
        if (saveEnabled)
        {
            if (Application.platform == RuntimePlatform.Android && Social.localUser.authenticated)
            {
                statusText.text = "loading from GPG...";
                GPGSSaveGame.LoadGameData(OnLoad);
            }
            else
            {
                if (File.Exists(gameSaveLocation))
                {
                    statusText.text = "loading from file...";
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(gameSaveLocation, FileMode.Open);
                    byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/balls.gcsav");
                    file.Read(data, 0, 0);
                    OnLoad(data);

                    //save = (SaveGame)bf.Deserialize(file);


                    file.Close();
                    onLoadEvent.Invoke();
                }
            }
        }
    }
    public void OnLoad(byte[] data)
    {
        statusText.text = "Reading received data...";
        if (data != null && data.Length > 0)
        {
            statusText.text = "parsing";
            using (MemoryStream stream = new MemoryStream(data))
            {
                statusText.text = "parsing.";
                XmlSerializer x = new XmlSerializer(typeof(SaveGame));
                statusText.text = "parsing.. data is " + data.Length;
                save = (SaveGame)x.Deserialize(stream);
                stream.Close();
                statusText.text = "parsing...";


                onLoadEvent.Invoke();
            }
        }
        onLoadEvent.Invoke();
    }
    public void SaveGame()
    {
        if (saveEnabled)
        {
            if (Application.platform == RuntimePlatform.Android && Social.localUser.authenticated)
            {
                byte[] message;
                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer x = new XmlSerializer(typeof(SaveGame));
                    x.Serialize(stream, save);
                    message = stream.ToArray();
                    stream.Close();
                }
                GPGSSaveGame.SaveGame(message, DateTime.Now - save.dateStarted);
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(gameSaveLocation);
                bf.Serialize(file, save);
                file.Close();
            }
        }
    }
    public void GiveGold()
    {
        save.totalGold += 1000;
    }

}
