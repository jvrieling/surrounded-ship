///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to toggle objects on and off.
/// </summary>
public class SetEnabledOnEvent : MonoBehaviour
{
    public GameObject[] disableObjects;
    public GameObject[] enableObjects;

    private bool reverse = false;

    public void ToggleObjects()
    {
        foreach(GameObject i in disableObjects)
        {
            i.SetActive(reverse);
        }
        foreach(GameObject i in enableObjects)
        {
            i.SetActive(!reverse);
        }
        reverse = !reverse;
    }
}
