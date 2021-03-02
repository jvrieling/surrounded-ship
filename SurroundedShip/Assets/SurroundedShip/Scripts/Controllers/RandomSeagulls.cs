///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// RandomSeagulls is a script to play an ambient seagull sound at timed intervals. The intervals ar randomized between a min and max.
/// </summary>
public class RandomSeagulls : MonoBehaviour
{
    [EventRef]
    public string gullSound;

    [SerializeField] private float minDelay = 10;
    [SerializeField] private float maxDelay = 35;

    private float timer;
    
    void Start()
    {
        ResetTimer();   
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            RuntimeManager.PlayOneShot(gullSound);
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
