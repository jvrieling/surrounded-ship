using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnimator : MonoBehaviour
{
    public string trigger = "die";

    void Start()
    {
        GetComponent<Animator>().SetTrigger(trigger);
    }
}