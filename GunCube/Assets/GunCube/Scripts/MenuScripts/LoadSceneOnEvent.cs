using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
