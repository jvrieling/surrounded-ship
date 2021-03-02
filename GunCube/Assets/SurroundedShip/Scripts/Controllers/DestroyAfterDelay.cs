///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A quick little script to destroy the object after a certain amount of seconds.
/// </summary>
public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
