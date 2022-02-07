using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SharkState { Rising, Waiting, Charging, Recovering, Sinking, Dying }
public class SharkBoss : MonoBehaviour
{
    [Header("State Stuff")]
    public SharkState state = SharkState.Rising;

    public float risingTime = 1.5f;
    public float risingDistance = 3.3f;

    public float waitingTime = 3f;

    public float chargingSpeed = 2f;

    public float stunDamagePercent = 0.3f;
    public float stunTime = 2f;

    public float sinkDistance = 4f;
    public float sinkTime = 1f;

    public float sinkSpeed = 2f;

    [Header("Effects")]
    public GameObject stunParticle;
    public ParticleSystem[] wakeParticles;

    private float timer;
    private float chargeStartHp;

    private DestructableObject destructibleObject;
    private Animator an;

    void OnDrawGizmosSelected()
    {
        if (ManagerManager.instance != null)
        {
            Vector3 movement = (ManagerManager.instance.player.transform.position - transform.position).normalized;
            movement.y = 0;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + movement * chargingSpeed);
        }
    }
    void Start()
    {
        an = GetComponent<Animator>();

        transform.Translate(0, -sinkDistance, 0);
        destructibleObject = GetComponent<DestructableObject>();
        stunParticle.SetActive(false);

        foreach (ParticleSystem i in wakeParticles)
        {
            ParticleSystem.MainModule psmain = i.main;
            psmain.startSpeed = (chargingSpeed * 0.04f);
            i.gameObject.SetActive(false);
        }

        CircleSpawner.bossActive = true;

        DestructableObject d = GetComponent<DestructableObject>();

        d.health += (ManagerManager.scoreManager.difficulty * 0.7f) + (Mathf.Floor(ManagerManager.scoreManager.difficulty / 20) * 20);
    }

    void Update()
    {
        transform.LookAt(ManagerManager.instance.player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (state == SharkState.Rising)
        {
            transform.Translate(0, risingDistance / risingTime * Time.deltaTime, 0);
            timer += Time.deltaTime;
            if (timer >= risingTime)
            {
                state = SharkState.Waiting;
                timer = 0;
                an.SetTrigger("idle");
            }
        }
        else if (state == SharkState.Waiting)
        {
            timer += Time.deltaTime;
            if (timer >= waitingTime)
            {
                an.SetTrigger("swim");
                state = SharkState.Charging;
                timer = 0;
                chargeStartHp = destructibleObject.health;

                foreach (ParticleSystem i in wakeParticles)
                {
                    i.gameObject.SetActive(true);
                }
            }
        }
        else if (state == SharkState.Charging)
        {
            transform.LookAt(ManagerManager.instance.player.transform.position);
            Vector3 movement = (ManagerManager.instance.player.transform.position - transform.position).normalized;
            movement.y = 0;
            transform.position += (movement * chargingSpeed * Time.deltaTime);

            if (chargeStartHp - destructibleObject.health > destructibleObject.originalHealth * stunDamagePercent)
            {
                an.SetTrigger("stun");
                state = SharkState.Recovering;
                stunParticle.SetActive(true);
                foreach (ParticleSystem i in wakeParticles)
                {
                    i.gameObject.SetActive(false);
                }
            }
        }
        else if (state == SharkState.Recovering)
        {
            timer += Time.deltaTime;
            if (timer >= stunTime)
            {
                an.SetTrigger("sink");
                stunParticle.SetActive(false);
                state = SharkState.Sinking;
                timer = 0;
            }
        }
        else if (state == SharkState.Sinking)
        {
            transform.Translate(0, -(sinkDistance / risingTime * Time.deltaTime), 0);
            timer += Time.deltaTime;
            if (timer >= sinkTime)
            {
                an.SetTrigger("idle");
                state = SharkState.Rising;
                timer = 0;
                transform.position = RandomCircle(Vector3.zero, 15) - new Vector3(0, sinkDistance, 0);
            }
        }
    }

    private void OnDestroy()
    {
        CircleSpawner.bossActive = false;
        BGMManager.instance.SetMusicLevel(1);
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }


}
