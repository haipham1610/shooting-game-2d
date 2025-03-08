using UnityEngine;

public class FlyEnemy : BaseEnemy
{
	[SerializeField] private GameObject explosionPrefabs;

	// Explosion
	private void CreateExplosion()
	{
		if (explosionPrefabs != null)
		{
			Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
		}
	}

	//Move
	protected override void MoveToPlayer()
	{
		if (player != null && currentHp != 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
			FlipEnemy();
		}
	}

	//Va cham
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			Die();
			CreateExplosion();
		}
	}

	//Die
	protected override void Die()
	{
		CreateExplosion();
		Destroy(gameObject);
	}

}
