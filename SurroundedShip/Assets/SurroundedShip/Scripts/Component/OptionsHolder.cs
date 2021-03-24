///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EasyMobile;
using System.Collections;

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

    private SavedGame savedGame;
    public static string savedGameName;

    private void Awake()
    {
        statusText.text = "waking up";

        instance = this;
        if (save.name != "test")
            save = new SaveGame();

        DontDestroyOnLoad(gameObject);

        save.name = "DefaultOptions";
        savedGameName = "SurroundedShipSave";
        gameSaveLocation = Application.persistentDataPath + "/balls.gcsav";

        statusText.text = "Attempting to log in...";
        StartCoroutine(WaitForLogin()); 
    }
    public IEnumerator WaitForLogin()
    {
        float timeSpent = 0;
        while (!GameServices.IsInitialized()){
            timeSpent += Time.deltaTime;
            if(timeSpent > 3)
            {
                onLoadEvent.Invoke();
                StopAllCoroutines();
            }
            yield return null;
        }
        statusText.text = "Logged in!";
        LoadGame();

        yield return new WaitForSeconds(2);
        onLoadEvent.Invoke();
    }
    public void LoadGame()
    {
        if (saveEnabled)
        {
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
            GameServices.SavedGames.OpenWithAutomaticConflictResolution(savedGameName, (openedGame, error) =>
            {
                if (string.IsNullOrEmpty(error))
                {
                    GameServices.SavedGames.WriteSavedGameData(savedGame, Utils.ObjectSerializationExtension.SerializeToByteArray(save), (updatedSavedGame, writingError) =>
                    {
                        if (string.IsNullOrEmpty(writingError))
                        {
                            Debug.Log("Saved game data has been written successfully!");
                        }
                        else
                        {
                            Debug.LogError("Writing saved game data failed with error: " + writingError);
                        }
                    });
                }
            });
        }
    }
    public void GiveGold()
    {
        save.totalGold += 1000;
    }

}
