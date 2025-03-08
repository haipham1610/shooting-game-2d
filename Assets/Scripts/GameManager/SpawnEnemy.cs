using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField] private GameObject[] enemiesLevel1;
	[SerializeField] private GameObject[] enemiesLevel2;
	[SerializeField] private GameObject[] enemiesLevel3;
	[SerializeField] private Transform[] spawnPositions;
	[SerializeField] private float timeBetweenSpawns = 2f;

	[SerializeField] private int bossLevel = 1;
	[SerializeField] private int enemiesSpawned = 0;
	[SerializeField] private int maxEnemiesPerLevel = 20; // Level 1
	[SerializeField] private bool bossSpawned = false;
	[SerializeField] private GameObject[] gunPickupPrefabs;
	private GameManager gameManager;

	void Start()
	{
		gameManager = FindAnyObjectByType<GameManager>();
		StartCoroutine(SpawnEnemyCoroutine());
	}

	private IEnumerator SpawnEnemyCoroutine()
	{
		while (true)
		{
			if (enemiesSpawned >= maxEnemiesPerLevel)
			{
				// Check quantity enemy &  not spawn boss -> spawn boss
				if (!bossSpawned && !gameManager.IsBossCalled())
				{
					yield return new WaitForSeconds(timeBetweenSpawns);
					SpawnBoss();
					bossSpawned = true;
					gameManager.SetBossCalled(true);
				}
				yield return null;
			}
			else
			{
				yield return new WaitForSeconds(timeBetweenSpawns);

				GameObject enemyToSpawn = GetEnemyForCurrentLevel();
				Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

				Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
				enemiesSpawned++;
			}
		}
	}

	private GameObject GetEnemyForCurrentLevel()
	{
		GameObject[] enemiesArray = GetEnemiesArray();
		return enemiesArray[Random.Range(0, enemiesArray.Length - 1)];
	}

	private void SpawnBoss()
	{
		GameObject[] enemiesArray = GetEnemiesArray();
		GameObject boss = enemiesArray[enemiesArray.Length - 1];
		Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

		Instantiate(boss, spawnPosition.position, Quaternion.identity);
	}

	private GameObject[] GetEnemiesArray()
	{
		switch (bossLevel)
		{
			case 1:
				return enemiesLevel1;
			case 2:
				return enemiesLevel2;
			case 3:
				return enemiesLevel3;
			default:
				return null;
		}
	}

	public void OnBossDefeated()
	{
		bossLevel++;
		enemiesSpawned = 0; // Reset quantity quai
		bossSpawned = false; // Reset status boss
		gameManager.SetBossCalled(false);

		switch (bossLevel)
		{
			case 2:
				timeBetweenSpawns++;
				maxEnemiesPerLevel = 20; // Level 2 
				break;
			case 3:
				timeBetweenSpawns++;
				maxEnemiesPerLevel = 25; // Level 3 
				break;
		}
		StopAllCoroutines();
		StartCoroutine(SpawnEnemyCoroutine());
	}
}
