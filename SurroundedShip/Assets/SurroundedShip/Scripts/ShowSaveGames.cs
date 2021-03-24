///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 24, 2021    ///
///////////////////////////////
///
using UnityEngine;
using EasyMobile;

public class ShowSaveGames : MonoBehaviour
{
    public void ShowSelectUI()
    {
        GameServices.SavedGames.ShowSelectSavedGameUI(
       "Select Saved Game",    // UI title
       5,        // maximum number of displayed saved games
       true,    // allow creating saved games     
       true,    // allow deleting saved games
       (SavedGame game, string error) =>
       {
           if (string.IsNullOrEmpty(error))
           {
               Debug.Log("You selected saved game: " + game.Name);
               OptionsHolder.savedGameName = game.Name;
           }
           else
           {
               Debug.Log(error);
           }
       }
    );
    }
}
