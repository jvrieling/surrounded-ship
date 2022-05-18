using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public string reference;

    private void Awake()
    {
        //Check if the button has been pressed before
        gameObject.SetActive(!PlayerPrefs.HasKey(reference));
    }

    public void ButtonPressed()
    {
        //Set the button to pressed
        PlayerPrefs.SetFloat(reference, 1);
        gameObject.SetActive(false);
    }
}
