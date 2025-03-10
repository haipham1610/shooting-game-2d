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

	//MENU
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject gameOverMenu;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject winnerMenu;

	[SerializeField] private AudioManager audioManager;	
	void Start()
	{
		spawnEnemy = FindAnyObjectByType<SpawnEnemy>();
		MainMenu();
		UpdateProcessBar();
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

	/* *
	 * MENU
	 * */
	public void MainMenu()
	{
		mainMenu.SetActive(true);
		gameOverMenu.SetActive(false);
		pauseMenu.SetActive(false);
		winnerMenu.SetActive(false);
		Time.timeScale = 0f;
	}
	public void GameOverMenu()
	{
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(true);
		pauseMenu.SetActive(false);
		winnerMenu.SetActive(false);
		Time.timeScale = 0f;
	}
	public void PauseMenu()
	{
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		pauseMenu.SetActive(true);
		winnerMenu.SetActive(false);
		Time.timeScale = 0f;
	}

	public void StartGame()
	{
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		pauseMenu.SetActive(false);
		winnerMenu.SetActive(false);
		Time.timeScale = 1f;
		audioManager.PlayDefaultAudio();
	}
	public void ResumeGame()
	{
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		pauseMenu.SetActive(false);
		winnerMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	public void Winner()
	{
		mainMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		pauseMenu.SetActive(false);
		winnerMenu.SetActive(true);
		Time.timeScale = 0f;
	}

}
