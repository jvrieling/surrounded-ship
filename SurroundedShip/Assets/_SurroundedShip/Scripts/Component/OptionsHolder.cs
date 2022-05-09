﻿///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EasyMobile;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using GooglePlayGames.BasicApi.SavedGame;

/// <summary>
/// OptionsHolder handles saving the SaveGame to the actual file. In the future, it will also handle saving the options within the game.
/// </summary>
public class OptionsHolder : MonoBehaviour
{
    public bool saveEnabled = true;
    public static OptionsHolder instance;

    /// <summary>
    /// MY save. The data about score, gold, etc
    /// </summary>
    public SaveGame save;
    private SaveGame localSave, GPGSave;

    private static string gameSaveLocation = "";

    public UnityEvent onLoadEvent;

    public Text statusText;
    public Text saveStatus;

    /// <summary>
    /// The EASY MOBILE saved game.
    /// </summary>
    private SavedGame savedGame;
    public static string savedGameName;

    public static bool GPGEnabled, FileSystemEnabled = true;
    public static string errorCode;

    private void Awake()
    {
        if (instance != null)
        {
            instance.Awake();
            Destroy(gameObject);
        }

        if (Application.platform == RuntimePlatform.Android) GPGEnabled = true;
        else GPGEnabled = false;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GPGEnabled = false;
            FileSystemEnabled = false;
        }

        Debug.Log("Game is running on " + Application.platform
            + "\n GPG: " + GPGEnabled
            + "\n File System: " + FileSystemEnabled);

        statusText.text = "waking up";

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (save.name != "test")
            save = new SaveGame();

        save.name = "DefaultSave";
        savedGameName = "sss";
        gameSaveLocation = Application.persistentDataPath + "/balls.gcsav";

