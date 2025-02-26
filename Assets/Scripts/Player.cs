using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
	public Vector3 moveInput;
	private SpriteRenderer rbSprite;
	private Animator animator;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rbSprite = rb.GetComponent<SpriteRenderer>();
		animator = rb.GetComponent<Animator>();
	}

	void Update()
	{
		Move();


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
}
