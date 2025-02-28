using UnityEngine;

public class FlyEnemy : BaseEnemy
{
	[SerializeField] private GameObject explosionPrefabs;
	private void CreateExplosion()
	{
		if (explosionPrefabs != null)
		{
			Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
		}
	}
	protected override void MoveToPlayer()
	{
		if (player != null && currentHp != 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
			FlipEnemy();
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			Die();
			CreateExplosion();
		}
	}
	protected override void Die()
	{
		CreateExplosion();
		Destroy(gameObject);
	}





}
