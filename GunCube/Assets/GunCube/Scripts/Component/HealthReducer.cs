using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReducer : MonoBehaviour
{
    public string collisionTag = "Enemy";

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == collisionTag)
        {
            ManagerManager.scoreManager.ReduceHealth(collision.gameObject.GetComponent<EnemyController>().damage);
        }
    }
}
