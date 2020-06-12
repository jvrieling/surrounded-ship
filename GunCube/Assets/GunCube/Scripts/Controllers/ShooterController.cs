using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public float timeBetweenShots = 1;
    private float shotTimer = 0;

    public GameObject bulletPrefab;

    public float bulletVelocity = 1;
    public float bulletDamage = 1;
    public float accuracy = 0.7f;

    public int sniperUpgrades;
    public int shotgunUpgrades;
    public int cannonUpgrades;

    // Update is called once per frame
    void Update()
    {

        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
        else if (shotTimer <= 0)
        {
            shotTimer = timeBetweenShots;
            GameObject temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
            temp.GetComponent<BulletController>().InstantiateBullet(transform.right, bulletVelocity, bulletDamage);
            temp.GetComponent<BulletController>().tagToIgnore = transform.parent.tag;
            temp.transform.SetParent(ManagerManager.shooterManager.bulletHolder.transform);
        }
    }
}
