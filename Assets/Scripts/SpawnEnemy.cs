using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; 
    [SerializeField] private Transform[] spawnPoints; 
    private float startDelay = 2.0f; 
    private float repeatDelay = 3f; 
    private float minDelay = 1.5f; 
    private float speedUpRate = 0.2f;
    private bool isPlayerDead = false;

    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, repeatDelay);
        InvokeRepeating("IncreaseSpawnSpeed", 10f, 10f); 
    }

    void SpawnRandomEnemy()
    {
        if (isPlayerDead) return;
        if (spawnPoints.Length > 0)
        {
            
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPos = spawnPoints[randomSpawnPoint].position;

            
            int randomIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomIndex], spawnPos, Quaternion.identity);
        }
    }

    void IncreaseSpawnSpeed()
    {
        if (repeatDelay > minDelay)
        {
            repeatDelay -= speedUpRate;
            CancelInvoke("SpawnRandomEnemy");
            InvokeRepeating("SpawnRandomEnemy", 0f, repeatDelay);
        }
    }

    public void StopSpawning()
    {
        isPlayerDead = true;
        CancelInvoke("SpawnRandomEnemy");
        CancelInvoke("IncreaseSpawnSpeed");
    }
}
