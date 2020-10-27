using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DestructableObject : MonoBehaviour
{
    public float health = 1;
    public int pointValue = 0;
    public int goldValue = 0;

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
        if (health <= 0)
        {
            ManagerManager.scoreManager.AddScore(pointValue);
            ManagerManager.scoreManager.AddGold(goldValue);
            if (gameObject.tag == "Enemy") ManagerManager.scoreManager.AddKill();

            if (deathPrefabs.Length > 0)
            {
                foreach (GameObject deathPrefab in deathPrefabs)
                    Instantiate(deathPrefab, transform.position, Quaternion.identity);
            }

            if(deathSound != "")
            {
                RuntimeManager.PlayOneShot(deathSound);
            }
            Destroy(gameObject);
        }
    }
}
