///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A quick little script to summon a set amount of random prefabs.
/// </summary>
public class CreateDebris : MonoBehaviour
{
    public int debrisCount = 3;
    public GameObject[] debrisPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < debrisCount; i++)
        {
            Instantiate(debrisPrefabs[Random.Range(0, debrisPrefabs.Length)], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
