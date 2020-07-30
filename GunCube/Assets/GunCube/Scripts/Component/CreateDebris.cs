using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
