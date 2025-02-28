using UnityEngine;

public class HealthEnemy : BaseEnemy
{
	[SerializeField] private GameObject energyObject;
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
		if(gameObject != null)
		{
			GameObject energy = Instantiate(energyObject, transform.position, Quaternion.identity);
			Destroy(energy, 5f);
		}	
		base.Die();
	}
}
