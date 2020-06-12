using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableObject))]
public class EnemyController : MonoBehaviour
{
    public Enemy enemyData;

    public float moveSpeed;
    public int damage;

    private GameObject player;
    private Rigidbody rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(((player.transform.position - transform.position) * moveSpeed) * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, (transform.position - player.transform.position) * moveSpeed);
        Gizmos.color = Color.white;
    }

    public void InitializeData(Enemy data)
    {
        enemyData = data;

        moveSpeed = data.movementSpeed;
        damage = data.damage;
        GetComponent<DestructableObject>().health = data.hp;
        GetComponent<MeshRenderer>().material.color = data.color;
    }

}
