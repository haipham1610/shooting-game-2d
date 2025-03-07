using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//Move
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
	public Vector3 moveInput;
	private SpriteRenderer rbSprite;
	private Animator animator;

	//Gun
	[SerializeField] private GameObject[] guns;
	[SerializeField] private Transform gunPosition;
	private int currentGun = 0;

	//HP
	[SerializeField] private float maxHp = 100f;
	[SerializeField] private Image hpBar;
	private float currentHp;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rbSprite = rb.GetComponent<SpriteRenderer>();
		animator = rb.GetComponent<Animator>();
		ChangeGun(currentGun);
		currentHp = maxHp;
		UpdateHpBar();
	}

	void Update()
	{
		Move();
		EquipGun();
	}

	//Di chuyen
	void Move()
	{
		moveInput.x = Input.GetAxis("Horizontal");
		moveInput.y = Input.GetAxis("Vertical");
		transform.position += moveInput * moveSpeed * Time.deltaTime;
		animator.SetBool("isRun", moveInput.sqrMagnitude > 0);


		if (moveInput.x != 0)
		{
			if (moveInput.x < 0)
			{
				rbSprite.flipX = true;
			}
			else
			{
				rbSprite.flipX = false;
			}
		}
	}

	/* Change Gun */
	void EquipGun()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun(0);
		if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeGun(1);
		if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeGun(2);
	}
	void ChangeGun(int gunIndex)
	{
		for (int i = 0; i < guns.Length; i++)
		{
			guns[i].gameObject.SetActive(i == gunIndex);
		}
	}
	/* Change Gun */

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
	private void Die()
	{
		animator.SetBool("isDeath", true);
		Destroy(gameObject, 0.5f);
	}

	//Bar
	private void UpdateHpBar()
	{
		if (hpBar != null)
		{
			hpBar.fillAmount = currentHp / maxHp;
		}
	}

	public void Heath()
	{
		if (currentHp < maxHp)
		{
			currentHp += 5f;
			currentHp = Mathf.Min(currentHp, maxHp);
			UpdateHpBar();
		}
	}
}
