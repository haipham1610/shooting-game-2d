using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Quantity enemy die
	public int currentEnemy = 0;
	[SerializeField] private int killedEnemyInLevel = 0; //Per level 

	//Process bar
	[SerializeField] private float maxProcessBar = 60;
	[SerializeField] private Image processBar;

	//Spawn
	protected SpawnEnemy spawnEnemy;
	private bool isCalledBoss = false;
	void Start()
	{
		spawnEnemy = FindAnyObjectByType<SpawnEnemy>();	
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AddProccessBar()
	{
		if (isCalledBoss)
		{
			return;
		}

		currentEnemy++;
		killedEnemyInLevel++;
		UpdateProcessBar();
	}

	public void BossDefeated(Vector2 bossPosition)
	{
		isCalledBoss = false;
		killedEnemyInLevel = 0;
		spawnEnemy.OnBossDefeated();
	}
	private void UpdateProcessBar()
	{
		if (processBar != null)
		{
			processBar.fillAmount = currentEnemy / maxProcessBar;
		}
	}
	public bool IsBossCalled()
	{
		return isCalledBoss;
	}

	public void SetBossCalled(bool value)
	{
		isCalledBoss = value;
	}

}
