using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Player player = collision.GetComponent<Player>();
			if (player != null)
			{
				player.TakeDamage(10f);
			}
			Destroy(gameObject);
		}
	}
}
