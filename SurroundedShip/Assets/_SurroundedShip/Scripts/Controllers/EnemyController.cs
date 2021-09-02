///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls enemies. It moves the object towards the player, handles the incoming data (from Enemy), and handles the wake particles.
/// </summary>
[RequireComponent(typeof(DestructableObject))]
public class EnemyController : MonoBehaviour
{
    private const float MOVE_SPEED_MODIFIER = 3.8f;
    public Enemy enemyData;

    public float moveSpeed;
    public int damage;

    private GameObject player;
    private Rigidbody rb;

    public ParticleSystem[] wakeParticles;

    public MeshRenderer[] sailsMeshRenderer;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        foreach (ParticleSystem i in wakeParticles)
        {
            ParticleSystem.MainModule psmain = i.main;
            psmain.startSpeed = (moveSpeed * 0.04f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(((player.transform.position - transform.position) * (moveSpeed) * MOVE_SPEED_MODIFIER) * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, (transform.position - player.transform.position) * moveSpeed);
        Gizmos.color = Color.white;
    }

    public void InitializeData(Enemy data)
    {
        gameObject.name = data.name;

        enemyData = data;

        moveSpeed = data.movementSpeed;
        damage = data.damage;
        DestructableObject temp = GetComponent<DestructableObject>();

        temp.health = data.hp;
        temp.health += ManagerManager.scoreManager.difficulty * 0.7f;

        temp.pointValue = data.pointValue;
        temp.pointValue += Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.5f);

        temp.goldValue = data.goldValue;
        temp.goldValue += Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.35f);

        foreach (MeshRenderer i in sailsMeshRenderer)
        {
            i.material.color = data.color;
        }
    }

}
