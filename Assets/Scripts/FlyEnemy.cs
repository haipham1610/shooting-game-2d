using UnityEngine;

public class FlyEnemy : BaseEnemy
{
	protected override void MoveToPlayer()
	{
		if (player != null && currentHp != 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
			FlipEnemy();
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player.TakeDamage(enterDamage);
		}
	}

	



}
