using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReducer : MonoBehaviour
{
    public string collisionTag = "Enemy";

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == collisionTag)
        {
            ManagerManager.scoreManager.ReduceHealth(other.gameObject.GetComponent<EnemyController>().damage);
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
