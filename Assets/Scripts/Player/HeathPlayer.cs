using UnityEngine;

public class HeathPlayer : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Player player = collision.GetComponent<Player>();
			if (player != null)
			{
				player.Heath();
			}
			Destroy(gameObject);
		}
	}
}
