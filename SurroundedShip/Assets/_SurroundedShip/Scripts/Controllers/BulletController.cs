///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// Makes the bullet fly and be destroyed.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    //The direction the bullet should travel in. Must be normalized!
    public Vector3 direction;
    public float velocity = 1f;

    public float bulletDamage = 1;

    public string[] tagsToIgnore;

    private Rigidbody rb;

    public GameObject collisionPrefab;

    [EventRef]
    public string hitSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = direction * velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(string i in tagsToIgnore)
        {
            if (other.gameObject.tag == i) return;
        }
        Instantiate(collisionPrefab, transform.position, Quaternion.identity);
        RuntimeManager.PlayOneShot(hitSound);
        Destroy(gameObject);
    }

    public void InstantiateBullet(Vector3 newDirection, float newVelocity, float newDamage)
    {
        direction = newDirection;
        velocity = newVelocity;
        bulletDamage = newDamage;
    }
}
