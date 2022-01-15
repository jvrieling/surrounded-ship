///////////////////////////////
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

using GooglePlayGames.BasicApi.SavedGame;

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

    public Text statusText;
    public Text saveStatus;

    private SavedGame savedGame;
    public static string savedGameName;

    private void Awake()
    {
        statusText.text = "waking up";

        instance = this;
        if (save.name != "test")
            save = new SaveGame();

        DontDestroyOnLoad(gameObject);

        save.name = "DefaultSave";
        savedGameName = "sss";
        gameSaveLocation = Application.persistentDataPath + "/balls.gcsav";

        statusText.text = "Attempting to log in...";
        StartCoroutine(WaitForLogin());
    }
    private void OnApplicationQuit()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "sc_MainMenu" || scene == "sc_Settings" || scene == "sc_Upgrades")
        {
            Debug.Log("Saving game before closing! scene is " + scene);
            SaveGame();
        }
    }
    public IEnumerator WaitForLogin()
    {
        float timeSpent = 0;
        while (!GameServices.IsInitialized())
        {
            //Wait for game services to initialize. If it takes too long just give up and move on.
            timeSpent += Time.deltaTime;
            if (timeSpent > 3)
            {
                onLoadEvent.Invoke();
                StopAllCoroutines();
            }
            yield return null;
        }

        //If we get here, game services are ready to go!
        statusText.text = "Logged in!";
        LoadGame();

        //If loading returns an error, it will freeze. Loading should take under 2 seconds...
        yield return new WaitForSeconds(2);
        onLoadEvent.Invoke();
    }
    public void LoadGame()
    {
        if (saveEnabled)
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
                                save = Utils.ObjectSerializationExtension.Deserialize<SaveGame>(data);
                                StopAllCoroutines();
                                onLoadEvent.Invoke();
                            }
                            else
                            {
                                Debug.LogWarning("The loaded saveGame has no data!");
                            }
                        }
                        else
                        {
                            statusText.text = "Failed to load data due to error: " + readingError;
                        }
                    });
                }
            });
        }
    }
    public void SaveGame()
    {
        if (saveEnabled)
        {
            if (savedGame != null)
            {
                if (savedGame.IsOpen)
                {
                    //If the saved game is ope, skip trying to open it.
                    if (saveStatus != null) saveStatus.text = "Savegame is already open!";
                    WriteSaveGame();
                }
                else
                {
                    if (saveStatus != null) saveStatus.text = "Reopening...";
                    GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName, (openedGame, error) =>
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            savedGame = openedGame;
                            WriteSaveGame();
                        }
                        else { saveStatus.text = "Error opening: " + error; }
                    });
                }
            } else
            {
                Debug.Log("There's no save game. Assuming this is the editor!");
            }
        }
    }
    public void WriteSaveGame()
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

}
