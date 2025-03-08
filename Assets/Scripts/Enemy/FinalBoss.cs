using UnityEngine;

public class FinalBoss : BaseEnemy
{
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Transform firePosition;
	[SerializeField] private float speedFire = 10f;
	[SerializeField] private float hpValue = 100f;
	[SerializeField] private GameObject miniEnemy;
	[SerializeField] private float skillCooldown = 2f;
	private float nextSkillTime = 0f;
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
	protected override void Update()
	{
		base.Update();
		if (Time.time >= nextSkillTime)
		{
			UseSkill();
		}
	}

	private void NormalShoot()
	{
		if (player != null)
		{
			Vector3 directionToPlayer = player.transform.position - firePosition.position;
			directionToPlayer.Normalize();
			GameObject bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
			FinalBossBullet finalBossBullet = bullet.AddComponent<FinalBossBullet>();
			finalBossBullet.SetMoveDirection(directionToPlayer * speedFire);
		}
	}

	private void ShootAround()
	{
		const int bulletCount = 12;
		float angleStep = 360f / bulletCount;
		for (int i = 0; i < bulletCount; i++)
		{
			float angle = i * angleStep;
			Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
			GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			FinalBossBullet finalBossBullet = bullet.AddComponent<FinalBossBullet>();
			finalBossBullet.SetMoveDirection(bulletDirection * speedFire);
		}
	}

	private void Health()
	{
		currentHp = Mathf.Min(currentHp + 20f, maxHp);
		UpdateHpBar();
	}

	private void CreateEnemy()
	{
		Instantiate(miniEnemy, transform.position, Quaternion.identity);
	}

	private void Flash()
	{
		if (player != null)
		{
			transform.position = player.transform.position;
		}
	}

	private void RandomSkill()
	{
		int random = Random.Range(0, 5);
		switch (random)
		{
			case 0:
				NormalShoot();
				break;
			case 1:
				ShootAround();
				break;
			case 2:
				Health();
				break;
			case 3:
				CreateEnemy();
				break;
			case 4:
				Flash();
				break;
		}
	}

	private void UseSkill()
	{
		nextSkillTime = Time.time+skillCooldown;
		RandomSkill();
	}

	protected override void Die()
	{
		base.Die();
		gameManager.BossDefeated(transform.position);
	}
}
