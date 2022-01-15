using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkSim : MonoBehaviour
{
    public float sinkSpeed = 2;
    public float sinkRotSpeed = 3;

    private Vector2 sinkDir;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        sinkDir.x = Random.Range(-1, 1);
        if (sinkDir.x < 0) sinkDir.x = -1;
        else sinkDir.x = 1;
        sinkDir.y = Random.Range(-1, 1);
        if (sinkDir.y < 0) sinkDir.x = -1;
        else sinkDir.y = 1;
    }

    
    void Update()
    {
        transform.Translate(new Vector3(0, -sinkSpeed * Time.deltaTime, 0));
        transform.Rotate(transform.right, sinkDir.x * sinkRotSpeed * Time.deltaTime);
        transform.Rotate(transform.forward, sinkDir.y * sinkRotSpeed * Time.deltaTime);
    }
}
