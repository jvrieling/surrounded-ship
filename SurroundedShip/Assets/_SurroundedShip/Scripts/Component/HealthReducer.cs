///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// HealthReducer is used on the player object to handle colliding with the specified tag. 
/// If the collision has the tag, it gets the amount of damage it does from its EnemyController and notifies the ScoreManager of the event.
/// </summary>
public class HealthReducer : MonoBehaviour
{
    public string collisionTag = "Enemy";
    public string collisionTag2;

    [EventRef]
    public string damageSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == collisionTag || other.gameObject.tag == collisionTag2)
        {
            ManagerManager.scoreManager.ReduceHealth(other.gameObject.GetComponent<EnemyController>().damage);
            RuntimeManager.PlayOneShot(damageSound);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == collisionTag || collision.gameObject.tag == collisionTag2)
        {
            ManagerManager.scoreManager.ReduceHealth(collision.gameObject.GetComponent<EnemyController>().damage);
        }
    }

#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            {
                collisionTag = "";
                collisionTag2 = "";
            } }
    }
#endif

}
