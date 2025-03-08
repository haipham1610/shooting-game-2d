using UnityEngine;

public class Boss_1 : BaseEnemy
{
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

	protected override void Die()
	{
		base.Die();
		if(gunPickupPrefab != null)
		{
			Instantiate(gunPickupPrefab, transform.position, Quaternion.identity);
		}
		gameManager.BossDefeated(transform.position);
	}
}
