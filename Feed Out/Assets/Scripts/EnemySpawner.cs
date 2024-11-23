using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      
    public float spawnInterval = 5f;   
    public float spawnIntervalDecrement = 0.5f; 
    public float minSpawnInterval = 2f; 
    public Vector3 spawnAreaSize;      
    public Transform spawnAreaCenter;   

    private float spawnTimer = 0f;
    private float timePassed = 0f;

    void Update()
    {
        // Timer for spawning enemies
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }


        timePassed += Time.deltaTime;

        if (timePassed >= 60f)
        {
            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= spawnIntervalDecrement;
                spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval);
            }
            timePassed = 0f;
        }

        void SpawnEnemy()
        {

            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            randomPosition += spawnAreaCenter.position;


            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter != null ? spawnAreaCenter.position : transform.position, spawnAreaSize);
    }
}
