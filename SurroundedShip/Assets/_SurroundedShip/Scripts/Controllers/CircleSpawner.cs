///////////////////////////////
/// Author: Justin Vrieling ///
/// Date: March 2, 2021     ///
///////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns enemies in a circle around a location. Used to summon enemies to attack the player.
/// </summary>
public class CircleSpawner : MonoBehaviour
{
    public float radius = 5;

    public GameObject enemyPrefab;
    public GameObject[] bossPrefabs;

    public float timeBetweenSpawns = 1;
    private float spawnTimer;

    public GameObject lockUI;

    public List<Enemy> enemies = new List<Enemy>();

    public static bool bossActive;

    private void Update()
    {
        lockUI.SetActive(bossActive);

        timeBetweenSpawns = ManagerManager.scoreManager.timeBetweenSpawns;
        spawnTimer += Time.deltaTime;
        if (spawnTimer > timeBetweenSpawns * ((bossActive) ? 2:1))
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
    }

    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnBoss(int index)
    {
        index = (bossPrefabs.Length - (index % bossPrefabs.Length)) - 1;   

        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center, radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject temp = Instantiate(bossPrefabs[index], pos, rot);
        BGMManager.instance.SetMusicLevel(2);
        temp.transform.SetParent(transform);
    }

    public void SpawnEnemy()
    {
        Enemy selectedEnemy = enemies[Random.Range(0, enemies.Count)];
        while (selectedEnemy.minDifficulty > ManagerManager.scoreManager.difficulty)
        {
            selectedEnemy = enemies[Random.Range(0, enemies.Count)];
        }
        SpawnEnemy(selectedEnemy);
    }
    public void SpawnEnemy(Enemy selectedEnemy)
    {
        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center, radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject temp = Instantiate(enemyPrefab, pos, rot);

        temp.transform.SetParent(transform);

        EnemyController tempController = temp.GetComponent<EnemyController>();
        tempController.InitializeData(selectedEnemy);
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
