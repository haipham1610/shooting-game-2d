using UnityEngine;

public class BasicEnemy : BaseEnemy
{
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


}
