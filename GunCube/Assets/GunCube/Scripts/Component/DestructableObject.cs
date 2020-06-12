using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public float health = 1;
    public int pointValue = 0;

    [Tooltip("This prefab will be instantiated just before the object dies.")]
    public GameObject deathPrefab;

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
            Destroy(gameObject);
            ManagerManager.scoreManager.AddScore(pointValue);
        }
    }
}
