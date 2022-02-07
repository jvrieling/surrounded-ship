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
    public const float MOVE_SPEED_MODIFIER = 3.8f;
    public Enemy enemyData;

    public float moveSpeed;
    public int damage;

    private static GameObject player;
    private Rigidbody rb;

    public ParticleSystem[] wakeParticles;

    public MeshRenderer[] sailsMeshRenderer;
    public MeshRenderer[] deckMeshes;
    public MeshRenderer[] deck2Meshes;
    public MeshRenderer[] flagMeshes;

    private Animator an;

    void Awake()
    {
        an = GetComponent<Animator>();

        if (enemyData != null) InitializeData(enemyData);
        if(player == null) player = GameObject.FindGameObjectWithTag("Player");
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
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, (transform.position - player.transform.position) * moveSpeed);
            Gizmos.color = Color.white;
        }
    }
    private void OnDestroy()
    {
        if(enemyData != null)
        if (enemyData.isBoss)
        {
            CircleSpawner.bossActive = false;
            BGMManager.instance.SetMusicLevel(1);
        }
    }
    public void InitializeData(Enemy data)
    {
        gameObject.name = data.name;

        enemyData = data;

        moveSpeed = data.movementSpeed;
        damage = data.damage;
        DestructableObject temp = GetComponent<DestructableObject>();

        temp.health = data.hp;
        temp.health += (ManagerManager.scoreManager.difficulty * 0.7f) + (Mathf.Floor(ManagerManager.scoreManager.difficulty/20) * 20);

        temp.pointValue = data.pointValue;
        temp.pointValue += Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.5f);

        temp.goldValue = data.goldValue;
        temp.goldValue += Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.35f);

        if (data.isBoss)
        {
            gameObject.tag = "Boss";
            CircleSpawner.bossActive = true;
        }

        transform.localScale = data.scale;

        foreach (MeshRenderer i in sailsMeshRenderer)
        {
            i.material.color = data.sailColour;
        }

        foreach (MeshRenderer i in deckMeshes) {
            i.material.color = data.deckColour;
        }
        foreach (MeshRenderer i in deck2Meshes)
        {
            i.material.color = data.deckColour;
        }
        foreach (MeshRenderer i in flagMeshes)
        {
            i.material.color = data.deckColour;
        }
    }

}
