using System.Collections.Generic;
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
	[SerializeField] private Transform gunPosition;
	[SerializeField] private GameObject defaultGunPrefab;
	[SerializeField] private List<GameObject> guns = new List<GameObject>();
	private GameObject currentGun;
	private int currentGunIndex = 0;

	//HP
	[SerializeField] private float maxHp = 100f;
	[SerializeField] private Image hpBar;
	private float currentHp;

	//game manager
	[SerializeField] private GameManager gameManager;
	private AudioManager audioManager;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rbSprite = rb.GetComponent<SpriteRenderer>();
		animator = rb.GetComponent<Animator>();
		EquipDefaultGun();
		currentHp = maxHp;
		UpdateHpBar();
		audioManager=FindAnyObjectByType<AudioManager>();
	}

	void Update()
	{
		Move();
		EquipGun();

		//Pause game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			gameManager.PauseMenu();
		}
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
	void EquipDefaultGun()
	{
		GameObject defaultGun = Instantiate(defaultGunPrefab, gunPosition.position, Quaternion.identity, gunPosition);
		defaultGun.transform.localPosition = Vector3.zero;
		defaultGun.transform.localRotation = Quaternion.identity;
		guns.Add(defaultGun); // Thêm vào danh sách
		currentGun = defaultGun;
	}

	/* Pickup Gun */
	public void PickupGun(GameObject newGunPrefab)
	{
		if (currentGun != null)
		{
			currentGun.SetActive(false);
		}

		GameObject newGun = Instantiate(newGunPrefab, gunPosition.position, Quaternion.identity, gunPosition);
		newGun.transform.localPosition = Vector3.zero;
		newGun.transform.localRotation = Quaternion.identity;
		guns.Add(newGun); // Lưu vào danh sách
		currentGun = newGun;
		currentGunIndex = guns.Count - 1; // Update current gun index
		currentGun.SetActive(true);
	}

	/* Change Gun */
	void EquipGun()
	{
		if (guns.Count < 2) { return; }
		if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeGun(0);
		if (Input.GetKeyDown(KeyCode.Alpha2) && guns.Count > 1) ChangeGun(1);
		if (Input.GetKeyDown(KeyCode.Alpha3) && guns.Count > 2) ChangeGun(2);
	}
	void ChangeGun(int gunIndex)
	{
		if (gunIndex < 0 || gunIndex >= guns.Count) return;

		if (currentGun != null)
		{
			currentGun.SetActive(false);
		}

		currentGun = guns[gunIndex];
		currentGun.SetActive(true);
		currentGunIndex = gunIndex;
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
		gameManager.GameOverMenu();
		audioManager.StopAudio();
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Winner"))
        {
			gameManager.Winner();
        }
    }
}