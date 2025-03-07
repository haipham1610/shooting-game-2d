using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour
{
	[SerializeField] protected float enemyMoveSpeed = 1f;
	protected Player player;
	[SerializeField] protected float maxHp = 50f;
	protected float currentHp;
	[SerializeField] protected Image hpBar;
	[SerializeField] protected float enterDamage = 10f;
	[SerializeField] protected float stayDamage = 1f;
	private Animator animator;

	protected virtual void Start()
	{
		player = FindAnyObjectByType<Player>();
		currentHp = maxHp;
		UpdateHpBar();
		animator = GetComponentInChildren<Animator>();
	}

	protected virtual void Update()
	{
		MoveToPlayer();
	}

	//Move 
	protected virtual void MoveToPlayer()
	{
		if (player != null && currentHp != 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
			FlipEnemy();
		}
	}

	//Flip face when move
	protected void FlipEnemy()
	{
		if (player != null)
		{
			transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
		}
	}

	//Get damage
	public void TakeDamage(float damage)
	{
		currentHp -= damage;
		currentHp = Mathf.Max(currentHp, 0);
		UpdateHpBar();
		if (currentHp <= 0)
		{
			Die();
		}
	}

	//Die
	protected virtual void Die()
	{
		animator.SetBool("isDeath", true);
		Destroy(gameObject, 0.5f);
	}

	//HP BAR 
	protected void UpdateHpBar()
	{
		if (hpBar != null)
		{
			hpBar.fillAmount = currentHp / maxHp;
		}
	}
}
