using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class RandomSeagulls : MonoBehaviour
{
    [EventRef]
    public string gullSound;

    public float minDelay = 10;
    public float maxDelay = 35;

    float timer;
    // Start is called before the first frame update
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
