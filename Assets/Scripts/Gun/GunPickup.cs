using UnityEngine;

public class GunPickup : MonoBehaviour
{
	[SerializeField] private GameObject gunPrefab;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Player player = collision.GetComponent<Player>();
			if (player != null)
			{
				player.PickupGun(gunPrefab);
				Destroy(gameObject); // Xóa súng rơi sau khi nhặt
			}
		}
	}
}
