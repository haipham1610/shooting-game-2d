using UnityEngine;

public class Boss : BaseEnemy
{
    [SerializeField] private float increasedSpeed = 5f; 
    [SerializeField] private float damageCooldown = 1f; 
    private float lastDamageTime; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(enterDamage); 
            lastDamageTime = Time.time; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                player.TakeDamage(stayDamage); 
                lastDamageTime = Time.time; 
            }
        }
    }

    private void Update()
    {
        if (currentHp <= maxHp * 0.2f)
        {
            enemyMoveSpeed = increasedSpeed; 
        }

        if (player != null && player.IsAlive())
        {
            MoveToPlayer();
        }
    }

    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
    }
}