        StartCoroutine(WaitForLogin());
    }

    public IEnumerator WaitForLogin()
    {
        float timeSpent = 0, loadTimeSpent = 0;
        bool cancelGPG = false, cancelGPGLoad = false;

        //////////////
        /// LOAD FROM FILE
        //////////////
        statusText.text = "Loading...";
        Debug.Log("Loading...");
        if (FileSystemEnabled) LoadFromFile();

        //////////////
        /// WAIT FOR GPG LOGIN
        //////////////
        if (GPGEnabled)
        {
            InfoPane.log += " Logging into GPG -";
            while (!GameServices.IsInitialized() && !cancelGPG)
            {
                statusText.text = "Logging in...";

                //Wait for game services to initialize. If it takes too long just give up and move on.
                timeSpent += Time.deltaTime;
                if (timeSpent > 4)
                {
                    cancelGPG = true;
                    GPGEnabled = false;
                    Debug.LogException(new Exception("Game Services initialization timed out!"));
                    InfoPane.log += " GPG timeout! -";
                }
                yield return null;
            }
        }
        else
        {
            InfoPane.log += " GPG Disabled -";
        }

        //////////////
        /// LOAD GPG CLOUD SAVE
        //////////////
        if (!cancelGPG && GPGEnabled)
        {
            //If we get here, game services are ready to go!
            statusText.text = "Logged in!";
            Debug.Log("Logged in!");
            LoadFromGPG();

            while (GPGSave == null && !cancelGPGLoad)
            {
                loadTimeSpent += Time.deltaTime;
                if (loadTimeSpent > 3)
                {
                    cancelGPGLoad = true;
                    InfoPane.log += " Loading GPG save took too long! ";
                    Debug.LogException(new Exception("Loading GPG save timed out!"));
                }
                yield return null;
            }
        }

        InfoPane.log += " login tried for " + timeSpent + " seconds - GPG save load took " + loadTimeSpent + " seconds -";

        //////////////
        /// USE THE LATEST SAVE
        //////////////
        if (localSave != null)
        {
            if (GPGEnabled && GPGSave != null)
            {
                Debug.Log("Comparing GPG with local save, choosing more recent.");

                save = SaveGame.CompareLastSaved(localSave, GPGSave);
                InfoPane.log += " using " + ((save == localSave) ? "local save." : "GPG save.");
            }
            else
            {
                InfoPane.log += " just using local save. GPGEnabled: " + GPGEnabled + " and FSEnabled: " + FileSystemEnabled + " GPGSave: " + ((GPGSave != null) ? GPGSave.name : "Null");
                Debug.Log("Using local save.");
                save = localSave;
            }

            if (Time.unscaledTime < 4) InfoPane.log += "\nWaiting a sec because loading went too fast!";

            //Make sure the logo shows for at least a second!
            if (Time.unscaledTime < 4) yield return new WaitForSeconds(4 - Time.unscaledTime);

            onLoadEvent.Invoke();
        }
    }
    private void OnApplicationQuit()
    {
        if (saveEnabled)
        {
            string scene = SceneManager.GetActiveScene().name;
            if (scene == "sc_MainMenu" || scene == "sc_Settings" || scene == "sc_Upgrades")
            {
                Debug.Log("Saving game before closing! scene is " + scene);
                WriteSave();
            }
        }
    }

    public void LoadFromFile()
    {
        if (File.Exists(gameSaveLocation))
        {
            try
            {
                byte[] data = File.ReadAllBytes(gameSaveLocation);
                if (data != null && data.Length > 0)
                    localSave = Utils.ObjectSerializationExtension.Deserialize<SaveGame>(data);
                else
                    localSave = new SaveGame();
                Debug.Log("Read from the file system! " + gameSaveLocation);
                InfoPane.log += "\nread from file -";
            }
            catch (IOException e)
            {
                Debug.LogException(new Exception("Error reading from the file system! " + e + " \nCreating empty save for the user."));
                localSave = new SaveGame();
            }
        }
        else
        {
            localSave = new SaveGame();
            File.Create(gameSaveLocation);
            InfoPane.log += "\ncreated new file -";
        }
    }
    public void LoadFromGPG()
    {
        Debug.Log("Opening file at " + gameSaveLocation + "!");

        if (GPGEnabled)
        {
            //open the save game...
            GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName, (openedGame, error) =>
            {
                if (string.IsNullOrEmpty(error))
                {
                    statusText.text = "Opened game!\nLoading data...";
                    savedGame = openedGame;
                    GameServices.SavedGames.ReadSavedGameData(savedGame, (game, data, readingError) =>
                    {
                        if (string.IsNullOrEmpty(readingError))
                        {
                            if (data.Length > 0)
                            {
                                statusText.text = "Almost done!";
                                GPGSave = Utils.ObjectSerializationExtension.Deserialize<SaveGame>(data);
                                InfoPane.log += "GPG save loaded -";
                            }
                            else
                            {
                                Debug.LogWarning("The loaded saveGame has no data!");
                            }
                        }
                        else
                        {
                            statusText.text = "Failed to load data due to error: " + readingError;
                            Debug.LogException(new Exception("Failed to load data from GPG due to error: " + readingError));
                            GPGEnabled = false;
                        }
                    });
                }
                else
                {
                    GPGEnabled = false;
                }
            });
        }

    }
    public void WriteSave()
    {
        if (saveEnabled)
        {
            save.lastSaved = DateTime.UtcNow;
            save.lastSaveGameVersion = Application.version;
            byte[] data = null;
            try
            {
                data = Utils.ObjectSerializationExtension.SerializeToByteArray(save);
            }
            catch (Exception e)
            {
                if (saveStatus != null) saveStatus.text = "Error serializing: " + e;
            }
            try
            {
                FileStream file = File.Create(gameSaveLocation);
                file.Write(data, 0, data.Length);
                file.Close();
                Debug.Log("Saved to file system!");
            }
            catch (IOException e)
            {
                Debug.LogError("Error writing to the file system! " + e);
            }

            if (GPGEnabled)
            {
                if (savedGame != null)
                {
                    if (savedGame.IsOpen)
                    {
                        //If the saved game is ope, skip trying to open it.
                        if (saveStatus != null) saveStatus.text = "Savegame is already open!";
                        WriteToGPG();
                    }
                    else
                    {
                        if (saveStatus != null) saveStatus.text = "Reopening...";
                        GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName, (openedGame, error) =>
                        {
                            if (string.IsNullOrEmpty(error))
                            {
                                savedGame = openedGame;
                                WriteToGPG();
                            }
                            else { saveStatus.text = "Error opening: " + error; }
                        });
                    }
                }
                else
                {
                    Debug.Log("There's no save game. Assuming this is the editor!");
                }
            }
        }
    }
    public void WriteToGPG()
    {
        byte[] saveData = null;
        try
        {
            saveData = Utils.ObjectSerializationExtension.SerializeToByteArray(save);
        }
        catch (Exception e)
        {
            if (saveStatus != null) saveStatus.text = "Error serializing: " + e;
        }

        //Assemble meta data for the cloud save.
        SavedGameInfoUpdate.Builder builder = new SavedGameInfoUpdate.Builder();
        builder.WithUpdatedDescription("Gold: " + save.totalGold + "\nMax Level: " + string.Format("{0:0.#}", save.recordDifficulty));
        TimeSpan timePlayed = TimeSpan.FromSeconds(save.totalTimePlayed);
        builder.WithUpdatedPlayedTime(timePlayed);

        SavedGameInfoUpdate infoUpdate = builder.Build();

        GameServices.SavedGames.WriteSavedGameData(savedGame, saveData, infoUpdate, (updatedSavedGame, writingError) =>
        {
            if (string.IsNullOrEmpty(writingError))
            {
                Debug.Log("Saved game data has been written successfully!");
                if (saveStatus != null) saveStatus.text = "Saved!";
            }
            else
            {
                Debug.LogError("Writing saved game data failed with error: " + writingError);
                if (saveStatus != null) saveStatus.text = "Error " + writingError + " size " + saveData.Length;
            }
        });
    }

    public void GiveGold()
    {
        save.totalGold += 1000;
    }

    public static void Reload(Text status, System.Action callback)
    {
        status.text = "Loading from device...";
        instance.LoadFromFile();
        status.text = "Loading from cloud...";
        instance.LoadFromGPG();

        if (instance.localSave != null)
        {
            if (GPGEnabled && instance.GPGSave != null)
            {
                Debug.Log("Comparing GPG with local save, choosing more recent.");

                instance.save = SaveGame.CompareLastSaved(instance.localSave, instance.GPGSave);
                InfoPane.log += " using " + ((instance.save == instance.localSave) ? "local save." : "GPG save.");
            }
            else
            {
                InfoPane.log += " just using local save. GPGEnabled: " + GPGEnabled + " and FSEnabled: " + FileSystemEnabled + " GPGSave: " + ((instance.GPGSave != null) ? instance.GPGSave.name : "Null");
                Debug.Log("Using local save.");
                instance.save = instance.localSave;
            }
        }

        callback.Invoke();
    }
}
