using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HealthReducer : MonoBehaviour
{
    public string collisionTag = "Enemy";

    [EventRef]
    public string damageSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == collisionTag)
        {
            ManagerManager.scoreManager.ReduceHealth(other.gameObject.GetComponent<EnemyController>().damage);
            RuntimeManager.PlayOneShot(damageSound);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == collisionTag)
        {
            ManagerManager.scoreManager.ReduceHealth(collision.gameObject.GetComponent<EnemyController>().damage);
        }
    }
}
