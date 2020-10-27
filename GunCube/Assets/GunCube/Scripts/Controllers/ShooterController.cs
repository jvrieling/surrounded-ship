using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ShooterController : MonoBehaviour
{
    public float timeBetweenShots = 1;
    private float shotTimer = 0;

    public GameObject bulletPrefab;

    public float bulletVelocity = 1;
    public float bulletDamage = 1;
    public float inaccuracy = 0.01f;
    public int bulletCount = 1;

    public int sniperUpgrades;
    public int shotgunUpgrades;
    public int cannonUpgrades;

    public ParticleSystem particle;

    [EventRef]
    public string shootSound;

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

            for (int i = 0; i < bulletCount; i++)
            {
                if (particle != null)
                {
                    particle.time = 0;
                    particle.Play();
                }

                RuntimeManager.PlayOneShot(shootSound);

                GameObject temp = Instantiate(bulletPrefab, transform.position, transform.rotation);

                float spreadFactor = inaccuracy;

                Vector3 direction = transform.right;

                direction.x += Random.Range(-spreadFactor, spreadFactor);
                //direction.y += Random.Range(-spreadFactor, spreadFactor);
                direction.z += Random.Range(-spreadFactor, spreadFactor);

                direction.Normalize();


                temp.GetComponent<BulletController>().InstantiateBullet(direction, bulletVelocity, bulletDamage);
                temp.GetComponent<BulletController>().tagsToIgnore = new string[] { transform.parent.tag, temp.tag};
                temp.transform.SetParent(ManagerManager.shooterManager.bulletHolder.transform);
            }
        }
    }


    public void InitializeData(ShooterData data)
    {
        inaccuracy = data.accuracy;
        bulletDamage = data.bulletDamage;
        bulletVelocity = data.bulletVelocity;
        timeBetweenShots = data.timeBetweenShots;
        bulletCount = data.bulletCount;
    }
}
