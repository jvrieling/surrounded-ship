///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/// <summary>
/// This is applied to a GameObject to make it destroyable via cannonballs.
/// </summary>
public class DestructableObject : MonoBehaviour
{
    public float health = 1;
    public int pointValue = 0;
    public int goldValue = 0;
    public float spawnYDelta = 0;
    public bool dead = false;

    [Tooltip("This prefab will be instantiated just before the object dies.")]
    public GameObject[] deathPrefabs;

    [EventRef]
    public string deathSound;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= collision.gameObject.GetComponent<BulletController>().bulletDamage;
            CheckAlive();
        }
    }

    public void CheckAlive()
    {
        //If there's no health left, the object is dead...
        if (health <= 0 && !dead)
        {
            dead = true;

            ManagerManager.scoreManager.AddScore(pointValue);
            ManagerManager.scoreManager.AddGold(goldValue);
            if (gameObject.tag == "Enemy") ManagerManager.scoreManager.AddKill();

            //Initiate the prefabs, if there are any specified
            if (deathPrefabs.Length > 0)
            {
                GameObject temp;
                foreach (GameObject deathPrefab in deathPrefabs)
                {
                    temp = Instantiate(deathPrefab, transform.position, Quaternion.identity);
                    temp.transform.rotation = transform.rotation;
                    temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y - spawnYDelta, temp.transform.position.z);
                }
            }

            if (deathSound != "")
            {
                RuntimeManager.PlayOneShot(deathSound);
            }

            //Finally, destroy this GameObject
            Destroy(gameObject);
        }
    }
}
