///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// invokes an event to load the main menu scene. To be used for flashing logos in a future update.
/// </summary>
public class LogoFlashes : MonoBehaviour
{
    public UnityEvent onLoadEvent;
    // Start is called before the first frame update
    void Start()
    {
        onLoadEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
