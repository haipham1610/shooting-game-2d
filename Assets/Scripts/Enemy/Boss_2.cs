using UnityEngine;

public class Boss_2 : BaseEnemy
{
	[SerializeField] private GameObject flyEnemy;
	private float nextSkillTime = 1f;
	[SerializeField] private float skillCooldown = 3f;
	[SerializeField] private GameObject gunPickupPrefab;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player.TakeDamage(enterDamage);
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player.TakeDamage(stayDamage);
		}
	}
	protected override void Update()
	{
		base.Update();
		if (Time.time >= nextSkillTime)
		{
			CreateFlyEnemy();
		}
	}
	private void CreateFlyEnemy()
	{
		nextSkillTime = Time.time + skillCooldown;
		Instantiate(flyEnemy, transform.position, Quaternion.identity);
	}
	protected override void Die()
	{
		base.Die();
		if (gunPickupPrefab != null)
		{
			Instantiate(gunPickupPrefab, transform.position, Quaternion.identity);
		}
		gameManager.BossDefeated(transform.position);
	}
}
