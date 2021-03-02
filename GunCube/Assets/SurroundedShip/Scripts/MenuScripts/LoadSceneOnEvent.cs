///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the specified scene when triggered by an event.
/// </summary>
public class LoadSceneOnEvent : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        LoadScene(sceneToLoad);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
