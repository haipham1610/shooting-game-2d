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
	[SerializeField] private int maxEnemiesPerLevel = 15; // Level 1
	[SerializeField] private bool bossSpawned = false;
	[SerializeField] private GameObject[] gunPickupPrefabs;
	private GameManager gameManager;
	private AudioManager audioManager;

	void Start()
	{
		gameManager = FindAnyObjectByType<GameManager>();
		StartCoroutine(SpawnEnemyCoroutine());
		audioManager = FindAnyObjectByType<AudioManager>();
	}

	private IEnumerator SpawnEnemyCoroutine()
	{
		while (bossLevel <= 3)
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
					audioManager.PlayBossAudio();
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
		if (bossLevel > 3)
		{
			return null;
		}
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
		if (bossLevel > 3)
		{
			StopAllCoroutines();
			return;
		}
		enemiesSpawned = 0; // Reset quantity quai
		bossSpawned = false; // Reset status boss
		gameManager.SetBossCalled(false);
		audioManager.PlayDefaultAudio();
		switch (bossLevel)
		{
			case 2:
				timeBetweenSpawns--;
				maxEnemiesPerLevel = 20; // Level 2 
				break;
			case 3:
				maxEnemiesPerLevel = 25; // Level 3 
				break;
		}
		StopAllCoroutines();
		StartCoroutine(SpawnEnemyCoroutine());
	}
}
