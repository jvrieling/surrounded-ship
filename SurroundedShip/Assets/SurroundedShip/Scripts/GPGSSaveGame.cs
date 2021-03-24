///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 18, 2021     ///
///////////////////////////////
using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;

public class GPGSSaveGame : MonoBehaviour
{
    public static ISavedGameMetadata openedGame;
    public static byte[] previousData;
    public static Action<byte[]> onLoadAction;

    public static Text statusDisplay;
    public Text tempStatus;
    private void Awake()
    {
        statusDisplay = tempStatus;
        openedGame = null;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OpenSavedGame("sssave", (status, meta) =>
        {
            if (status == SavedGameRequestStatus.Success)
            {
                openedGame = meta;
                statusDisplay.text = "Opened savegame";
            }
            else
            {
                Debug.LogError("Failed to open the GPG save game!");
                statusDisplay.text = "failed to open from GPG cloud.";
            }
        });
    }

    public static void OpenSavedGame(string filename, Action<SavedGameRequestStatus, ISavedGameMetadata> callback = null)
    {
        if (callback == null) callback = OnSavedGameOpened;
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, callback);
    }

    public static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            openedGame = game;
            statusDisplay.text = "Opened savegame";
        }
        else
        {
            Debug.LogError("Failed to open the GPG save game!");
            statusDisplay.text = "failed to open";
        }
    }
    public static void SaveGame(byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(openedGame, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved the game to the GPG cloud!");
            statusDisplay.text = "saved to the cloud!";
        }
        else
        {
            Debug.LogError("Failed to save the game to GPG!");
            statusDisplay.text = "failed to save!";
        }
    }
    public static void LoadGameData(Action<byte[]> callback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        onLoadAction = callback;
        savedGameClient.ReadBinaryData(openedGame, OnSavedGameDataRead);
    }

    public static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            statusDisplay.text = "data read! ";
            previousData = data;
            Debug.Log("Loaded the game from GPG!");
            onLoadAction.Invoke(data);
        }
        else
        {
            Debug.LogError("Failed to load the GPG save game!");
            statusDisplay.text = "failed to load data";
            onLoadAction.Invoke(null);
        }
    }
}
