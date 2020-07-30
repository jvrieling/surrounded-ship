using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float floatPower = 0.2f;

    public bool sinkAfterTimer = false;

    public float sinkTimer = 5;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sinkAfterTimer)
        {
            sinkTimer -= Time.deltaTime;
            if(sinkTimer <= 0)
            {
                floatPower -= floatPower * 0.004f;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.tag == "Water")
        {
            rb.AddForce(new Vector3(0, floatPower, 0));
        }
    }
}
