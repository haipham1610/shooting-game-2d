using UnityEngine;

public class Boss : BaseEnemy
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player.TakeDamage(enterDamage);
		}
	}
}
