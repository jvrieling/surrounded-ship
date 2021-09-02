///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Plays a sound when clicked.
/// </summary>
public class ClickSound : MonoBehaviour
{
    [EventRef]
    public string sound;

    private void OnMouseDown()
    {
        PlaySound();
    }
    public void PlaySound()
    {
        RuntimeManager.PlayOneShot(sound);

    }
}
