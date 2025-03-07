using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float timeDestroy = 0.5f;
    private float damage = 10f;

    void Start()
    {
        Destroy(gameObject, timeDestroy);    
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy") || collision.CompareTag("BossEnemy"))
        {
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
