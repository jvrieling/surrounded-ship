using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    public float radius = 5;

    public GameObject enemyPrefab;

    public float timeBetweenSpawns = 1;
    private float spawnTimer;

    public List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        ManagerManager.scoreManager.spawnSpeedChangeEvent.AddListener(SetSpawnTimer);
    }

    private void Update()
    {
        timeBetweenSpawns = ManagerManager.scoreManager.timeBetweenSpawns;
        spawnTimer += Time.deltaTime;
        if(spawnTimer > timeBetweenSpawns)
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

    public void SetSpawnTimer(float time)
    {
        timeBetweenSpawns = time;
    }

    public void SpawnEnemies(int count)
    {
        for(int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Enemy selectedEnemy = enemies[Random.Range(0, enemies.Count)];
        while(selectedEnemy.minDifficulty > ManagerManager.scoreManager.difficulty)
        {
            selectedEnemy = enemies[Random.Range(0, enemies.Count)];
        }

        Vector3 center = transform.position;
        Vector3 pos = RandomCircle(center, radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject temp = Instantiate(enemyPrefab, pos, rot);

        temp.transform.SetParent(transform);

        selectedEnemy.hp += ManagerManager.scoreManager.difficulty * 0.5f;
        selectedEnemy.goldValue += Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.5f);

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
