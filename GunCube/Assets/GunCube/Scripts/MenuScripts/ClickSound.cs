using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ClickSound : MonoBehaviour
{
    [EventRef]
    public string sound;

    private void OnMouseDown()
    {
        Debug.Log("Clicked!!!");
        PlaySound();
    }
    public void PlaySound()
    {
        RuntimeManager.PlayOneShot(sound);

    }
}
