using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnStart : MonoBehaviour
{

    public float launchPower = 1;
    public bool launchRotation = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-launchPower * 1.3f, launchPower * 1.3f), Random.Range(launchPower * 0.3f, launchPower * 1.3f), Random.Range(-launchPower * 1.3f, launchPower * 1.3f)), ForceMode.Impulse);
        if (launchRotation)
        {
            GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-launchPower * 1.3f, launchPower * 1.3f), Random.Range(launchPower * 0.3f, launchPower * 1.3f), Random.Range(-launchPower * 1.3f, launchPower * 1.3f)), ForceMode.Impulse);
        }
    }

}
