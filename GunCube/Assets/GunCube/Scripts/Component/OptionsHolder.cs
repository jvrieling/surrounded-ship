using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHolder : MonoBehaviour
{
    public static SaveGame options;

    public SaveGame optionOnAwake;

    private void Awake()
    {
        if (optionOnAwake) options = optionOnAwake;
        else options = new SaveGame();

        DontDestroyOnLoad(gameObject);
    }
}
