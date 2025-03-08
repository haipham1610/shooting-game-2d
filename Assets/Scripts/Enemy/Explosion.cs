using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] private float damage = 20f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Player player = collision.GetComponent<Player>();
		BaseEnemy baseEnemy = collision.GetComponent<BaseEnemy>();

		if (collision.CompareTag("Player"))
		{
			player.TakeDamage(damage);
		}
		if (collision.CompareTag("Enemy"))
		{
			baseEnemy.TakeDamage(damage);
		}
	}

	public void DestroyExplosion()
	{
		Destroy(gameObject);
	}
}
