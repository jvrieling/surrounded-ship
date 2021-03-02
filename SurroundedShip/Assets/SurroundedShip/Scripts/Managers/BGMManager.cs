///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Manages the background music.
/// </summary>
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    FMOD.Studio.EventInstance BGM;

    [EventRef]
    public string music;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        BGM = RuntimeManager.CreateInstance(music);
        StartMusic();
    }
    public void StartMusic()
    {
        if (!IsPlaying(BGM)) {
            BGM.setParameterByName("Level", 0);
            BGM.start();
        }
    }
    public void StopMusic()
    {
        BGM.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void SetMusicLevel(float level)
    {
        BGM.setParameterByName("Level", level);
    }
    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
