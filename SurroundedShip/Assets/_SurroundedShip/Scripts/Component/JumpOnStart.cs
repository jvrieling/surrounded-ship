///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Launches the GameObject into the air when the object is initialized. Used for the chunks of ship and pirates when defeating a ship.
/// </summary>
public class JumpOnStart : MonoBehaviour
{
    public float launchPower = 1;
    public bool launchRotation = true;
    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-launchPower * 1.3f, launchPower * 1.3f), Random.Range(launchPower * 0.3f, launchPower * 1.3f), Random.Range(-launchPower * 1.3f, launchPower * 1.3f)), ForceMode.Impulse);
        if (launchRotation)
        {
            GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-launchPower * 1.3f, launchPower * 1.3f), Random.Range(launchPower * 0.3f, launchPower * 1.3f), Random.Range(-launchPower * 1.3f, launchPower * 1.3f)), ForceMode.Impulse);
        }
    }

}
